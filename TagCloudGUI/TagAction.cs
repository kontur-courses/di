using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.Rectangles;
using TagsCloudVisualization;


namespace TagCloudGraphicalUserInterface
{
    public class TagAction : IActionForm
    {
        
        private IImage image;
        private TagCloudSettings settings;
        private Palette palette;
        public TagAction(IImage image, TagCloudSettings settings,Palette palette)
        {
            this.image = image;
            this.settings = settings;
            this.palette = palette;
        }
        string IActionForm.Category => "Нарисовать";

        string IActionForm.Name => "TagCloud";

        string IActionForm.Description => "Нарисовать Облако тегов";

        void IActionForm.Perform()
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();
            settings.ImagesDirectory= dialog.FileName;
            SettingsForm.For(settings).ShowDialog();
            var cloud = TagCloud.InitialCloud(settings.ImagesDirectory,settings.Font,settings.maxFont,settings.minFont);
            cloud.CreateTagCloud(new CircularCloudLayouter(),new ArithmeticSpiral(settings.StartPoint,settings.ellipsoide,settings.desteny));
            var sizeCloud = cloud.GetScreenSize();
            image.RecreateImage(new ImageSettings() { Height = sizeCloud.Height, Width = sizeCloud.Width });
            Draw(cloud, image, palette);
        }
        public static void Draw(TagCloud tagCloud, IImage drawImage,Palette palette)
        {
            var srcSize = drawImage.GetImageSize();
            var graphics = drawImage.StartDrawing();
            graphics.FillRectangle(new SolidBrush(palette.BackgroundColor),new Rectangle(Point.Empty,srcSize));
            var rectangles = tagCloud.GetRectangles();

            var col = palette.PrimaryColor;
            foreach (var textRectangle in rectangles)
            {
                //g.DrawRectangle(new Pen(Color.Black, 1),
                //    new Rectangle(textRectangle.rectangle.Location + srcSize / 2, textRectangle.rectangle.Size));
                var color = Color.FromArgb((int)(col.R+ textRectangle.font.Size) % 255, 0,
                    (int)((col.B + textRectangle.font.Size)) % 255);
                graphics.DrawString(textRectangle.text, textRectangle.font, new SolidBrush(color),
                    textRectangle.rectangle.Location + new Size(srcSize.Width / 2, srcSize.Height / 2));
                drawImage.UpdateUi();
            }
        }
    }
}
