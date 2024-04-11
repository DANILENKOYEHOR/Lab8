using System;
using System.IO;
using System.Text.Json;

public class DataSerializer
{
    public static void SerializeToJson<T>(T data, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(data, options);
        File.WriteAllText(filePath, jsonString);
    }

    public static T DeserializeFromJson<T>(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(jsonString);
    }
}