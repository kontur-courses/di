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

            var visualizer = new CloudVisualization(PictureBox.Image, palette,
                font.MainFont, colorAlgorithm, words, cloud);
            visualizer.Paint();

            PictureBox.Refresh();
            Application.DoEvents();
        }
    }
}