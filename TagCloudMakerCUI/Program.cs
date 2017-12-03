using System.Collections;
using TagCloud;

namespace TagCloudMakerCUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var cloudRectangles = TagCloudMaker.GetTagCloudRectangles(new [] {"qwe"});
        }
    }
}
