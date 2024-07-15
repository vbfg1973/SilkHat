using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SilkHat.Domain.CodeAnalysis.Abstract;
using SilkHat.Domain.CodeAnalysis.Extensions;
using SilkHat.Domain.Common.Locations;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Triples;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;
using Location = Microsoft.CodeAnalysis.Location;

namespace SilkHat.Domain.CodeAnalysis.Walkers.CSharp
{
    public class CSharpMethodInvocationWalker(
        TypeDeclarationSyntax declarationSyntax,
        WalkerOptions walkerOptions)
        : CSharpBaseTypeWalker(walkerOptions), ICodeWalker
    {
        private readonly TypeDeclarationSyntax _declarationSyntax = declarationSyntax;

        private readonly List<Triple> _triples = new();

        public IEnumerable<Triple> Walk()
        {
            base.Visit(_declarationSyntax);

            return _triples;
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax syntax)
        {
            MethodNode methodNode = GetMethodNode(syntax);

            foreach (ExpressionSyntax expressionSyntax in syntax.DescendantNodes().OfType<ExpressionSyntax>())
            {
                switch (expressionSyntax)
                {
                    case ObjectCreationExpressionSyntax creation:
                        ClassNode classNode = GetTypeNodeFromInstantiation(creation);
                        _triples.Add(new ConstructsTriple(methodNode, classNode));
                        break;
                    case InvocationExpressionSyntax invocation:
                        AddInvokedMethodTriple(invocation, methodNode);
                        break;
                }
            }

            base.VisitMethodDeclaration(syntax);
        }

        private ClassNode GetTypeNodeFromInstantiation(ObjectCreationExpressionSyntax creationExpressionSyntax)
        {
            return _walkerOptions.DotnetOptions.SemanticModel.GetTypeInfo(creationExpressionSyntax).CreateClassNode();
        }

        private void AddInvokedMethodTriple(InvocationExpressionSyntax invocation, MethodNode parentMethodNode)
        {
            ISymbol? symbol = _walkerOptions
                .DotnetOptions
                .SemanticModel
                .GetSymbolInfo(invocation)
                .Symbol;

            if (symbol is not IMethodSymbol invokedMethodSymbol) return;


            if (!invokedMethodSymbol.TryCreateMethodNode(_walkerOptions.DotnetOptions.SemanticModel,
                    out MethodNode? invokedMethod))
                return;

            Location loc = invocation.GetLocation();

            string invocationNodeName = parentMethodNode.FullName + "_" + invokedMethod!.FullName;
            Common.Locations.Location location = new(new LocationPosition(loc.SourceSpan.Start, 0), new LocationPosition(loc.SourceSpan.End, 0));
            InvocationNode invocationNode = new(parentMethodNode, invokedMethod, location);

            // Ignore dotnet's core methods
            if (invokedMethod.FullName.StartsWith("System", StringComparison.InvariantCultureIgnoreCase) ||
                invokedMethod.FullName.StartsWith("Microsoft.Asp", StringComparison.InvariantCultureIgnoreCase) ||
                invokedMethod.FullName.StartsWith("Microsoft.EntityFrameworkCore.Metadata",
                    StringComparison.InvariantCultureIgnoreCase) ||
                invokedMethod.FullName.StartsWith("Microsoft.EntityFrameworkCore.Migrations",
                    StringComparison.InvariantCultureIgnoreCase) ||
                invokedMethod.FullName.StartsWith("Microsoft.Extensions",
                    StringComparison.InvariantCultureIgnoreCase) ||
                invokedMethod.FullName.StartsWith("Moq", StringComparison.InvariantCultureIgnoreCase)
               ) return;

            _triples.Add(new InvokeTriple(parentMethodNode, invocationNode));
            _triples.Add(new InvocationOfTriple(invocationNode, invokedMethod));
        }
    }
}