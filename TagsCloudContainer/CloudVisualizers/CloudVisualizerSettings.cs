namespace TagsCloudContainer.CloudVisualizers
{
    public class CloudVisualizerSettings
    {
        public Palette Palette { get; set; }
        public IBitmapMaker BitmapMaker { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}