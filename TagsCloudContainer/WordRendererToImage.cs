﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordRendererToImage : IWordRenderer
    {
        public Font DefaultFont = new Font("Arial", 32, GraphicsUnit.Pixel);
        public Color DefaultColor = Color.Red;
        
        private Image output;
        private Graphics graphics;
        
        private Func<RenderingInfo, LayoutedWord, Font> fontFunction = DefaultFontFunction;
        private Func<RenderingInfo, LayoutedWord, Color> colorFunction = DefaultColorFunction;
        private Func<SizingInfo, LayoutedWord, float> scaleFunction = (info, word) => word.Count;

        public bool AutoSize = false;

        public Image Output
        {
            get => output;
            set
            {
                output = value;
                graphics = Graphics.FromImage(output);
            }
        }

        public WordRendererToImage()
        {
            Output = new Bitmap(1, 1);
            AutoSize = true;
        }

        public WordRendererToImage(Image output)
        {
            Output = output;
        }

        public WordRendererToImage WithFont(Func<RenderingInfo, LayoutedWord, Font> fontFunc)
        {
            fontFunction = fontFunc;
            return this;
        }
        
        public WordRendererToImage WithDefaultFont(Font defaultFont)
        {
            DefaultFont = defaultFont;
            fontFunction = (info, word) => info.Renderer.DefaultFont;
            return this;
        }

        public WordRendererToImage WithFont(Font defaultFont)
            => WithDefaultFont(defaultFont).WithFont((info, word) => info.Renderer.DefaultFont);
        
        public WordRendererToImage WithColor(Func<RenderingInfo, LayoutedWord, Color> colorFunc)
        {
            colorFunction = colorFunc;
            return this;
        }

        public WordRendererToImage WithDefaultColor(Color defaultColor)
        {
            DefaultColor = defaultColor;
            return this;
        }

        public WordRendererToImage WithColor(Color defaultColor)
            => WithDefaultColor(defaultColor).WithColor((info, word) => info.Renderer.DefaultColor);
        
        public WordRendererToImage WithScale(Func<SizingInfo, LayoutedWord, float> scaleFunc)
        {
            scaleFunction = scaleFunc;
            return this;
        }

        public IEnumerable<LayoutedWord> SizeWords(IEnumerable<LayoutedWord> words)
        {
            var sizingInfo = new SizingInfo(this, words.ToArray());
            foreach (var word in sizingInfo.WordsArray)
            {
                var size = graphics.MeasureString(word.Word, DefaultFont);
                var scale = scaleFunction(sizingInfo, word);
                size = new SizeF(size.Width * scale, size.Height * scale);
                yield return new LayoutedWord(word.Word, word.Count, size);
            }
        }

        public void Render(IEnumerable<LayoutedWord> words)
        {
            var renderingInfo = new RenderingInfo(this, words.ToArray());
            if(AutoSize)
                Output = new Bitmap((int) renderingInfo.WordsBorders.Size.Width, (int) renderingInfo.WordsBorders.Size.Height);
            foreach (var word in renderingInfo.WordsArray)
            {
                var font = ScaledToRectangle(fontFunction(renderingInfo, word), word.Rectangle);
                var color = colorFunction(renderingInfo, word);
                var rect = word.Rectangle;
                
                if (!AutoSize) rect.Offset(Output.Width / 2f, Output.Height / 2f);
                else rect.Offset(-renderingInfo.WordsBorders.X, -renderingInfo.WordsBorders.Y);
                
                graphics.DrawString(word.Word, font, new SolidBrush(color), rect.Location);
            }
        }

        private static Font DefaultFontFunction(RenderingInfo info, LayoutedWord word) => info.Renderer.DefaultFont;

        private static Color DefaultColorFunction(RenderingInfo info, LayoutedWord word)
        {
            var minCount = info.MinWordCount;
            var maxCount = info.MaxWordCount;
            var t = (word.Count - minCount) / (float) (maxCount - minCount);
            return Color.FromArgb(
                info.Renderer.LerpInt(0, 255, t),
                info.Renderer.LerpInt(255, 0, t),
                128);
        }

        private Font ScaledToRectangle(Font font, RectangleF rectangle)
        {
            var scale = rectangle.Height / graphics.MeasureString("h", font).Height;
            return new Font(font.FontFamily, font.Size * scale, font.Style, font.Unit);
        }
        
        private int LerpInt(int a, int b, float t) => (int) (a + (b - a) * t);
        private float Lerp(float a, float b, float t) => a + (b - a) * t;

        public class SizingInfo
        {
            public readonly WordRendererToImage Renderer;
            public readonly LayoutedWord[] WordsArray;
            public readonly int MinWordCount;
            public readonly int MaxWordCount;
            public readonly int TotalWordsCount;
            
            public SizingInfo(WordRendererToImage renderer, LayoutedWord[] wordsArray)
            {
                Renderer = renderer;
                WordsArray = wordsArray;

                MinWordCount = wordsArray.Min(word => word.Count);
                MaxWordCount = wordsArray.Max(word => word.Count);
                TotalWordsCount = wordsArray.Sum(word => word.Count);
            }
        }

        public class RenderingInfo : SizingInfo
        {
            public RectangleF WordsBorders;

            public RenderingInfo(WordRendererToImage renderer, LayoutedWord[] wordsArray) : base(renderer, wordsArray)
            {
                WordsBorders = RectangleF.FromLTRB(
                    WordsArray.Min(w => w.Rectangle.Left),
                    WordsArray.Min(w => w.Rectangle.Top),
                    WordsArray.Max(w => w.Rectangle.Right),
                    WordsArray.Max(w => w.Rectangle.Bottom));
            }
        }
    }
}