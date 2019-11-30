using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.Text
{
    public abstract class TextReader : IEnumerable<string>
    {
        protected Stream input;

        public TextReader(Stream stream)
        {
            input = stream;
        }

        public abstract IEnumerator<string> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}