using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Changers
{
    internal class LowerCaseWordChanger : IWordChanger
    {
        public string Change(string word)
        {
            return word.ToLower();
        }
    }
}