using System.Text.Json;
using System.Text.Json.Serialization;

public static class JsonReaderAndWriter<T>
{
    public static List<T> ReadJsonFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        var jsonContent = File.ReadAllText(filePath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        return JsonSerializer.Deserialize<List<T>>(jsonContent, options)
               ?? new List<T>();
    }

    public static void WriteJsonFile(string filePath, List<T> data)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var jsonContent = JsonSerializer.Serialize(data, options);
        File.WriteAllText(filePath, jsonContent);
    }
}
