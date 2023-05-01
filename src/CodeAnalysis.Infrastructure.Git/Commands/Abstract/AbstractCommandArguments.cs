namespace CodeAnalysis.Infrastructure.Git.Commands.Abstract
{
    public abstract record AbstractCommandArguments
    {
        protected AbstractCommandArguments()
        {
            Arguments = new List<string>();
        }

        public string FileName { get; init; } = null!;
        public List<string> Arguments { get; init; }
    }
}