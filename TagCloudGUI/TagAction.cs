using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TagsCloudVisualization;


namespace TagCloudGraphicalUserInterface
{
    public class TagAction : IActionForm
    {
       
        private Graphics g;
        private IImage image;
        private TagCloud cloud;
        public TagAction(TagCloud cloud, IImage image)
        {
            this.cloud = cloud;
            this.image = image;
        }
        string IActionForm.Category => "Нарисовать";

        string IActionForm.Name => "TagCloud";

        string IActionForm.Description => "Нарисовать Облако тегов";

        void IActionForm.Perform()
        {
            SettingsForm.For(cloud).ShowDialog();
            cloud = new TagCloud();
            cloud.CreateTagCloud(DividerTags.GetCircularCloudLayouter(Environment.CurrentDirectory + "\\..\\..\\..\\..\\TagCloudShould\\word_data\\data.txt"),new ArithmeticSpiral(Point.Empty));
            Draw(cloud,image);
            
            
            //cloud.Save("dede");

            //cloud.Draw(g);
        }
        public static void Draw(TagCloud tagCloud, IImage drawImage)
        {
            var srcSize = tagCloud.GetScreenSize();


            var graphics = drawImage.StartDrawing();
            var rectangles = tagCloud.GetRectangles();
            foreach (var textRectangle in rectangles)
            {
                //g.DrawRectangle(new Pen(Color.Black, 1),
                //    new Rectangle(textRectangle.rectangle.Location + srcSize / 2, textRectangle.rectangle.Size));
                var color = Color.FromArgb((int)textRectangle.font.Size % 255, 0,
                    (int)(textRectangle.font.Size * 2) % 255);
                graphics.DrawString(textRectangle.text, textRectangle.font, new SolidBrush(color),
                    textRectangle.rectangle.Location + new Size(srcSize.Width / 2, srcSize.Height / 2));
                drawImage.UpdateUi();
            }
            

        }
    }
}
