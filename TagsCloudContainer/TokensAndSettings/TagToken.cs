using System.Drawing;

namespace TagsCloudContainer.TokensAndSettings
{
    public class TagToken : WordToken
    {
        public Rectangle Rectangle { get; }

        public TagToken(WordToken wordToken, Rectangle rectangle) : base(wordToken.Word, wordToken.Count)
        {
            Rectangle = rectangle;
        }
    }
}
