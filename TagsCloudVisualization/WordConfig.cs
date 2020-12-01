using System.Drawing;

namespace TagsCloudVisualization
{
    public class WordConfig : IWordConfig //TODO add center
    {
        public Font font { get; }//toDo rename

        public WordConfig(Font font)
        {
            this.font = font;
        }
    }
}