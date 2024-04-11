using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/*
 * (!) Регистр не важен
 * (!) Знаки пунктуцаии не являются частью слов
 */

using System.Text;

internal class Program
{
    static void Main(string[] args)
    {
        string text1 =
            @"Создайте приложение для управления списком покупок. Реализуйте операции добавления, удаления и просмотра товаров в списке.
Создайте систему для организации мероприятий. Разработайте список мероприятий и позвольте пользователю добавлять, удалять и просматривать мероприятия.
Создайте игру, в которой программа случайным образом выбирает число, а пользователь должен угадать его. Используйте список для сохранения попыток пользователя.";
        string text2 = @"Напишите программу, которая принимает ввод от пользователя и сохраняет уникальные элементы в HashSet.
Реализуйте приложение для учета фильмов, просмотренных пользователями. Используйте HashSet для хранения уникальных фильмов.
Разработайте приложение для хранения контактов с использованием словаря. Позвольте пользователю добавлять, удалять и искать контакты по имени.";
        //Console.WriteLine(text1);
        //Console.WriteLine(text2);

        text1 = text1.ToLower();
        text2 = text2.ToLower();

        string text1_noPunct = ClearPunctuation(text1);
        //Console.WriteLine(text1_noPunct);
        string text2_NoPunct = ClearPunctuation(text2);
        //Console.WriteLine(text2_NoPunct);

        string[] t1_words = GetWords(text1_noPunct);
        //Console.WriteLine(t1_words.Length);
        string[] t2_words = GetWords(text2_NoPunct);
        //Console.WriteLine(t2_words.Length);

        Dictionary<string, int> first_t = WordFreq(t1_words);
        
        Dictionary<string, int> second_t = WordFreq(t2_words);
        
        // результаты для первого текста
        /*Console.WriteLine("\nЧастота слов в первом тексте:");
        foreach (var pair in first_t)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }*/

        //  результаты для второго текста
        /*Console.WriteLine("\nЧастота слов во втором тексте:");
        foreach (var pair in second_t)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
        */
        
        var filePath = "data.json";
        DataSerializer.SerializeToJson(first_t, filePath);
        var restoredData = DataSerializer.DeserializeFromJson<Dictionary<string, int>>(filePath);
        
        foreach (var pair in restoredData)
        {
            Console.WriteLine($"Word: {pair.Key}, Frequency: {pair.Value}");
        }

        var binaryFilePath = "data.bin";
        BinaryDataSerializer.SerializeToBinary(first_t, binaryFilePath);
        var restoredBinaryData = BinaryDataSerializer.DeserializeFromBinary(binaryFilePath);
        Console.WriteLine("\nИз бинарного файла:\n");
        foreach (var pair in restoredBinaryData)
        {
            Console.WriteLine($"Word: {pair.Key}, Frequency: {pair.Value}");
        }

        
    }

    
    
    static string ClearPunctuation(string src)
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < src.Length; i++)
        {
            if (!char.IsPunctuation(src[i]))
            {
                builder.Append(src[i]);
            }
        }

        return builder.ToString();
    }

    static string[] GetWords(string source)
    {
        source = source.Replace("\r\n", " ");

        return source.Split(" ");
    }

    static Dictionary<string, int> WordFreq(string[] words)
    {
        Dictionary<string, int> frequency = new Dictionary<string, int>();
        
        foreach (var word in words)
        {
            if (frequency.ContainsKey(word))
            {
                frequency[word]++;
            }
            else
            {
                frequency.Add(word, 1);
            }
        }
        return frequency;
    }
}