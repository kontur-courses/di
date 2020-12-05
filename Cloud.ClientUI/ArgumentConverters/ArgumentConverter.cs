using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Cloud.ClientUI.ArgumentConverters
{
    public class ArgumentConverter : IArgumentConverter
    {
        public ConvertedArguments ParseArguments(Arguments arguments)
        {
            var result = new ConvertedArguments
            {
                Center = ParseCenter(arguments),
                Font = ParseFont(arguments),
                ImageSize = ParseImageSize(arguments),
                TextColor = ParseColor(arguments),
                BoringWords = ParseBoringWords(arguments),
                InputFileName = ParseInputFileName(arguments),
                OutputFileName = ParseOutputFileName(arguments)
            };
            PrintArguments(result);
            return result;
        }

        private string ParseInputFileName(Arguments arguments)
        {
            if (!string.IsNullOrEmpty(arguments.InputFileName)) return arguments.InputFileName;
            Console.WriteLine("Неверно задано значение имени файла со словами, установлено дефолтное значение");
            return DefaultConfig.GetInputFileName();
        }

        private string ParseOutputFileName(Arguments arguments)
        {
            if (!string.IsNullOrEmpty(arguments.OutputFileName)) return arguments.OutputFileName;
            Console.WriteLine("Неверно задано значение имени итогового файла, установлено дефолтное значение");
            return DefaultConfig.GetOutputFileName();
        }

        private HashSet<string> ParseBoringWords(Arguments arguments)
        {
            if (arguments.BoringWords == null)
            {
                Console.WriteLine("Неверно заданы скучные слова, устанолвено дефолтное значение");
                return DefaultConfig.GetBoringWords();
            }

            var words = arguments.BoringWords.Split(',');
            return words.ToHashSet();
        }

        private void PrintArguments(ConvertedArguments arguments)
        {
            Console.WriteLine("Итоговый конфиг:");
            var font = $"\tНазвание шрифта: {arguments.Font.Name}, размер шрифта: {arguments.Font.Size}";
            var size = $"\tРазмер изображения: {arguments.ImageSize.ToString()}";
            var color = $"\tЦвет слов: {arguments.TextColor.ToString()}";
            var center = $"\tЦентр изображения: {arguments.Center.ToString()}";
            var boringWords = $"\tСкучные слова: {string.Join(", ", arguments.BoringWords)}";
            var inputFileName = $"\tИмя файла со словами: {arguments.InputFileName}";
            var outputFileName = $"\tИмя итогового файла: {arguments.OutputFileName}";
            Console.WriteLine(font);
            Console.WriteLine(size);
            Console.WriteLine(color);
            Console.WriteLine(center);
            Console.WriteLine(boringWords);
            Console.WriteLine(inputFileName);
            Console.WriteLine(outputFileName);
        }

        private Point ParseCenter(Arguments arguments)
        {
            if (arguments.XValue == null || arguments.YValue == null)
            {
                Console.WriteLine("Один из параметров центра изображения был неверным, установлено дефолтное значение");
                return DefaultConfig.GetCenter();
            }

            var isCorrectX = int.TryParse(arguments.XValue, out var x);
            var isCorrectY = int.TryParse(arguments.YValue, out var y);
            if (isCorrectX && isCorrectY)
                return new Point(x, y);
            Console.WriteLine("Один из параметров центра изображения был неверным, установлено дефолтное значение");
            return DefaultConfig.GetCenter();
        }

        private Font ParseFont(Arguments arguments)
        {
            if (arguments.FontName == null || arguments.FontSize == null)
            {
                Console.WriteLine("Один из параметров шрифта был неверным, установлено дефолтное значение");
                return DefaultConfig.GetFont();
            }

            var isCorrectSize = int.TryParse(arguments.FontSize, out var size);
            if (isCorrectSize)
                return new Font(arguments.FontName, size);
            Console.WriteLine("Один из параметров шрифта был неверным, установлено дефолтное значение");
            return DefaultConfig.GetFont();
        }

        private Size ParseImageSize(Arguments arguments)
        {
            if (arguments.ImageHeight == null || arguments.ImageWidth == null)
            {
                Console.WriteLine(
                    "Один из параметров размера изображения был неверным, установлено дефолтное значение");
                return DefaultConfig.GetSize();
            }

            var isCorrectWidth = int.TryParse(arguments.ImageWidth, out var width);
            var isCorrectHeight = int.TryParse(arguments.ImageHeight, out var height);
            if (isCorrectHeight && isCorrectWidth) return new Size(width, height);
            Console.WriteLine("Один из параметров размера изображения был неверным, установлено дефолтное значение");
            return DefaultConfig.GetSize();
        }

        private Color ParseColor(Arguments arguments)
        {
            if (arguments.Color == null)
                return DefaultConfig.GetColor();
            try
            {
                return Color.FromName(arguments.Color);
            }
            catch
            {
                Console.WriteLine("Неправильное название цвета, установлено дефолтное значение");
                return DefaultConfig.GetColor();
            }
        }
    }
}