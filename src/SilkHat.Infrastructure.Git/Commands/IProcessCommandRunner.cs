using SilkHat.Infrastructure.Git.Commands.Abstract;

namespace SilkHat.Infrastructure.Git.Commands
{
    public interface IProcessCommandRunner
    {
        /// <summary>
        ///     Run the process making output immediately available through a IEnumerable of strings. The caller is
        ///     responsible for parsing this into intended output
        /// </summary>
        /// <param name="commandLineArguments"></param>
        /// <returns></returns>
        IEnumerable<string> Runner(AbstractCommandLineArguments commandLineArguments);
    }
}