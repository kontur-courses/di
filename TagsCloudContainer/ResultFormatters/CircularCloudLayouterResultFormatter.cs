using System.Drawing;
using TagsCloudContainer.DataProviders;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.ResultFormatters
{
    public class CircularCloudLayouterResultFormatter : IResultFormatter
    {
        private readonly IDataProvider dataProvider;
        private readonly ICloudSettings cloudSettings;
        private readonly IFontSettings fontSettings;

        public CircularCloudLayouterResultFormatter(IDataProvider dataProvider, ICloudSettings cloudSettings,
            IFontSettings fontSettings)
        {
            this.dataProvider = dataProvider;
            this.cloudSettings = cloudSettings;
            this.fontSettings = fontSettings;
        }
        public void GenerateResult(string outputFileName)
        {
            using (var bitmap = new Bitmap(cloudSettings.Size.Width, cloudSettings.Size.Height))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    foreach (var entry in dataProvider.GetData())
                    {
                        var font = new Font(fontSettings.FontFamily, 10);
                        var generatedFont = GetFont(graphics, entry.Key, entry.Value.Item1.Size, font);

                        graphics.DrawString(entry.Key, generatedFont, fontSettings.Brush, entry.Value.Item1);

                    }
                    bitmap.Save(outputFileName);
                }
            }
        }

        private Font GetFont(Graphics g, string longString, Size room, Font preferredFont)
        {
            var realSize = g.MeasureString(longString, preferredFont);
            var heightScaleRatio = room.Height / realSize.Height;
            var widthScaleRatio = room.Width / realSize.Width;

            var scaleRatio = heightScaleRatio < widthScaleRatio ? heightScaleRatio : widthScaleRatio;

            var scaleFontSize = preferredFont.Size * scaleRatio;

            return new Font(preferredFont.FontFamily, scaleFontSize - 1 > 0 ? scaleFontSize - 1 : scaleFontSize);
        }
    }
}
