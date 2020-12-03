using System;
using System.Collections.Generic;
using Autofac;

namespace WordCloudGenerator
{
    public class ConsoleUI : IUserInterface
    {
        public void Run(IContainer container)
        {
            var pathToText = Ask("полный путь к файлу с текстом");
            var text = Reader.ReadFile(pathToText);
            var wordsToSkip = RepeatUntil(
                () => Ask("Введите слово которое нужно пропускать (пустая строка, чтобы закончить)"),
                string.IsNullOrEmpty);

            using var scope = container.BeginLifetimeScope();
            var preparer = scope.Resolve<Preparer>(new NamedParameter("wordsToSkip", wordsToSkip));
            var wordCount = int.Parse(
                AskUntilCorrect("сколько слов должно быть в облаке", str => int.TryParse(str, out _)));

            var prepared = PerformAndReport("Обработка текста", () => preparer.CreateWordFreqList(text, wordCount));

            var algoChoice = AskUntilCorrect(
                "какой алгоритм использовать - экспоненциальный или пропорциональный? (1/2)",
                str => str == "1" || str == "2");

            var algorithm = algoChoice == "1" ? GeneratorAlgorithms.Exponential : GeneratorAlgorithms.Proportional;

            var generator = scope.Resolve<Generator>(new TypedParameter(algorithm.GetType(), algorithm));
            var graphicStrings = PerformAndReport("Генерация облака слов",
                () => generator.CalculateFontSizeForWords(prepared));

            var painter = scope.Resolve<Painter>();

            var img = PerformAndReport("Рисование облака", () => painter.Paint(graphicStrings));

            var pathToSave = AskUntilCorrect("путь по которому сохранить изображение", Saver.IsPathCorrect);
            Saver.SaveImage(img, pathToSave);

            Print($"Изображение сохранено в {pathToSave}");
        }

        private string Ask(string what)
        {
            Console.WriteLine($"Введите {what}");

            return Console.ReadLine();
        }

        private void Print(string str)
        {
            Console.WriteLine(str);
        }

        private string AskUntilCorrect(string what, Func<string, bool> isCorrect)
        {
            Console.WriteLine($"Введите {what}");
            var read = Console.ReadLine();
            while (!isCorrect(read))
            {
                Console.WriteLine("Вы ввели неправильное значение, введите снова");
                read = Console.ReadLine();
            }

            return read;
        }

        private IEnumerable<T> RepeatUntil<T>(Func<T> get, Func<T, bool> shouldStop)
        {
            while (true)
            {
                var getted = get();
                if (shouldStop(getted))
                    yield break;

                yield return getted;
            }
        }

        private T PerformAndReport<T>(string actionName, Func<T> func)
        {
            Console.Write($"{actionName} выполняется");
            var result = func();
            ClearLine();
            Console.Write($"{actionName} закончена");
            return result;
        }

        private void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
        }
    }
}