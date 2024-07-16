using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.Abstract;
using SilkHat.Domain.CodeAnalysis.Extensions;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;
using SilkHat.Domain.Graph.SemanticTriples.Triples;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.CodeAnalysis.Walkers.CSharp
{
    public class CSharpTypeDiscoveryWalker(
        FileNode fileNode,
        ProjectNode projectNode,
        WalkerOptions walkerOptions)
        : CSharpBaseTypeWalker(walkerOptions), ICodeWalker
    {
        private readonly ProjectNode _projectNode = projectNode;
        private readonly List<Triple> _triples = new();

        public IEnumerable<Triple> Walk()
        {
            base.Visit(walkerOptions.DotnetOptions.SyntaxTree.GetRoot());

            return _triples;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax syntax)
        {
            GetTypeDeclarationTriples(syntax);

            SubWalkers(syntax);

            base.VisitClassDeclaration(syntax);
        }


        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax syntax)
        {
            GetTypeDeclarationTriples(syntax);

            SubWalkers(syntax);

            base.VisitInterfaceDeclaration(syntax);
        }

        public override void VisitRecordDeclaration(RecordDeclarationSyntax syntax)
        {
            GetTypeDeclarationTriples(syntax);

            SubWalkers(syntax);

            base.VisitRecordDeclaration(syntax);
        }

        // public override void VisitStructDeclaration(StructDeclarationSyntax node)
        // {
        //     GetTypeDeclarationTriples(node);
        //
        //     base.VisitStructDeclaration(node);
        // }

        private void GetTypeDeclarationTriples(TypeDeclarationSyntax syntax)
        {
            TypeNode typeNode = GetTypeNode(syntax);

            HasLocationTriple? hasLocationTriple = GetHasLocationTripleFromSyntaxNode(typeNode, syntax);
            if (hasLocationTriple != null)
            {
                _triples.Add(hasLocationTriple);
            }
            
            _triples.Add(new DeclaredAtTriple(typeNode, fileNode));
            _triples.Add(new BelongsToTriple(typeNode, _projectNode));
            _triples.AddRange(syntax.GetInherits(typeNode, walkerOptions.DotnetOptions.SemanticModel));
            _triples.AddRange(WordTriples(typeNode));
        }

        private void SubWalkers(TypeDeclarationSyntax syntax)
        {
            if (!_walkerOptions.DescendIntoSubWalkers) return;

            CSharpTypeDefinitionWalker typeDefinitionWalker = new(syntax, _walkerOptions);
            _triples.AddRange(typeDefinitionWalker.Walk());

            CSharpMethodInvocationWalker methodInvocationWalker = new(syntax, _walkerOptions);
            _triples.AddRange(methodInvocationWalker.Walk());
        }
    }
}