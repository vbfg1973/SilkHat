namespace SilkHat.Infrastructure.Git.Commands.Abstract
{
    public abstract record AbstractGitCommandLineArguments : AbstractCommandLineArguments
    {
        protected AbstractGitCommandLineArguments()
        {
            FileName = "git";
        }
    }
}