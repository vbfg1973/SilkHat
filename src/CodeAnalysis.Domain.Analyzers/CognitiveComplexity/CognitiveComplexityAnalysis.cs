using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysis.Domain.Analyzers.CognitiveComplexity
{
    public static class CognitiveComplexityAnalysis
    {
        public class CognitiveComplexityAnalyzer
        {
            public static int AnalyzeMethod(MethodDeclarationSyntax methodSyntax)
            {
                return AnalyzeStatements(methodSyntax.Body.Statements, 0);
            }

            private static int AnalyzeStatements(SyntaxList<StatementSyntax> statements, int nesting)
            {
                var score = 0;

                foreach (var item in statements)
                    switch (item)
                    {
                        case IfStatementSyntax ifStatement:
                            score += AnalyzeIfStatement(ifStatement, nesting);
                            break;
                        case SwitchStatementSyntax switchStatement:
                            score += AnalyzeSwitchStatement(switchStatement, nesting);
                            break;
                        case TryStatementSyntax trySyntax:
                            score += AnalyzeTrySyntax(trySyntax, nesting);
                            break;
                        case WhileStatementSyntax whileSyntax:
                            score += AnalyzeWhileSyntax(whileSyntax, nesting);
                            break;
                        case DoStatementSyntax whileSyntax:
                            score += AnalyzeDoWhileSyntax(whileSyntax, nesting);
                            break;
                        case ForStatementSyntax forSyntax:
                            score += AnalyzeForSyntax(forSyntax, nesting);
                            break;
                        case ForEachStatementSyntax forEachSyntax:
                            score += AnalyzeForEachSyntax(forEachSyntax, nesting);
                            break;
                    }

                return score;
            }

            private static int AnalyzeForEachSyntax(ForEachStatementSyntax statement, int nesting)
            {
                var score = 1;

                score += statement.Expression.DescendantNodes().Count(t => t is InvocationExpressionSyntax);
                var nested = statement.DescendantNodes().OfType<StatementSyntax>();
                score += AnalyzeStatements(new SyntaxList<StatementSyntax>(nested), nesting + 1);

                return score;
            }

            private static int AnalyzeForSyntax(ForStatementSyntax statement, int nesting)
            {
                var score = 1;

                score += statement.Declaration.DescendantNodesAndSelf().Count(t => t is InvocationExpressionSyntax);

                score += statement.Incrementors.Count(t => t is InvocationExpressionSyntax);

                if (statement.Condition is BinaryExpressionSyntax bes)
                {
                    var leftNested = bes.Left.DescendantNodesAndSelf().OfType<InvocationExpressionSyntax>();
                    var rightNested = bes.Right.DescendantNodesAndSelf().OfType<InvocationExpressionSyntax>();
                    score += leftNested.Count() + rightNested.Count();
                }

                var nested = statement.DescendantNodes().OfType<StatementSyntax>();
                score += AnalyzeStatements(new SyntaxList<StatementSyntax>(nested), nesting + 1);

                return score;
            }

            private static int AnalyzeDoWhileSyntax(DoStatementSyntax statement, int nesting)
            {
                var score = 1;

                score += statement.Condition.ConditionComplexityScore();

                var nested = statement.DescendantNodes().OfType<StatementSyntax>();
                score += AnalyzeStatements(new SyntaxList<StatementSyntax>(nested), nesting + 1);

                return score;
            }

            private static int AnalyzeWhileSyntax(WhileStatementSyntax statement, int nesting)
            {
                var score = 1;

                score += statement.Condition.ConditionComplexityScore();

                var nested = statement.DescendantNodes().OfType<StatementSyntax>();
                score += AnalyzeStatements(new SyntaxList<StatementSyntax>(nested), nesting + 1);

                return score;
            }

            private static int AnalyzeTrySyntax(TryStatementSyntax trySyntax, int nesting)
            {
                var score = 1;

                score += trySyntax.Catches.Sum(catchClauseSyntax =>
                    AnalyzeStatements(catchClauseSyntax.Block.Statements, nesting));

                return score;
            }

            private static int AnalyzeSwitchStatement(SwitchStatementSyntax statement, int nesting)
            {
                var score = 1;

                var nested = statement.DescendantNodes().OfType<StatementSyntax>();

                if (nested.Any())
                    score += AnalyzeStatements(new SyntaxList<StatementSyntax>(nested), nesting);

                return score + nesting;
            }

            private static int AnalyzeIfStatement(IfStatementSyntax statement, int nesting)
            {
                var score = 1;

                var nested = statement.Statement.DescendantNodesAndSelf().OfType<StatementSyntax>();
                if (statement.Else != null)
                    nested = nested.Concat(statement.Else.Statement.DescendantNodesAndSelf().OfType<StatementSyntax>());

                if (nested.Any())
                    score += AnalyzeStatements(new SyntaxList<StatementSyntax>(nested), nesting + 1);

                score += statement.Condition.ConditionComplexityScore();

                return score + nesting;
            }
        }
    }
}