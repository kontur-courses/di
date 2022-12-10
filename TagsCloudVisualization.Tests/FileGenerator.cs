using System;
using System.IO;

namespace TagsCloudVisualization.Tests
{
    public class FileGenerator
    {
        private readonly Random random = new Random();
        private readonly string[] englishWords = new[]
        {
            "Apple", "Microsoft", "JetBrains", "Wiki", "Windows", "CopyPaste", "Linux", "Tim", "Bill", "Tsvetkov", 
            "Andronov", "Mishurin", "Konovalov", "Davletbaev", "Fruit", "Jenga", "Kubernetes", "Samsung", "Tinkoff", "SKB",
            "Mother", "Alex", "Max", "SQWOZBAB", "Command", "Lion", "Discord", "Teams", "Son", "Pen", "Java", "C#", "Python",
            "We", "They", "He", "Him", "All",
            "Or", "And", "A", "An", "In", "Out"
        };
        private readonly string[] allWords = new[]
        {
            "Админ", "Код-ревью", "Авторизация", "Программирование", "Аджайл", "Айпи", "Компьютер", "Айтишник",
            "Аккаунт", "Билл Гейтс", "Генератор", "Алгоритм", "Фабрика", "Альфа", "Апдейт", "Блокчейн",
            "Девайс", "Дыра", "Железо", "Капча", "Кейс",
            "IT", "C#", "SKB",
            "Ты", "Он", "Она", "Я",
            "А", "И", "Или",
        };

        public void Generate(string fileName, int amountLines, bool onlyEngish = true)
        {
            using var writer = new StreamWriter(fileName);

            var words = onlyEngish ? allWords : englishWords;
            
            for (var i = 0; i < amountLines; i++)
                writer.WriteLine(words[random.Next(0, words.Length)]);
        }
    }
}