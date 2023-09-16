using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите текст для анализа:");
        string inputText = Console.ReadLine();

        // Разделение текста на слова и подсчет частоты
        Dictionary<string, int> wordFrequency = AnalyzeWordFrequency(inputText);

        // Нахождение наиболее частых слов (например, топ 10)
        int topN = 1; // Количество наиболее частых слов для вывода
        var mostFrequentWords = GetMostFrequentWords(wordFrequency, topN);

        // Вывод результатов
        Console.WriteLine("Наиболее частые слова:");
        foreach (var pair in mostFrequentWords)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value} раз");
        }

        // Анализ длины предложений
        AnalyzeSentenceLength(inputText);

        // Анализ наиболее часто встречающего символа
        char mostFrequentChar = AnalyzeMostFrequentCharacter(inputText);
        Console.WriteLine($"Наиболее часто встречающийся символ: '{mostFrequentChar}'");

        // Анализ уникальных слов
        List<string> uniqueWords = AnalyzeUniqueWords(inputText);


        // Нахождение самого уникального слова
        string mostUniqueWord = GetMostUniqueWord(uniqueWords);
        Console.WriteLine($"Самое уникальное слово: {mostUniqueWord}");
    }

    static Dictionary<string, int> AnalyzeWordFrequency(string text)
    {
        string[] words = text.Split(new[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        // Используем словарь для подсчета частоты слов
        Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

        foreach (string word in words)
        {
            string cleanedWord = word.ToLower(); // Приводим слово к нижнему регистру
            if (wordFrequency.ContainsKey(cleanedWord))
            {
                wordFrequency[cleanedWord]++;
            }
            else
            {
                wordFrequency[cleanedWord] = 1;
            }
        }

        return wordFrequency;
    }

    static List<KeyValuePair<string, int>> GetMostFrequentWords(Dictionary<string, int> wordFrequency, int topN)
    {
        // Сортируем словарь по убыванию частоты и берем топ N
        var sortedWords = wordFrequency.OrderByDescending(pair => pair.Value).Take(topN).ToList();
        return sortedWords;
    }

    static void AnalyzeSentenceLength(string text)
    {
        string[] sentences = text.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        // Анализ и вывод длины каждого предложения
        Console.WriteLine("\nДлина предложений:");
        foreach (string sentence in sentences)
        {
            string cleanedSentence = sentence.Trim(); // Удаление лишних пробелов
            int sentenceLength = cleanedSentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            Console.WriteLine($"Длина: {sentenceLength} слов");
        }
    }

    static char AnalyzeMostFrequentCharacter(string text)
    {
        Dictionary<char, int> charFrequency = new Dictionary<char, int>();

        foreach (char character in text)
        {
            if (character != ' ') // Исключаем пробелы из анализа
            {
                if (charFrequency.ContainsKey(character))
                {
                    charFrequency[character]++;
                }
                else
                {
                    charFrequency[character] = 1;
                }
            }
        }

        char mostFrequentChar = charFrequency.OrderByDescending(pair => pair.Value).First().Key;
        return mostFrequentChar;
    }

    static List<string> AnalyzeUniqueWords(string text)
    {
        string[] words = text.Split(new[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        HashSet<string> uniqueWords = new HashSet<string>();

        foreach (string word in words)
        {
            string cleanedWord = word.ToLower(); // Приводим слово к нижнему регистру
            uniqueWords.Add(cleanedWord);
        }

        return uniqueWords.ToList();
    }

    static string GetMostUniqueWord(List<string> uniqueWords)
    {
        // Если есть уникальные слова, выбираем самое короткое слово в качестве самого уникального
        if (uniqueWords.Count > 0)
        {
            string mostUniqueWord = uniqueWords.OrderBy(word => word.Length).First();
            return mostUniqueWord;
        }
        else
        {
            return "Нет уникальных слов в тексте.";
        }
    }
}
