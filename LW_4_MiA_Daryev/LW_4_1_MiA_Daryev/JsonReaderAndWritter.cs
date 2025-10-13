using System.Text.Json;
namespace LW_4_1_MiA_Daryev_JSONFILES
{
    public static class JsonReaderAndWritter<T>
    {
        public static List<T> ReadJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            string jsonContent = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(jsonContent) ?? new List<T>();
        }
        public static void WriteJsonFile(string filePath, List<T> data)
        {
            string jsonContent = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonContent);
        }
    }
}

