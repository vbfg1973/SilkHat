using CodeAnalysis.Domain.Analyzers.Complexity.Abstract;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace CodeAnalysis.Domain.Analyzers.Complexity.CognitiveComplexity
{
    public class VisualBasicCognitiveComplexityAnalyzer : VisualBasicSyntaxWalker, IComplexityAnalyzer
    {
        private readonly MethodBlockSyntax _methodBlockSyntax;
        private int _nesting;

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

        public Location Location { get; }
        public List<Location> Locations { get; }

        public sealed override void Visit(SyntaxNode node)
        {
            base.Visit(node);
        }

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

        #endregion
        
        #region  ForeachLoops

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
    }
}