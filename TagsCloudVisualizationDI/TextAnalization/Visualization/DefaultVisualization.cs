﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudVisualizationDI.TextAnalization.Visualization
{
    public class DefaultVisualization : IVisualization
    {
        private List<RectangleWithWord> Elementslist { get; }
        private Brush ColorBrush { get; }
        private Font TextFont { get; }
        private Size ImageSize { get; }

        //private ImageFormat Format { get; }
        //private string SavePath { get; }

        public DefaultVisualization(List<RectangleWithWord> rectangleWithWordsList, 
            SolidBrush brush, Font font, Size imageSize)
        {
            Elementslist = rectangleWithWordsList;
            ColorBrush = brush;
            TextFont = font;
            ImageSize = imageSize;
            //Format = imageFormat;
            //SavePath = savePath + '.' + imageFormat;
        }

        /*
        public void DrawAndSaveImage()
        {
            using (var image = new Bitmap(ImageSize.Width, ImageSize.Height))
            {
                var drawImage = DrawRectangles(image);
                drawImage.Save(SavePath, Format);
            }
        }
        */

        public void DrawAndSaveImage(string savePath, ImageFormat format)
        {
            using var image = new Bitmap(ImageSize.Width, ImageSize.Height);
            var drawImage = DrawRectangles(image);
            if (!(File.Exists(savePath)))
                throw new FileNotFoundException($"файл по указанному пути: {savePath}  не найден");
            drawImage.Save(savePath, format);
        }


        private Bitmap DrawRectangles(Bitmap image)
        {
            using var graphics = Graphics.FromImage(image);
            foreach (var element in Elementslist)
            {
                var fontSize = TextFont.Size + 3 * element.WordElement.CntOfWords;
                var font = new Font("Times", fontSize);

                graphics.DrawString(element.WordElement.WordText, font, ColorBrush,
                    element.RectangleElement.Location.X, element.RectangleElement.Location.Y);
            }

            return image;
        }

        public void Dispose()
        {
            ColorBrush.Dispose();
            TextFont.Dispose();
        }
    }
}
