using System.Globalization;
using CsvHelper;

namespace CodeAnalysis.Domain.Extensions
{
    public static class CsvUtilities
    {
        public static async Task CsvWriteAsync<T>(string path, IEnumerable<T> records)
        {
            await using var writer = new StreamWriter(path);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(records);
        } 
    }
}