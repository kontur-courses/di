using System.Drawing.Imaging;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class ImageSettingsAction : IUIAction
    {
        private Settings settings;

        public ImageSettingsAction(Settings settings)
        {
            this.settings = settings;
        }

        public string GetDescription() => "Image settings";

        public void Handle()
        {
            Console.WriteLine("1 - Palette");
            Console.WriteLine("2 - Font");
            Console.WriteLine("3 - Format");
            Console.WriteLine("4 - Size");
            Console.WriteLine("5 - Back");
            var answer = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
            switch (answer.KeyChar)
            {
                case '1':
                    PaletteKey();
                    break;
                case '2':
                    FontKey();
                    break;
                case '3':
                    FormatKey();
                    break;
                case '4':
                    SizeKey();
                    break;
                default:
                    return;
            }
        }

        private void PaletteKey()
        {
            Console.WriteLine($"Primary color is {settings.Palette.Primary}");
            Console.WriteLine($"Background color is {settings.Palette.Background}");
            Console.WriteLine("Enter color number to select");
            Console.WriteLine("Or any other key to keep the current one");
            Console.WriteLine("1 - Red");
            Console.WriteLine("2 - Orange");
            Console.WriteLine("3 - Yellow");
            Console.WriteLine("4 - Green");
            Console.WriteLine("5 - Blue");
            Console.WriteLine("6 - DarkBlue");
            Console.WriteLine("7 - Violet");
            Console.WriteLine("8 - Black");
            Console.WriteLine("9 - White");
            Console.WriteLine("Enter primary color");
            settings.Palette.Primary = ReadColor(Console.ReadKey().KeyChar, true);
            Console.WriteLine();
            Console.WriteLine("Enter background color");
            settings.Palette.Background = ReadColor(Console.ReadKey().KeyChar, false);
            Console.WriteLine();
        }

        private Color ReadColor(char input, bool isPrimary)
        {
            var deafultColor = isPrimary
                ? settings.Palette.Primary
                : settings.Palette.Background;

            switch (input)
            {
                case '1':
                    return Color.Red;
                case '2':
                    return Color.Orange;
                case '3':
                    return Color.Yellow;
                case '4':
                    return Color.Green;
                case '5':
                    return Color.Blue;
                case '6':
                    return Color.DarkBlue;
                case '7':
                    return Color.Violet;
                case '8':
                    return Color.Black;
                case '9':
                    return Color.White;
                default:
                    return deafultColor;
            }
        }

        private void FontKey()
        {
            if (!TryReadFontFamily(out var family)
                || !TryReadFontStyle(out var style))
                return;

            Console.WriteLine("Enter new size");
            Console.WriteLine("Or pass an empty string to be brought back to menu");
            var answer = Console.ReadLine() ?? "";
            Console.WriteLine();

            if (int.TryParse(answer, out var size))
                settings.Font = new Font(family, size, style);
        }

        private bool TryReadFontFamily(out FontFamily family)
        {
            Console.WriteLine("1 - Serif");
            Console.WriteLine("2 - SansSerif");
            Console.WriteLine("3 - Monospace");
            Console.WriteLine("4 - Back");
            var answer = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();

            family = FontFamily.GenericSerif;
            switch (answer.KeyChar)
            {
                case '1':
                    family = FontFamily.GenericSerif;
                    break;
                case '2':
                    family = FontFamily.GenericSansSerif;
                    break;
                case '3':
                    family = FontFamily.GenericMonospace;
                    break;
                default:
                    return false;
            }

            return true;
        }

        private bool TryReadFontStyle(out FontStyle style)
        {
            Console.WriteLine("1 - Regular");
            Console.WriteLine("2 - Bold");
            Console.WriteLine("3 - Italic");
            Console.WriteLine("4 - Underline");
            Console.WriteLine("5 - Strikeout");
            Console.WriteLine("6 - Back");
            var answer = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();

            style = FontStyle.Regular;
            switch (answer.KeyChar)
            {
                case '1':
                    style = FontStyle.Regular;
                    break;
                case '2':
                    style = FontStyle.Bold;
                    break;
                case '3':
                    style = FontStyle.Italic;
                    break;
                case '4':
                    style = FontStyle.Underline;
                    break;
                case '5':
                    style = FontStyle.Strikeout;
                    break;
                default:
                    return false;
            }

            return true;
        }

        private void FormatKey()
        {
            Console.WriteLine("1 - Png");
            Console.WriteLine("2 - Jpeg");
            Console.WriteLine("3 - Bmp");
            Console.WriteLine("4 - Back");
            var answer = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();

            switch (answer.KeyChar)
            {
                case '1':
                    settings.Format = ImageFormat.Png;
                    break;
                case '2':
                    settings.Format = ImageFormat.Jpeg;
                    break;
                case '3':
                    settings.Format = ImageFormat.Bmp;
                    break;
                default:
                    return;
            }
        }

        private void SizeKey()
        {
            Console.WriteLine($"Image size is {settings.ImageSize}");
            Console.WriteLine("Enter new size as");
            Console.WriteLine("HEIGHT WIDTH");
            Console.WriteLine("Or pass an empty string to be brought back to menu");
            var answer = Console.ReadLine() ?? "";
            Console.WriteLine();
            var split = answer.Split(' ');

            if (split.Length != 2)
                return;

            if (int.TryParse(split[0], out var height)
                && int.TryParse(split[1], out var width))
                settings.ImageSize = new Size(height, width);
        }
    }
}
