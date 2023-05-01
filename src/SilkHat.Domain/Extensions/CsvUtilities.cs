using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace SilkHat.Domain.Extensions
{
    /// <summary>
    ///     Helpers for reading, writing and generating CSV files
    /// </summary>
    public static class CsvUtilities
    {
        private static readonly TypeConverterOptions Options = new TypeConverterOptions { Formats = new[] { "O" } };
        
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
            csv.RegisterTypeConversions();
            await csv.WriteRecordsAsync(records);
        }

        /// <summary>
        ///     Writes generic collections to a CSV file mapped via classmap
        /// </summary>
        /// <param name="path"></param>
        /// <param name="records"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TClassMap"></typeparam>
        public static async Task CsvWriteAsync<T, TClassMap>(string path, IEnumerable<T> records) where TClassMap : ClassMap<T>
        {
            await using var writer = new StreamWriter(path);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<TClassMap>();
            csv.RegisterTypeConversions();

            await csv.WriteRecordsAsync(records);
        }

        private static void RegisterTypeConversions(this CsvWriter csv)
        {
            csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(Options);
            csv.Context.TypeConverterOptionsCache.AddOptions<DateTimeOffset>(Options);            
        }
        
        private static void RegisterTypeConversions(this CsvReader csv)
        {
            csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(Options);
            csv.Context.TypeConverterOptionsCache.AddOptions<DateTimeOffset>(Options);            
        }
    }
}