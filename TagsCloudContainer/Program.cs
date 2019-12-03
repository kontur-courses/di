using System.Drawing;
using TagsCloudContainer.Core;

namespace TagsCloudContainer
{
    class Program
    {
        public static void Main(string[] args)
        {
            var circularCloudLayouter = new CircularCloudLayouter(new Point(500, 500));
            for (var i = 0; i < 600; i++)
                circularCloudLayouter.PutNextRectangle(new Size(20, 20));
            var cloudImageCreator = new TagCloudImageCreator(circularCloudLayouter);
            cloudImageCreator.Save();
        }
    }
}
