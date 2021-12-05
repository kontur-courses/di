namespace TagCloud
{
    public class TagCloud
    {
        private ICloudLayouter _layouter;
        private ITagCloudDrawer _drawer;
        private IDrawerSettings _drawerSettings;
        private ITextProcessingSettings _textProcessingSettings;

        public TagCloud(IDrawerSettings drawerSettings, ITextProcessingSettings textProcessingSettings)
        {
            _drawerSettings = drawerSettings;
            _textProcessingSettings = textProcessingSettings;
        }
    }

    public interface ITextProcessingSettings
    {
        
    }

    public interface IDrawerSettings
    {
        
    }
}