using System.IO;

namespace TagsCloudVisualization.Text
{
    public abstract class TextReader
    {
        private Stream input { get; }

        public TextReader(Stream stream)
        {
            input = stream;
        }

        public abstract string GetNextWord();
    }
}