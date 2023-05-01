namespace SilkHat.Infrastructure.Git.Commands.Abstract
{
    public abstract record AbstractGitArgument : AbstractCommandArguments
    {
        protected AbstractGitArgument()
        {
            FileName = "git";
        }
    }
}