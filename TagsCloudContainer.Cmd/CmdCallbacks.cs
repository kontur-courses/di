using System;
using System.Drawing;

namespace TagsCloudContainer.Cmd
{
    public class CmdArguments
    {
        public Size ImageSize { get; set; } = new Size(800, 800);

        public Color Color { get; set; } = Color.BlueViolet;

        public PointF SpiralOffset { get; set; } = new PointF(-70, -50);
    }

    public class CmdCallbacks
    {
        public CmdArguments CmdArgs { get; } = new CmdArguments();

        public string GetHelpInformation()
        {
            return @"TagCloudContainer
/input filename - задает файл для ввода (по умолчанию: input.txt)
/output filename - задает файл вывода (по умолчанию: result.png)
/exclude filename - исключает записанные в файле слова из результата
Настройки формата вывода:
/imageSize 800x800 - задает размер изображения 100x100 (вместо x можно использовать: пробел, запятая
/font Arial - устанавливает шрифт
/fontSize 12 - устанавливает размер шрифта
/color BlueViolet - устанавливает цвет шрифта
Настройки алгоритма:
/spiralAngleStep 10 - задает шаг спирали
/spiralOffset 10;10 - задает смещение спирали от середины изображения (в качестве разделителя можно использовать пробел
";
        }

        public void SetImageSize(string imageSize)
        {
            var size = imageSize.Split(' ', ',', 'x');

            if (size.Length != 2)
            {
                throw new ArgumentException("Can't parse size, use 800x800 for example", nameof(imageSize));
            }

            CmdArgs.ImageSize = new Size(int.Parse(size[0]), int.Parse(size[1]));
        }

        public void SetColor(string color)
        {
            CmdArgs.Color = Color.FromName(color);
        }

        public void SetSpiralOffset(string spiralOffset)
        {
            var size = spiralOffset.Split(' ', ';');

            if (size.Length != 2)
            {
                throw new ArgumentException("Can't parse spiralOffset, use 12;12 for example", nameof(spiralOffset));
            }

            CmdArgs.SpiralOffset = new PointF(int.Parse(size[0]), int.Parse(size[1]));
        }
    }
}