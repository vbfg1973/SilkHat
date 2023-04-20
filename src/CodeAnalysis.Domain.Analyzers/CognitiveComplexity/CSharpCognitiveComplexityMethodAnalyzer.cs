using CodeAnalysis.Domain.Analyzers.Abstract;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAnalysis.Domain.Analyzers.CognitiveComplexity
{
    public class CSharpCognitiveComplexityMethodAnalyzer : IMethodAnalyzer
    {
        private readonly MethodDeclarationSyntax _element;
        private int _nesting;

        public string Name => _element
            .Identifier
            .ToString();

        public string ContainingClassName => _element.ContainingClass().Identifier.ToString();

        public string ContainingNamespace => _element.ContainingClass().ContainingNamespace().Name.ToString();

        public int ComplexityScore { get; private set; }

        public CSharpCognitiveComplexityMethodAnalyzer(MethodDeclarationSyntax element)
        {
            _element = element;
            _nesting = 0;
            ComplexityScore = 0;
        }
        
        public int Analyze(MethodDeclarationSyntax methodSyntax)
        {
            return AnalyzeStatements(methodSyntax.Body.Statements, 0);
        }

        private int AnalyzeStatements(SyntaxList<StatementSyntax> statements, int nesting)
        {
            throw new NotImplementedException();
        }
    }
}