using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class ConsoleClient : IClient
    {
        private static HashSet<string> fontFamilies = FontFamily.Families
            .Select(font => font.Name).ToHashSet();

        public Size GetImageSize()
        {
            Console.WriteLine("задайте размер изображения(его ширина и высота в пикселях через пробел)");
            var input = Console.ReadLine();
            var digits = input.Split(" ");

            if (digits.Length != 2)
            {
                Console.WriteLine("Размер должен задаваться через пробел и содержать 2 целых числа");
                return GetImageSize();
            }
            if (int.TryParse(digits[1], out var resHeight)
                && int.TryParse(digits[0], out var resWidth))
                return new Size(resWidth, resHeight);

            Console.WriteLine("Размер должен задаваться целыми числами");
            return GetImageSize();
        }

        private Color GetColor(string obj)
        {
            Console.WriteLine($"задайте цвет для {obj} в формате RGB(каждый из цветов через пробел)");
            var input = Console.ReadLine();
            var digits = input.Split(" ");

            if (digits.Length != 3)
            {
                Console.WriteLine("Цвет должен задаваться через пробел и содержать 3 целых числа");
                return GetColor(obj);
            }
            if (byte.TryParse(digits[1], out var green)
                && byte.TryParse(digits[0], out var red)
                && byte.TryParse(digits[2], out var blue))
                return Color.FromArgb(red, green, blue);

            Console.WriteLine("Цвет должен быть в рамках от 0 до 255");
            return GetColor(obj);
        }

        public FontFamily GetFontFamily()
        {
            Console.WriteLine("Напишите название шрифта для текста(Arial, Times New Roman, ...)");
            var input = Console.ReadLine();

            if (fontFamilies.Contains(input))
                return new FontFamily(input);

            Console.WriteLine("Шрифт не найден");
            return GetFontFamily();
        }

        public Color GetTextColor()
        {
            return GetColor("текста");
        }

        public Color GetBackgoundColor()
        {
            return GetColor("фона");
        }

        public void ShowPathToNewFile(string path)
        {
            Console.WriteLine($"Изображение сохранено {path}");
        }

        public string GetNameForImage()
        {
            Console.WriteLine("Придумайте название для изображения");
            return Console.ReadLine();
        }

        public bool IsFinish()
        {
            if (Console.KeyAvailable)
            {
                var cl = Console.ReadKey(true);

                return cl.Modifiers.HasFlag(ConsoleModifiers.Control) 
                    && cl.Key.ToString() == "c";
            }
            return false;
        }
    }
}
