namespace TagsCloudContainer.Infrastructure
{
    internal interface IFontSizeGetter
    {
        public double GetFontSize(string word, double frequency);
    }
}