using CodeAnalysis.Domain.Analyzers.Complexity.IndentationComplexity;
using FluentAssertions;

namespace CodeAnalysis.Domain.Analyzers.Complexity.Tests.IndentationComplexity
{
    public class IndentationComplexityTests
    {
        [Theory]
        [InlineData("BinaryExpressionClass.CSharp", 120)]
        [InlineData("BinaryExpressionClass.VisualBasic", 92)]
        
        [InlineData("CatchClass.CSharp", 500)]
        [InlineData("CatchClass.VisualBasic", 316)]
        
        [InlineData("DoWhileClass.CSharp", 1032)]
        [InlineData("DoWhileClass.VisualBasic", 728)]
        
        [InlineData("ForClass.CSharp", 1032)]
        [InlineData("ForClass.VisualBasic", 728)]
        
        [InlineData("ForeachClass.CSharp", 1032)]
        [InlineData("ForeachClass.VisualBasic", 728)]
        
        [InlineData("GotoClass.CSharp", 72)]
        [InlineData("GotoClass.VisualBasic", 64)]
        
        [InlineData("IfElseClass.CSharp", 1476)]
        [InlineData("IfElseClass.VisualBasic", 1060)]
        
        [InlineData("LambdaClass.CSharp", 204)]
        [InlineData("LambdaClass.VisualBasic", 251)]
        
        [InlineData("MethodClass.CSharp", 120)]
        [InlineData("MethodClass.VisualBasic", 92)]
        
        [InlineData("SwitchClass.CSharp", 1444)]
        [InlineData("SwitchClass.VisualBasic", 708)]
        
        [InlineData("WhileClass.CSharp", 1032)]
        [InlineData("WhileClass.VisualBasic", 728)]
        public void GivenSourceCodeFileCorrectComplexityScore(string fileName, int expectedComplexityScore)
        {
            var classData = ReadWholeTestClass(fileName);
            var analyzer = new IndentationComplexityAnalyzer(classData);

            analyzer
                .ComplexityScore
                .Should()
                .Be(expectedComplexityScore);
        }

        private string ReadWholeTestClass(string fileName)
        {
            return File.ReadAllText(Path.Combine("TestClasses", fileName));
        }
    }
}