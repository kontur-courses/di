using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Core;

namespace TagsCloud.ClientGUI
{
    public class TagsCloudPainter
    {
        private readonly ColorAlgorithm colorAlgorithm;
        private readonly FontSetting font;
        private readonly Dictionary<char, Color> letterColor = new Dictionary<char, Color>();
        private readonly Palette palette;
        private readonly PathSettings pathSettings;
        private readonly PictureBoxImageHolder pictureBox;
        private readonly SpiralSettings spiralSettings;
        private readonly Random random = new Random();

        public TagsCloudPainter(PictureBoxImageHolder pictureBox, Palette palette,
            FontSetting font, SpiralSettings spiralSettings,
            PathSettings pathSettings, ColorAlgorithm colorAlgorithm)
        {
            this.pictureBox = pictureBox;
            this.palette = palette;
            this.font = font;
            this.spiralSettings = spiralSettings;
            this.pathSettings = pathSettings;
            this.colorAlgorithm = colorAlgorithm;
        }

        public void Paint()
        {
            using (var graphics = pictureBox.StartDrawing())
            {
                var imageSize = pictureBox.GetImageSize();
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), 0, 0,
                    imageSize.Width, imageSize.Height);

                var words = TagsHelper.GetWords(pathSettings.PathToText, pathSettings.PathToBoringWords,
                    pathSettings.PathToDictionary, pathSettings.PathToAffix);
                var rectangles = TagsHelper.GetRectangles(imageSize, words,
                    spiralSettings.SpiralParameter, font.MainFont.Size);

                for (var i = 0; i < rectangles.Count; ++i)
                {
                    var drawFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    graphics.DrawRectangle(new Pen(Color.White), rectangles[i]);

                    using (var currentFont = new Font(font.MainFont.FontFamily,
                        (int) (font.MainFont.Height / 2 * Math.Log(words[i].Item2 + 1)),
                        font.MainFont.Style))
                    {
                        graphics.DrawString(words[i].Item1, currentFont,
                            new SolidBrush(GetColor(words[i].Item1[0])), rectangles[i], drawFormat);
                    }
                }
            }

            pictureBox.Refresh();
            Application.DoEvents();
        }

        private Color GetColor(char letter)
        {
            switch (colorAlgorithm.Type)
            {
                case ColorAlgorithmType.MultiColor:
                    return palette.ForeColors[random.Next(0, palette.ForeColors.Length)];
                case ColorAlgorithmType.SameFirstLetterHasSameColor:
                    if (!letterColor.ContainsKey(letter))
                        letterColor[letter] =
                            Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                    return letterColor[letter];
                default:
                    return palette.ForeColor;
            }
        }
    }
}