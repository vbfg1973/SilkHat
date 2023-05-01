using CodeAnalysis.Infrastructure.Git.Commands;
using CodeAnalysis.Infrastructure.Git.Commands.Abstract;

namespace CodeAnalysis.Infrastructure.Git.Tests.Helpers
{
    public class FileReaderProcessRunner : IProcessCommandRunner
    {
        private readonly string _path;

        public FileReaderProcessRunner(string path)
        {
            _path = path;
        }

        public IEnumerable<string> Runner(AbstractCommandArguments commandArguments)
        {
            return File.ReadLines(_path).Select(NormaliseLineEndings);
        }

        private static string NormaliseLineEndings(string str)
        {
            return str.Replace("\\r", "");
        }
    }
}