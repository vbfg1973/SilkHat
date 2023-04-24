using System.Globalization;
using CsvHelper;

namespace CodeAnalysis.Domain.Extensions
{
    /// <summary>
    ///     Helpers for reading, writing and generating CSV files
    /// </summary>
    public static class CsvUtilities
    {
        /// <summary>
        ///     Writes generic collections to a CSV file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="records"></param>
        /// <typeparam name="T"></typeparam>
        public static async Task CsvWriteAsync<T>(string path, IEnumerable<T> records)
        {
            await using var writer = new StreamWriter(path);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(records);
        }
    }
}