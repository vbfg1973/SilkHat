namespace SilkHat.Infrastructure.Git.Tests.CommandTests.GitCommitHelperTests.Data.Abstract
{
    public abstract class BaseGitCommitHelperClassData
    {
        private readonly List<string> _dirPath = new()
        {
            "Resources",
            "GitCommitHelperExamples"
        };

        protected IEnumerable<string> ReadAllFilesExcept(string fileToSkip)
        {
            return Files()
                .Where(file => !file.EndsWith(fileToSkip))
                .SelectMany(ReadFile);
        }

        protected IEnumerable<string> ReadFile(string fileName)
        {
            return File.ReadAllLines(Path.Combine(GetDataPath(), fileName));
        }

        protected IEnumerable<string> Files()
        {
            return Directory
                .EnumerateFiles(GetDataPath())
                .Select(Path.GetFileName)!;
        }

        private string GetDataPath()
        {
            return Path.Combine(_dirPath.ToArray());
        }
    }
}