using CodeAnalysis.Domain.Analyzers.Abstract;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysis.Domain.Analyzers.CognitiveComplexity
{
    public class CSharpCognitiveComplexityMethodAnalyzer : CSharpSyntaxWalker, IMethodAnalyzer
    {
        private int _nesting;
        private readonly MethodDeclarationSyntax _methodDeclarationSyntax;

        public CSharpCognitiveComplexityMethodAnalyzer(MethodDeclarationSyntax methodDeclarationSyntax)
        {
            _nesting = 0;
            _methodDeclarationSyntax = methodDeclarationSyntax;
            Locations = new List<Location>();
            Visit(_methodDeclarationSyntax);
        }

        public string Name => _methodDeclarationSyntax.Identifier.ToString();

        public string ContainingClassName => _methodDeclarationSyntax
            .Ancestors()
            .OfType<ClassDeclarationSyntax>()
            .First()
            .Identifier
            .ToString();

        public string ContainingNamespace => _methodDeclarationSyntax
            .Ancestors()
            .OfType<NamespaceDeclarationSyntax>()
            .First()
            .Name
            .ToString();

        public int ComplexityScore { get; private set; }
        public Location Location => _methodDeclarationSyntax.GetLocation();
        public List<Location> Locations { get; }

        public sealed override void Visit(SyntaxNode? node)
        {
            // If Local Function

            // else if switch

            // else if binary pattern

            // else 
            base.Visit(node);
        }

        public override void VisitIfStatement(IfStatementSyntax syntaxNode)
        {
            if (syntaxNode.Parent.IsKind(SyntaxKind.ElseClause))
            {
                base.VisitIfStatement(syntaxNode);
            }
            else
            {
                IncreaseComplexityByNesting(syntaxNode.IfKeyword);
                VisitWithNesting(syntaxNode, base.VisitIfStatement);
            }
        }

        public override void VisitElseClause(ElseClauseSyntax syntaxNode)
        {
            IncreaseComplexity(syntaxNode.ElseKeyword, 1);
            base.VisitElseClause(syntaxNode);
        }

        private void IncreaseComplexity(SyntaxToken syntaxToken, int increment = 1)
        {
            ComplexityScore += increment;
            Locations.Add(syntaxToken.GetLocation());
        }

        private void IncreaseComplexityByNesting(SyntaxToken syntaxToken)
        {
            IncreaseComplexity(syntaxToken, _nesting + 1);
        }

        private void VisitWithNesting<TSyntaxNode>(TSyntaxNode syntaxNode, Action<TSyntaxNode> visitMethod)
        {
            _nesting++;
            visitMethod(syntaxNode);
            _nesting--;
        }
    }
}