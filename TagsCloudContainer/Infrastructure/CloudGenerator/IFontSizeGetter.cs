namespace TagsCloudContainer.Infrastructure.CloudGenerator
{
    internal interface IFontSizeGetter
    {
        public double GetFontSize(string word, double frequency);
    }
}