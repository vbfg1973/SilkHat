using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using CodeAnalysis.Infrastructure.Git.Commands.Abstract;

namespace CodeAnalysis.Infrastructure.Git.Commands
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
        /// <param name="commandArguments"></param>
        /// <returns></returns>
        public IEnumerable<string> Runner(AbstractCommandArguments commandArguments)
        {
            _process.StartInfo = BuildStartInfo(commandArguments);

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
        /// <param name="commandArguments"></param>
        /// <returns></returns>
        private static ProcessStartInfo BuildStartInfo(AbstractCommandArguments commandArguments)
        {
            return new ProcessStartInfo
            {
                FileName = commandArguments.FileName,
                Arguments = BuildArguments(commandArguments),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
        }

        /// <summary>
        ///     Build an argument string from passed in arguments object
        /// </summary>
        /// <param name="commandArguments"></param>
        /// <returns></returns>
        private static string BuildArguments(AbstractCommandArguments commandArguments)
        {
            var stringBuilder = new StringBuilder();

            foreach (var argument in commandArguments.Arguments) stringBuilder.Append($"{argument} ");

            return stringBuilder.ToString().TrimEnd();
        }

        /// <summary>
        ///     Strip carriage returns leaving only newlines
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string NormaliseLineEnding(string str)
        {
            return str.Replace("\\r", "");
        }
    }
}