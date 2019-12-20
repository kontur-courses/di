using TagsCloudVisualization.Providers.WordSource.Interfaces;

namespace TagsCloudVisualization.Providers.WordSource.Changers
{
    internal class LowerCaseWordChanger : IWordChanger
    {
        public string Change(string word)
        {
            return word.ToLower();
        }
    }
}