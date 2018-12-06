using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class Word : IWord
    {
        public string Value { get; }
        public Font Font { get; }

        public Word(string value, Font font)
        {
            Value = value;
            Font = font;
        }
    }
}