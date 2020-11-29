using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IWordConverter
    {
        public Rectangle ConvertWord(string word);
    }
}