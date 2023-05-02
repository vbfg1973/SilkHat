using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using SilkHat.Infrastructure.Git.Commands.Abstract;

namespace SilkHat.Infrastructure.Git.Commands
{
    /// <summary>
    ///     Command runner for shelling external processes and reading back their output
    /// </summary>
    public sealed class ProcessCommandRunner : IProcessCommandRunner
    {
        private readonly Process _process;

        public ProcessCommandRunner()
        {
            _process = new Process();
            _process.EnableRaisingEvents = true;
        }

        /// <summary>
        ///     Run the process making output immediately available through a IEnumerable of strings. The caller is
        ///     responsible for parsing this into intended output
        /// </summary>
        /// <param name="commandLineArguments"></param>
        /// <returns></returns>
        public IEnumerable<string> Runner(AbstractCommandLineArguments commandLineArguments)
        {
            _process.StartInfo = BuildStartInfo(commandLineArguments);

            var blockingCollection = new BlockingCollection<string>();

            _process.OutputDataReceived += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(ev.Data)) return;
                blockingCollection.Add(NormaliseLineEnding(ev.Data));
            };

            _process.Exited += (s, e) => { blockingCollection.CompleteAdding(); };

            _process.Start();
            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine(); // Currently doing nowt with this

            return blockingCollection.GetConsumingEnumerable();
        }

        /// <summary>
        ///     Create a standard ProcessStartInfo object suitable for all window-less processes
        /// </summary>
        /// <param name="commandLineArguments"></param>
        /// <returns></returns>
        private static ProcessStartInfo BuildStartInfo(AbstractCommandLineArguments commandLineArguments)
        {
            return new ProcessStartInfo
            {
                FileName = commandLineArguments.FileName,
                Arguments = BuildArguments(commandLineArguments),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
        }

        /// <summary>
        ///     Build an argument string from passed in arguments object
        /// </summary>
        /// <param name="commandLineArguments"></param>
        /// <returns></returns>
        private static string BuildArguments(AbstractCommandLineArguments commandLineArguments)
        {
            var stringBuilder = new StringBuilder();

            foreach (var argument in commandLineArguments.Arguments) stringBuilder.Append($"{argument} ");

            return stringBuilder.ToString().TrimEnd();
        }

        /// <summary>
        ///     Strip carriage returns leaving only newlines
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string NormaliseLineEnding(string str)
        {
            return str.ReplaceLineEndings("\n");
        }
    }
}