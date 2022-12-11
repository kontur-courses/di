//using System.Drawing;

//namespace TagsCloudVisualization
//{
//    public static class TagCloudDrawer
//    {
//        public static void Draw(Graphics g,Size srcSize,IEnumerable<TextRectangle> rectangles, Point offsetPoint,Color primaryColor,Color secondColor)
//        {
//            var offsetArgb= primaryColor.ToArgb() - secondColor.ToArgb();
//            var offsetColor = Color.FromArgb(offsetArgb);
//            var maxOffset = rectangles.OrderBy(x => x.font.Size).Last().font.Size;
//            //var minOffset= rectangles.OrderBy(x=>x.font.Size).First().font.Size;
//            foreach (var textRectangle in rectangles)
//            {
//                var color = Color.FromArgb((int)(offsetArgb*((double)maxOffset/textRectangle.font.Size)));
//                g.DrawString(textRectangle.text, textRectangle.font, new SolidBrush(color),
//                    textRectangle.rectangle.Location + (Size)offsetPoint + new Size(srcSize.Width / 2, srcSize.Height / 2));
//            }
//        }
//    }
//}