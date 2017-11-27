using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class CloudTagDrawer
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly IFontSizeMaker fontSizeMaker;
        private readonly IFileAnalyzer fileAnalyzer;
        private readonly ITagHandler tagHandler;
        private readonly int height;
        private readonly int width;

        public CloudTagDrawer(
            ICloudLayouter cloudLayouter,
            IFontSizeMaker fontSizeMaker,
            IFileAnalyzer fileAnalyzer,
            ITagHandler tagHandler,
            int height,
            int width
        )
        {
            this.cloudLayouter = cloudLayouter;
            this.fontSizeMaker = fontSizeMaker;
            this.fileAnalyzer = fileAnalyzer;
            this.tagHandler = tagHandler;
            this.height = height;
            this.width = width;
        }


//
//        public  void DrawTagsToFile(IEnumerable<string> input, string output)
//        {
//            var frequencyDict = fileAnalyzer.GetWordsFrequensy(input);
//            var tagRectangles = tagHandler.MakeTagRectangles(frequencyDict);
//            
//            using (var bitmap = new Bitmap(width, height))
//            {
//                DrawTagsOnBitmap(tagRectangles, bitmap);
//                bitmap.Save(output);
//            }
//        }
//        public  void DrawTagsToForm(IEnumerable<string> input)
//        {
//            var frequencyDict = fileAnalyzer.GetWordsFrequensy(input);
//            var tagRectangles = tagHandler.MakeTagRectangles(frequencyDict);
//            
//            using (var bitmap = new Bitmap(width, height))
//            {
//                DrawTagsOnBitmap(tagRectangles, bitmap);
//                Form aForm = new Form();
//                aForm.Width = width;
//                aForm.Height = height;
//                aForm.BackgroundImage = bitmap;
//                aForm.ShowDialog();
//            }
//        }

        public Bitmap DrawTags(IEnumerable<string> input)
        {
            var frequencyDict = fileAnalyzer.GetWordsFrequensy(input);
            var tagRectangles = tagHandler.MakeTagRectangles(frequencyDict);
            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                var selPen = new Pen(Color.Blue);
                var selBrush = new SolidBrush(Color.Black);


                foreach (var tag in tagRectangles)
                {
                    StringFormat stringFormat = new StringFormat();
//                    g.DrawRectangle(selPen, tag.Key);
                    g.DrawString(tag.Value.Item1, tag.Value.Item2, selBrush, tag.Key.X, tag.Key.Y, stringFormat);
                }
            }
            return bitmap;
        }
    }
}