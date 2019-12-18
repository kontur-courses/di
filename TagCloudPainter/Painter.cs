﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NUnit.Framework;
using TagsCloudVisualization.Canvas;
using TagsCloudVisualization.TagCloudLayouter;
using TextPreprocessor.Core;

namespace TagCloudPainter
{
    public class Painter : ITagCloudPainter
    {
        private readonly LayouterFactory layouterFactory;
        private readonly PainterConfig painterConfig;
        private readonly Random random;

        public Painter(LayouterFactory layouterFactory, PainterConfig config)
        {
            this.layouterFactory = layouterFactory;
            painterConfig = config;
            random = new Random();
        }
        
        //TODO разбить метод на более мелкие методы.
        public void Draw(IEnumerable<TagInfo> tagInfos)
        {
            var canvas = new TagCloudCanvas(painterConfig.ImageWidth, painterConfig.ImageHeight);
            
            DrawBackground(canvas);
            
            DrawTags(canvas, tagInfos);
            
            canvas.Save(painterConfig.PathForSave, painterConfig.ImageName);
        } 

        private int GetTagFontSize(TagInfo tagInfo, PainterConfig config, int maxFrequency, int minFrequency)
        {
            var fontSize = config.MinFontSize + ((config.MaxFontSize - config.MinFontSize) * tagInfo.Frequency) / (maxFrequency - minFrequency);
            return fontSize;
        }

        private Size GetTagSize(TagInfo tagInfo, int fontSize, FontFamily family)
        {
            var font = new Font(family, fontSize);
            return TextRenderer.MeasureText(tagInfo.Tag.Content, font);
        }
        
        private Brush GetRandomBrush(List<Color> pallet)
        {
            if(pallet.Count == 0)
                throw new Exception("Сolors in the palette are not set");
            
            var color = pallet[random.Next(0, pallet.Count)];

            return new SolidBrush(color);
        }

        private void DrawBackground(Canvas canvas)
        {
            canvas.Draw(
                new Rectangle(new Point(0,0), new Size(painterConfig.ImageWidth, painterConfig.ImageHeight)), 
                new SolidBrush(painterConfig.BackgroundColor));
        }

        private void DrawTags(Canvas canvas, IEnumerable<TagInfo> tagInfos)
        {
            var layouter = layouterFactory.GetCircularLayouter(painterConfig.CloudCenter, painterConfig.layoutAlgorithm);
            
            var sortedTagInfos = tagInfos
                .OrderByDescending(tagInfo => tagInfo.Frequency)
                .ToArray();
            var maxFrequency = sortedTagInfos.First().Frequency;
            var minFrequency = sortedTagInfos.Last().Frequency;
            
            foreach (var tagInfo in sortedTagInfos)
            {
                var tagFontSize = GetTagFontSize(tagInfo, painterConfig, maxFrequency, minFrequency);
                var rec = layouter.PutNextRectangle(GetTagSize(tagInfo, tagFontSize, painterConfig.FontFamily));
                canvas.Draw(tagInfo.Tag.Content,new Font(painterConfig.FontFamily, tagFontSize), rec, GetRandomBrush(painterConfig.Palette));
            }
        }
    }
}