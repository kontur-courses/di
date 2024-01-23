namespace TagsCloudContainer.Infrastucture.Settings
{
    public class ImageSettings
    {
        public int Width { get; set; } = 600;

        public int Height { get; set; } = 600;

        public Color RectangleBackgroundColor { get; set; } = Color.BlueViolet;

        public Color RectangleBordersColor { get; set; } = Color.Indigo;

        public Color BackgroundColor { get; set; } = Color.Black;

        public Color TextColor { get; set; } = Color.White;

        public Font Font { get; set; } = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Point);
    }
}