using Annytab.Stemmer;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SilkHat.Domain.CodeAnalysis.Abstract;
using SilkHat.Domain.CodeAnalysis.Extensions;
using SilkHat.Domain.Common;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.CodeAnalysis.Walkers.CSharp
{
    public abstract class CSharpBaseTypeWalker(WalkerOptions walkerOptions) : CSharpSyntaxWalker
    {
        private readonly IStemmer _stemmer = new EnglishStemmer();
        protected readonly WalkerOptions _walkerOptions = walkerOptions;

        protected TypeNode GetTypeNode(TypeDeclarationSyntax typeDeclarationSyntax)
        {
            return _walkerOptions
                .DotnetOptions
                .SemanticModel
                .GetDeclaredSymbol(typeDeclarationSyntax)!
                .CreateTypeNode(typeDeclarationSyntax);
        }

        protected MethodNode GetMethodNode(MethodDeclarationSyntax methodDeclarationSyntax)
        {
            return _walkerOptions
                .DotnetOptions
                .SemanticModel
                .GetDeclaredSymbol(methodDeclarationSyntax)!
                .CreateMethodNode();
        }

        protected PropertyNode GetPropertyNode(PropertyDeclarationSyntax propertyDeclarationSyntax)
        {
            return _walkerOptions
                .DotnetOptions
                .SemanticModel
                .GetDeclaredSymbol(propertyDeclarationSyntax)!
                .CreatePropertyNode();
        }

        protected IEnumerable<Triple> WordTriples(CodeNode node)
        {
            IEnumerable<string> words = node.Name.SplitStringOnCapitals();

            foreach (string word in words.Select(w => w.ToLower()))
            {
                WordNode wordNode = new(word, word);
                yield return new UsesWordTriple(node, wordNode);

                string root = (_stemmer.GetSteamWord(word) ?? word).ToLower();
                yield return new WordDerivationTriple(wordNode, new WordRootNode(root, root));
            }
        }
    }
}