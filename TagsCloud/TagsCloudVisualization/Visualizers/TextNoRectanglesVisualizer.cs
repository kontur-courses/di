using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Drawing.Drawing2D;
using TagsCloudTextPreparation;
using TagsCloudVisualization.Styling;
using TagsCloudVisualization.Styling.Themes;
using TagsCloudVisualization.Styling.WordSizeCalculators;

namespace TagsCloudVisualization.Visualizers
{
    public class TextNoRectanglesVisualizer : ICloudVisualizer
    {
        public Bitmap Visualize(Theme theme, IEnumerable<RectangleF> rectangles,
            int width = 1000, int height = 1000)
        {
            var result = new Bitmap(width, height);
            var random = new Random();
            using (var graphics = Graphics.FromImage(result))
            {
                graphics.FillRectangle(theme.BackgroundBrush, new RectangleF(0, 0, width, height));
                foreach (var rectangle in rectangles)
                    graphics.FillRectangle(GetRandomBrush(random, theme.RectangleBrushes), rectangle);
                return result;
            }
        }

        public Bitmap Visualize(Style style, IEnumerable<Tag> tags,
            int width = 1000, int height = 1000)
        {
            var result = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(result))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.CompositingMode = CompositingMode.SourceOver;
                
                graphics.FillRectangle(style.Theme.BackgroundBrush, new RectangleF(0, 0, width, height));

                foreach (var tag in tags)
                {
                    var font = new Font(style.FontProperties.Name, style.GetWordSize(tag.Count));
                    var state = graphics.Save();

                    var size = style.WordSizeCalculator.GetTagSize(style.FontProperties,
                        style.GetWordSize(tag.Count), new FrequencyWord(tag.Word, tag.Count));

                   
                  
                    
                   // graphics.FillRectangle(style.Theme.RectangleBrushes[0],tag.Area);
                    
                    

                    var format = new StringFormat()
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center,
                    };

                    
                    var path = new GraphicsPath();
                    path.AddString(tag.Word, font.FontFamily,
                        Convert.ToInt32(FontStyle.Bold), tag.Area.Height, new PointF(0, 0), format);


                    RectangleF textArea = path.GetBounds();
                    PointF[] target_pts = {
                        new PointF(tag.Area.Left, tag.Area.Top),
                        new PointF(tag.Area.Right, tag.Area.Top),
                        new PointF(tag.Area.Left, tag.Area.Bottom)};
                    graphics.Transform = new Matrix(textArea, target_pts);

                    // Draw the text.
                    
                    graphics.FillPath(style.Theme.GetTagBrush(tag), path);

                    /*var tagSize = graphics.MeasureString(tag.Word, font);
                    
                    //graphics.TranslateTransform(tag.Area.Left, tag.Area.Top);
                    //graphics.ScaleTransform(
                    //    tag.Area.Width/size.Width,tag.Area.Height/size.Height);
                    //graphics.DrawString(tag.Word, font, style.Theme.GetTagBrush(tag), PointF.Empty);
                    var path = new GraphicsPath();
                    path.AddString(
                        tag.Word,
                        font.FontFamily,
                        (int) font.Style,
                        graphics.DpiY * font.Size / 72f, // em size
                        new Point(0, 0), // location where to draw text
                        format);
                    
                    var matr = new Matrix();
                    matr.Translate(tag.Area.Left,tag.Area.Right);
                    path.Transform(matr);
                    
                    //graphics.FillPath(style.Theme.GetTagBrush(tag),path);
                    graphics.DrawString(tag.Word, font, style.Theme.GetTagBrush(tag), tag.Area);*/
                    graphics.Restore(state);
                }

                return result;
            }
        }

        private Brush GetRandomBrush(Random random, ImmutableArray<Brush> brushes)
        {
            return brushes[random.Next(0, brushes.Length)];
        }
    }
}