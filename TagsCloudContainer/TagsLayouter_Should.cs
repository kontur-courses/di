using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer
{
    [TestFixture]
    public class TagsLayouter_Should
    {
        [TestCase(true)]
        [TestCase(false)]
        public void CreateNewImageTagsLayouter(bool input)
        {
            var cloudLayouter = new CircularCloudLayouter(new Point(400, 250),input);
            var tags = new ReadTagsFromTxt().ReadTagsFromFile("test.txt");
            var tagsLayouter = new TagsLayouter(cloudLayouter, tags, new FontFamily("Arial"), 60.0f
                , new SolidBrush(Color.Black), new Bitmap(800, 500));
            tagsLayouter.PutAllTags();
            tagsLayouter.SaveBitmapWithText(TestContext.CurrentContext.Test.Name);
        }
    }
}
