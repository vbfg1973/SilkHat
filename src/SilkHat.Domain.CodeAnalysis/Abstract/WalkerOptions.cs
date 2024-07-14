namespace SilkHat.Domain.CodeAnalysis.Abstract
{
    public class WalkerOptions(
        DotnetOptions dotnetOptions,
        bool descendIntoSubWalkers = false)
    {
        public DotnetOptions DotnetOptions { get; } = dotnetOptions;

        // public ICodeWalkerFactory CodeWalkerFactory { get; } = codeWalkerFactory;
        // public ILoggerFactory LoggerFactory { get; } = loggerFactory;
        public bool DescendIntoSubWalkers { get; } = descendIntoSubWalkers;
    }
}