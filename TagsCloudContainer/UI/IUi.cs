namespace TagsCloudContainer.UI
{
    public interface IUi
    {
        public string FontName { get; set; }
        public string BackGroundColor { get; set; }
        public string BrushColor { get; set; }
        public string PathToSave { get; set; }
        public string PathToOpen { get; set; }
        
        public string FormatToSave { get; set; }
        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }
        public int CanvasBorder { get; set; }
        public double RadiusOffset { get; set; }
        public double AngleOffset { get; set; }
    }
}