using CodeAnalysis.Domain.Analyzers.Complexity.Abstract;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace CodeAnalysis.Domain.Analyzers.Complexity.CognitiveComplexity
{
    public class VisualBasicCognitiveComplexityAnalyzer : VisualBasicSyntaxWalker, IDotnetComplexityAnalyzer
    {
        private readonly MethodBlockSyntax _methodBlockSyntax;
        private int _nesting;
        private IList<SyntaxNode> ToIgnore { get; } = new List<SyntaxNode>();

        public VisualBasicCognitiveComplexityAnalyzer(MethodBlockSyntax methodBlockSyntax)
        {
            _methodBlockSyntax = methodBlockSyntax;
            Locations = new List<Location>();
            Visit(_methodBlockSyntax);
        }

        public string Name => _methodBlockSyntax
            .SubOrFunctionStatement
            .Identifier
            .ToString();

        public string ContainingClassName => _methodBlockSyntax
            .Ancestors()
            .OfType<ClassBlockSyntax>()
            .First()
            .ClassStatement
            .Identifier
            .ToString();

        public string ContainingNamespace => _methodBlockSyntax
            .Ancestors()
            .OfType<NamespaceBlockSyntax>()
            .First()
            .NamespaceStatement
            .Name
            .ToString();

        public int ComplexityScore { get; private set; }

        public Location Location => _methodBlockSyntax.GetLocation();
        public List<Location> Locations { get; }

        public sealed override void Visit(SyntaxNode node)
        {
            base.Visit(node);
        }

        #region ForeachLoops

        public override void VisitForEachBlock(ForEachBlockSyntax node)
        {
            IncreaseComplexityByNesting(node.ForEachStatement.ForKeyword);
            VisitWithNesting(node, base.VisitForEachBlock);
        }

        #endregion

        #region For Loops

        public override void VisitForBlock(ForBlockSyntax node)
        {
            IncreaseComplexityByNesting(node.ForStatement.ForKeyword);
            VisitWithNesting(node, base.VisitForBlock);
        }

        #endregion

        #region While Loops

        public override void VisitWhileBlock(WhileBlockSyntax node)
        {
            IncreaseComplexityByNesting(node.WhileStatement.WhileKeyword);
            VisitWithNesting(node, base.VisitWhileBlock);
        }

        #endregion

        #region DoWhile Loops

        public override void VisitDoLoopBlock(DoLoopBlockSyntax node)
        {
            IncreaseComplexityByNesting(node.DoStatement.DoKeyword);
            VisitWithNesting(node, base.VisitDoLoopBlock);
        }

        #endregion

        #region Switch

        public override void VisitSelectBlock(SelectBlockSyntax node)
        {
            IncreaseComplexityByNesting(node.SelectStatement.SelectKeyword);
            VisitWithNesting(node, base.VisitSelectBlock);
        }

        public override void VisitCaseBlock(CaseBlockSyntax node)
        {
            IncreaseComplexity(node.CaseStatement.CaseKeyword);
            base.VisitCaseBlock(node);
        }

        #endregion

        #region Catch Clause

        public override void VisitCatchBlock(CatchBlockSyntax node)
        {
            IncreaseComplexityByNesting(node.CatchStatement.CatchKeyword);
            VisitWithNesting(node, base.VisitCatchBlock);
        }

        #endregion
        
        #region Goto (Considered harmful, ho ho ho)

        public override void VisitGoToStatement(GoToStatementSyntax node)
        {
            IncreaseComplexityByNesting(node.GoToKeyword);
            base.VisitGoToStatement(node);
        }

        #endregion

        #region Lambda

        public override void VisitSingleLineLambdaExpression(SingleLineLambdaExpressionSyntax node) =>
            VisitWithNesting(node, base.VisitSingleLineLambdaExpression);

        public override void VisitMultiLineLambdaExpression(MultiLineLambdaExpressionSyntax node) =>
            VisitWithNesting(node, base.VisitMultiLineLambdaExpression);

        #endregion
        
        #region Binary Expressions

        public override void VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            var nodeKind = node.Kind();

            if (!ToIgnore.Contains(node) && (nodeKind is SyntaxKind.AndExpression or SyntaxKind.AndAlsoExpression
                    or SyntaxKind.OrExpression or SyntaxKind.OrElseExpression) )
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
        
        public override void VisitBinaryConditionalExpression(BinaryConditionalExpressionSyntax node)
        {
            IncreaseComplexity(node.IfKeyword);
            VisitWithNesting(node, base.VisitBinaryConditionalExpression);
        }

        #endregion
        
        #region Complexity Modifiers

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

        #region If/Else Conditions

        public override void VisitSingleLineIfStatement(SingleLineIfStatementSyntax node)
        {
            IncreaseComplexity(node.IfKeyword, _nesting + 1);
            VisitWithNesting(node, base.VisitSingleLineIfStatement);
        }

        public override void VisitMultiLineIfBlock(MultiLineIfBlockSyntax node)
        {
            IncreaseComplexity(node.IfStatement.IfKeyword, _nesting + 1);
            VisitWithNesting(node, base.VisitMultiLineIfBlock);
        }

        public override void VisitElseIfStatement(ElseIfStatementSyntax node)
        {
            IncreaseComplexity(node.ElseIfKeyword);
            base.VisitElseIfStatement(node);
        }

        public override void VisitElseStatement(ElseStatementSyntax node)
        {
            IncreaseComplexity(node.ElseKeyword);
            base.VisitElseStatement(node);
        }
        
        public override void VisitTernaryConditionalExpression(TernaryConditionalExpressionSyntax node)
        {
            IncreaseComplexityByNesting(node.IfKeyword);
            VisitWithNesting(node, base.VisitTernaryConditionalExpression);
        }

        #endregion
    }
}