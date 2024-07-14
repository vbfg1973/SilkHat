using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SilkHat.Domain.CodeAnalysis.Abstract;
using SilkHat.Domain.CodeAnalysis.Extensions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Triples;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;
using CSharpExtensions = Microsoft.CodeAnalysis.CSharp.CSharpExtensions;

namespace SilkHat.Domain.CodeAnalysis.Walkers.CSharp
{
    public class CSharpTypeDefinitionWalker(
        TypeDeclarationSyntax typeDeclarationSyntax,
        WalkerOptions walkerOptions)
        : CSharpBaseTypeWalker(walkerOptions), ICodeWalker
    {
        private readonly List<Triple> _triples = new();

        public IEnumerable<Triple> Walk()
        {
            base.Visit(typeDeclarationSyntax);

            return _triples;
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax syntax)
        {
            GetComplexity(syntax);
            GetHasTriple(syntax);
            GetImplementationOfTriples(syntax);

            base.VisitMethodDeclaration(syntax);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax syntax)
        {
            GetHasTriple(syntax);

            base.VisitPropertyDeclaration(syntax);
        }

        private void GetComplexity(MethodDeclarationSyntax syntax)
        {
            CSharpCognitiveComplexityWalker walker = new(syntax, _walkerOptions);

            MethodNode methodNode = GetMethodNode(syntax);
            CognitiveComplexityNode cognitiveComplexityNode = new(walker.ComplexityScore);

            _triples.Add(new HasComplexityTriple(methodNode, cognitiveComplexityNode));
        }

        private void GetHasTriple(MethodDeclarationSyntax syntax)
        {
            TypeNode typeNode = GetTypeNode(typeDeclarationSyntax);
            MethodNode methodNode = GetMethodNode(syntax);

            _triples.Add(new HasTriple(typeNode, methodNode));
            _triples.AddRange(WordTriples(methodNode));
        }

        private void GetHasTriple(PropertyDeclarationSyntax syntax)
        {
            TypeNode typeNode = GetTypeNode(typeDeclarationSyntax);
            IPropertySymbol propertySymbol =
                CSharpExtensions.GetDeclaredSymbol(_walkerOptions.DotnetOptions.SemanticModel, syntax)!;
            PropertyNode propertyNode = propertySymbol.CreatePropertyNode();

            _triples.Add(new HasTriple(typeNode, propertyNode));
            _triples.AddRange(WordTriples(propertyNode));
        }

        private void GetImplementationOfTriples(MethodDeclarationSyntax syntax)
        {
            MethodNode methodNode = GetMethodNode(syntax);
            IMethodSymbol methodSymbol =
                CSharpExtensions.GetDeclaredSymbol(_walkerOptions.DotnetOptions.SemanticModel, syntax)!;

            if (!methodSymbol.TryGetInterfaceMethodFromImplementation(_walkerOptions.DotnetOptions.SemanticModel,
                    out MethodNode interfaceMethodNode)) return;

            _triples.Add(new ImplementationOfTriple(methodNode, interfaceMethodNode));
        }
    }
}