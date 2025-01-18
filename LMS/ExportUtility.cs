using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LMS
{
    public static class ExportUtility
    {
        public static async Task ExportCollectionToCSVAsync<T>(IEnumerable<T> collection, string fileName)
        {
            if (collection == null || !collection.Any())
            {
                throw new ArgumentException("The collection is empty or null.");
            }
            //fileName = GetUniqueFileName(fileName);

            try
            {
                using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    var properties = typeof(T).GetProperties()
                                            .Where(p => p.GetCustomAttribute<JsonIgnoreAttribute>() == null) // Exclude properties marked with [JsonIgnore]
                                            .ToList();
                    // Write headers
                    var headers = properties.Select(p => p.Name);
                    writer.WriteLine(string.Join(",", headers));

                    // Write rows asynchronously
                    foreach (var item in collection)
                    {
                        var row = properties.Select(p => p.GetValue(item)?.ToString() ?? string.Empty);
                        await writer.WriteLineAsync(string.Join(",", row));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error exporting data: {ex.Message}", ex);
            }
        }

        //private static string GetUniqueFileName(string fileName)
        //{
        //    string directory = Path.GetDirectoryName(fileName) ?? string.Empty;
        //    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        //    string extension = Path.GetExtension(fileName);

        //    int count = 1;
        //    while (File.Exists(fileName))
        //    {
        //        fileName = Path.Combine(directory, $"{fileNameWithoutExtension}({count}){extension}");
        //        count++;
        //    }
        //    return fileName;
        //}

    }
}
