using CodeAnalysis.Infrastructure.Git.Commands.Abstract;

namespace CodeAnalysis.Infrastructure.Git.Commands
{
    public interface IProcessCommandRunner
    {
        /// <summary>
        ///     Run the process making output immediately available through a IEnumerable of strings. The caller is
        ///     responsible for parsing this into intended output
        /// </summary>
        /// <param name="commandArguments"></param>
        /// <returns></returns>
        IEnumerable<string> Runner(AbstractCommandArguments commandArguments);
    }
}