namespace CodeAnalysis.Domain.Extensions
{
    /// <summary>
    ///     Helper class for finding files
    /// </summary>
    public static class FileUtilities
    {
        /// <summary>
        ///     Finds file paths with matching extensions
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public static IEnumerable<string> FindFiles(string directoryPath, string[] extensions)
        {
            var paths = FindFiles(directoryPath).ToList();

            foreach (var filePath in FindFiles(directoryPath))
            {
                var fileName = Path.GetFileName(filePath);

                if (extensions.Contains(Path.GetExtension(fileName))) yield return filePath;
            }
        }

        /// <summary>
        ///     Finds all file paths
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static IEnumerable<string> FindFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath)) yield break;

            foreach (var file in Directory.GetFiles(directoryPath)) yield return file;

            foreach (var directory in Directory.GetDirectories(directoryPath))
            foreach (var file in FindFiles(directory))
                yield return file;
        }

        /// <summary>
        ///     Read a file with carriage return characters stripped
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<string> ReadNormalisedFile(string path)
        {
            var fileContents = await File.ReadAllTextAsync(path);

            return fileContents.NormaliseLineEndings();
        }
    }
}