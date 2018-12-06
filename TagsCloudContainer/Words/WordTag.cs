using System.Drawing;

namespace TagsCloudContainer.Words
{
    public class WordTag
    {
        public Rectangle DescribedRectangle;
        public string Word;

        public WordTag(Rectangle describedRectangle, string word)
        {
            DescribedRectangle = describedRectangle;
            Word = word;
        }
    }
}
