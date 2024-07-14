using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.Abstract;
using SilkHat.Domain.CodeAnalysis.Extensions;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;
using SilkHat.Domain.Graph.TripleDefinitions.Triples;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

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

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            GetTypeDeclarationTriples(node);

            SubWalkers(node);

            base.VisitClassDeclaration(node);
        }


        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            GetTypeDeclarationTriples(node);

            SubWalkers(node);

            base.VisitInterfaceDeclaration(node);
        }

        public override void VisitRecordDeclaration(RecordDeclarationSyntax node)
        {
            GetTypeDeclarationTriples(node);

            SubWalkers(node);

            base.VisitRecordDeclaration(node);
        }

        // public override void VisitStructDeclaration(StructDeclarationSyntax node)
        // {
        //     GetTypeDeclarationTriples(node);
        //
        //     base.VisitStructDeclaration(node);
        // }

        private void GetTypeDeclarationTriples(TypeDeclarationSyntax node)
        {
            TypeNode typeNode = GetTypeNode(node);

            _triples.Add(new DeclaredAtTriple(typeNode, fileNode));
            _triples.Add(new BelongsToTriple(typeNode, _projectNode));
            _triples.AddRange(node.GetInherits(typeNode, walkerOptions.DotnetOptions.SemanticModel));
            _triples.AddRange(WordTriples(typeNode));
        }

        private void SubWalkers(TypeDeclarationSyntax node)
        {
            if (!_walkerOptions.DescendIntoSubWalkers) return;

            CSharpTypeDefinitionWalker typeDefinitionWalker = new(node, _walkerOptions);
            _triples.AddRange(typeDefinitionWalker.Walk());

            CSharpMethodInvocationWalker methodInvocationWalker = new(node, _walkerOptions);
            _triples.AddRange(methodInvocationWalker.Walk());
        }
    }
}