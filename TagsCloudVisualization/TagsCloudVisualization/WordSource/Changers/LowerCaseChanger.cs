using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Changers
{
    internal class LowerCaseChanger : IChanger<string>
    {
        public string Change(string word)
        {
            return word.ToLower();
        }
    }
}