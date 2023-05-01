namespace CodeAnalysis.Infrastructure.Git.Commands.Abstract
{
    public abstract record AbstractGitArgument : AbstractCommandArguments
    {
        protected AbstractGitArgument()
        {
            FileName = "git";
        }
    }
}