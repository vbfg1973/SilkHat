using System.Text.RegularExpressions;

namespace CodeAnalysis.Domain.Extensions
{
    public static class FileUtilities
    {
        public static IEnumerable<string> FindFiles(string directoryPath, string[] extensions)
        {
            var paths = FindFiles(directoryPath).ToList();

            foreach (var filePath in FindFiles(directoryPath))
            {
                var fileName = Path.GetFileName(filePath);

                if (extensions.Contains(Path.GetExtension(fileName)))
                {
                    yield return filePath;
                }
            }
        }

        private static IEnumerable<string> FindFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath)) yield break;

            foreach (var file in Directory.GetFiles(directoryPath)) yield return file;

            foreach (var directory in Directory.GetDirectories(directoryPath))
            foreach (var file in FindFiles(directory))
                yield return file;
        }
    }
}