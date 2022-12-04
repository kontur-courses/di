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
            var tags = TagsLayouterHelper.ReadTagsFromFile("test.txt");
            var font = new System.Drawing.Font("Arial", 20, FontStyle.Bold);
            var tagsLayouter = new TagsLayouter(cloudLayouter, tags, font);
            tagsLayouter.PutAllTags();
            tagsLayouter.SaveBitmapWithText(TestContext.CurrentContext.Test.Name);
        }
    }
}
