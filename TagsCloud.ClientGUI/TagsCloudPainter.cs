using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Common;
using TagsCloud.Core;
using TagsCloud.Visualization;

namespace TagsCloud.ClientGUI
{
    public class TagsCloudPainter
    {
        private readonly ColorAlgorithm colorAlgorithm;
        private readonly FontSetting font;
        private readonly Palette palette;
        private readonly PathSettings pathSettings;

        public TagsCloudPainter(PictureBoxImageHolder pictureBox, Palette palette,
            FontSetting font, PathSettings pathSettings, ColorAlgorithm colorAlgorithm)
        {
            PictureBox = pictureBox;
            this.palette = palette;
            this.font = font;
            this.pathSettings = pathSettings;
            this.colorAlgorithm = colorAlgorithm;
        }

        public PictureBoxImageHolder PictureBox { get; }

        public void Paint(ICircularCloudLayouter cloud)
        {
            var words = TagsHelper.GetWords(pathSettings.PathToText, pathSettings.PathToBoringWords,
                pathSettings.PathToDictionary, pathSettings.PathToAffix);

            var correctFonts = new List<Font>();
            var rectangles = new List<Rectangle>();
            foreach (var word in words)
            {
                var newFont = new Font(font.MainFont.FontFamily, 
                    (int) (font.MainFont.Size * Math.Log(word.Item2 + 1)), font.MainFont.Style);
                correctFonts.Add(newFont);
                var rect = cloud.PutNextRectangle(new Size((int) newFont.Size * word.Item1.Length, newFont.Height));
                rectangles.Add(rect);
            }

            var visualizer = new CloudVisualization(PictureBox.Image, palette, 
                colorAlgorithm, words, rectangles, correctFonts);
            visualizer.Paint();

            DisposeFonts(correctFonts);
            PictureBox.Refresh();
            Application.DoEvents();
        }

        private void DisposeFonts(List<Font> fonts)
        {
            foreach (var currentFont in fonts)
                currentFont.Dispose();
        }
    }
}