using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.CloudLayouter
{
    public class Cloud
    {
        public Point Center { get; }
        public List<Word> Words { get; }

        public Cloud(Point center)
        {
            Center = center;
            Words = new List<Word>();
        }
    }
}