using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SilkHat.Domain.CodeAnalysis.Abstract
{
    public interface ICodeWalkerFactory
    {
        ICodeWalker GetWalker(ClassDeclarationSyntax classDeclarationSyntax, WalkerOptions walkerOptions);
        ICodeWalker GetWalker(InterfaceDeclarationSyntax interfaceDeclarationSyntax, WalkerOptions walkerOptions);
        ICodeWalker GetWalker(MethodDeclarationSyntax methodDeclarationSyntax, WalkerOptions walkerOptions);
    }
}