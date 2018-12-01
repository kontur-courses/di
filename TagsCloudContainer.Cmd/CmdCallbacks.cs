using System;
using System.Drawing;

namespace TagsCloudContainer.Cmd
{
    public class CmdArguments
    {
        public Size ImageSize { get; set; } = new Size(800, 800);

        public Color Color { get; set; } = Color.BlueViolet;

        public string FontFamily { get; set; } = "Arial";

        public float FontSize { get; set; } = 12;

        public PointF SpiralOffset { get; set; } = new PointF(-70, -50);

        public double SpiralAngleStep { get; set; } = 10f;

        public string InputFilename { get; set; } = "input.txt";

        public string OutputFilename { get; set; } = "result.png";
    }

    public class CmdCallbacks
    {
        public CmdArguments CmdArgs { get; } = new CmdArguments();

        public string GetHelpInformation()
        {
            return @"TagCloudContainer
/input filename - задает файл для ввода (по умолчанию: input.txt)
/output filename - задает файл вывода (по умолчанию: result.png)
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

        public void SetInputFilename(string inputFilename)
        {
            CmdArgs.InputFilename = inputFilename;
        }

        public void SetOutputFilename(string filename)
        {
            CmdArgs.OutputFilename = filename;
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

        public void SetFontSize(double font)
        {
            CmdArgs.FontSize = (float)font;
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

        public void SetSpiralAngleStep(double spiralAngleStep)
        {
            CmdArgs.SpiralAngleStep = spiralAngleStep;
        }

        public void SetFont(string font)
        {
            CmdArgs.FontFamily = font;
        }
    }
}