using CodeAnalysis.Domain.Analyzers.Complexity.Abstract;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysis.Domain.Analyzers.Complexity.CognitiveComplexity
{
    public class CSharpCognitiveComplexityAnalyzer : CSharpSyntaxWalker, IDotnetComplexityAnalyzer
    {
        private readonly MethodDeclarationSyntax _methodDeclarationSyntax;
        private int _nesting;
        private IList<SyntaxNode> ToIgnore { get; } = new List<SyntaxNode>();


        public CSharpCognitiveComplexityAnalyzer(MethodDeclarationSyntax methodDeclarationSyntax)
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

        #region ForLoops

        public override void VisitForStatement(ForStatementSyntax node)
        {
            IncreaseComplexityByNesting(node.ForKeyword);
            VisitWithNesting(node, base.VisitForStatement);
        }

        #endregion

        #region WhileLoops

        public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            IncreaseComplexityByNesting(node.WhileKeyword);
            VisitWithNesting(node, base.VisitWhileStatement);
        }

        #endregion

        #region DoWhileLoops

        public override void VisitDoStatement(DoStatementSyntax node)
        {
            IncreaseComplexityByNesting(node.DoKeyword);
            VisitWithNesting(node, base.VisitDoStatement);
        }

        #endregion

        #region ForeachLoops

        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            IncreaseComplexityByNesting(node.ForEachKeyword);
            VisitWithNesting(node, base.VisitForEachStatement);
        }

        #endregion

        #region If/Else Conditions

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
            IncreaseComplexity(syntaxNode.ElseKeyword);
            base.VisitElseClause(syntaxNode);
        }

        #endregion

        #region Switch

        public override void VisitSwitchStatement(SwitchStatementSyntax node)
        {
            IncreaseComplexityByNesting(node.SwitchKeyword);
            VisitWithNesting(node, base.VisitSwitchStatement);
        }

        public override void VisitBreakStatement(BreakStatementSyntax node)
        {
            IncreaseComplexity(node.BreakKeyword);
            base.VisitBreakStatement(node);
        }

        #endregion

        #region Catch Clause

        public override void VisitCatchClause(CatchClauseSyntax node)
        {
            IncreaseComplexityByNesting(node.CatchKeyword);
            VisitWithNesting(node, base.VisitCatchClause);
        }

        #endregion

        #region Goto Statement (Harmful, etc)

        public override void VisitGotoStatement(GotoStatementSyntax node)
        {
            IncreaseComplexityByNesting(node.GotoKeyword);
            base.VisitGotoStatement(node);
        }

        #endregion

        #region Lambda

        public override void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node) =>
            VisitWithNesting(node, base.VisitSimpleLambdaExpression);

        public override void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node) =>
            VisitWithNesting(node, base.VisitParenthesizedLambdaExpression);

        #endregion

        #region Binary Expressions

        public override void VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            var nodeKind = node.Kind();
            if ((nodeKind is SyntaxKind.LogicalAndExpression or SyntaxKind.LogicalOrExpression) && !ToIgnore.Contains(node))
            {
                var left = node.Left.RemoveParentheses();
                if (!left.IsKind(nodeKind))
                {
                    IncreaseComplexity(node.OperatorToken);
                }

                var right = node.Right.RemoveParentheses();
                if (right.IsKind(nodeKind))
                {
                    ToIgnore.Add(right);
                }
            }

            base.VisitBinaryExpression(node);
        }

        #endregion

        #region Complexity modifiers

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

        #endregion
    }
}