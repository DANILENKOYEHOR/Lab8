using System.Collections.Generic;
using System.IO;

public class BinaryDataSerializer
{
    public static void SerializeToBinary<T>(T data, string filePath)
    {
        using (var binaryWriter = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            foreach (var entry in (Dictionary<string, int>)(object)data)
            {
                binaryWriter.Write(entry.Key);
                binaryWriter.Write(entry.Value);
            }
        }
    }

    public static Dictionary<string, int> DeserializeFromBinary(string filePath)
    {
        var data = new Dictionary<string, int>();

        using (var binaryReader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                string key = binaryReader.ReadString();
                int value = binaryReader.ReadInt32();
                data.Add(key, value);
            }
        }

        return data;
    }
}