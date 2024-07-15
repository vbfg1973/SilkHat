using Annytab.Stemmer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SilkHat.Domain.CodeAnalysis.Abstract;
using SilkHat.Domain.CodeAnalysis.Extensions;
using SilkHat.Domain.Common;
using SilkHat.Domain.Common.Locations;
using SilkHat.Domain.Graph.SemanticTriples;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;
using Location = SilkHat.Domain.Common.Locations.Location;

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

        protected HasLocationTriple? GetHasLocationTripleFromSyntaxNode(Node node, SyntaxNode syntaxNode)
        {
            string filePath = syntaxNode.SyntaxTree.FilePath;

            if (string.IsNullOrEmpty(filePath)) return null;
            
            Location location = GetLocationFromSyntaxNode(syntaxNode);
            return new HasLocationTriple(node, new LocationNode(filePath, location));
        }
        
        protected Location GetLocationFromSyntaxNode(SyntaxNode syntaxNode)
        {
            Microsoft.CodeAnalysis.Location location = syntaxNode.GetLocation();

            LocationPosition startLocationPosition = new LocationPosition(location.SourceSpan.Start, 0);
            LocationPosition endLocationPosition = new LocationPosition(location.SourceSpan.End, 0);

            return new Location(startLocationPosition, endLocationPosition);
        }
    }
}