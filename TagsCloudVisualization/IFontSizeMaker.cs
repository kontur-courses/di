namespace TagsCloudVisualization
{
    public interface IFontSizeMaker
    {
        int GetFontSizeByFreq(int maxFreq, int frequency);
    }
}