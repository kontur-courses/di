using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.Rectangles;
using TagsCloudVisualization;


namespace TagCloudGraphicalUserInterface
{
    public class TagAction : IActionForm
    {
        private readonly IImageSettingsProvider imageSettingsProvider;
        private readonly TagCloudSettings settings;
        private readonly Palette palette;

        public TagAction(IImageSettingsProvider imageSettingsProvider, TagCloudSettings settings, Palette palette)
        {
            this.imageSettingsProvider = imageSettingsProvider;
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
            settings.ImagesDirectory = dialog.FileName;
            SettingsForm.For(settings).ShowDialog();
            var cloud = TagCloud.InitialCloud(settings.ImagesDirectory, settings.Font, settings.maxFont,
                settings.minFont);
            cloud.CreateTagCloud(new CircularCloudLayouter(),
                new ArithmeticSpiral(settings.StartPoint, settings.ellipsoidMultiplier, settings.densityMultiplier));
            var sizeCloud = cloud.GetScreenSize();
            imageSettingsProvider.RecreateImage(new ImageSettings()
            { Height = sizeCloud.Height, Width = sizeCloud.Width });
            DrawCloud(cloud, imageSettingsProvider, palette);
        }

        public static void DrawCloud(TagCloud tagCloud, IImageSettingsProvider drawImageSettingsProvider,
            Palette palette)
        {
            var srcSize = drawImageSettingsProvider.GetImageSize();
            var graphics = drawImageSettingsProvider.StartDrawing();
            graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), new Rectangle(Point.Empty, srcSize));
            var rectangles = tagCloud.GetRectangles();
            foreach (var textRectangle in rectangles)
            {
                graphics.DrawString(textRectangle.text, textRectangle.font, new SolidBrush(palette.PrimaryColor),
                    textRectangle.rectangle.Location + new Size(srcSize.Width / 2, srcSize.Height / 2));
                drawImageSettingsProvider.UpdateUi();
            }
        }
    }
}
