using System.Linq;

namespace TagsCloudVisualization.Tests;

public static class TestWords
{
    public static readonly string[] EnglishWords = new[]
    {
        "Apple", "Microsoft", "JetBrains", "Wiki", "Windows", "CopyPaste", "Linux", "Tim", "Bill", "Tsvetkov", 
        "Andronov", "Mishurin", "Konovalov", "Davletbaev", "Fruit", "Jenga", "Kubernetes", "Samsung", "Tinkoff", "SKB",
        "Mother", "Alex", "Max", "SQWOZBAB", "Command", "Lion", "Discord", "Teams", "Son", "Pen", "Java", "C#", "Python"
    };

    public static readonly string[] RussianWords = new[]
    {
        "Админ", "Код-ревью", "Авторизация", "Программирование", "Аджайл", "Айпи", "Компьютер", "Айтишник",
        "Аккаунт", "Билл Гейтс", "Генератор", "Алгоритм", "Фабрика", "Альфа", "Апдейт", "Блокчейн",
        "Девайс", "Дыра", "Железо", "Капча", "Кейс",
        "IT", "C#", "SKB",
    };
    
    public static readonly string[] UnnecessaryEnglishWords = new[]
    {
        "We", "They", "He", "Him", "All",
        "Or", "And", "A", "An", "In", "Out"
    };
    
    public static readonly string[] UnnecessaryRussianWords = new[]
    {
        "Ты", "Он", "Она", "Я",
        "А", "И", "Или"
    };

    public static string[] RussianWordsWithUnnecessary =>
        RussianWords.Concat(UnnecessaryRussianWords).ToArray();
    
    public static string[] EnglishWordsWithUnnecessary =>
        RussianWords.Concat(UnnecessaryEnglishWords).ToArray();
}