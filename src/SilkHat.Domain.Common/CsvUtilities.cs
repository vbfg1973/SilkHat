using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace SilkHat.Domain.Common
{
    public static class CsvUtilities
    {
        public static async Task WriteCsvAsync<T>(string fileName, IEnumerable<T> records)
        {
            await using StreamWriter stream = new(fileName);
            await using CsvWriter csvWriter = new(stream, CultureInfo.InvariantCulture);
            await csvWriter.WriteRecordsAsync(records);
        }

        public static async Task WriteCsvAsync<T>(string fileName, IEnumerable<T> records, ClassMap<T> classMap)
        {
            await using StreamWriter stream = new(fileName);
            await using CsvWriter csvWriter = new(stream, CultureInfo.InvariantCulture);
            csvWriter.Context.RegisterClassMap(classMap);
            await csvWriter.WriteRecordsAsync(records);
        }
    }
}