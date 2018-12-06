using System.Drawing;

namespace TagsCloudContainer
{
    public class WordsContainer
    {
        public string[] RawWords { get; set; } = new string[0];
        public WordsManager ProcessedWords { get; set; } = new WordsManager();
        public WordDrawInfo[] WordsToDraw { get; set; } = new WordDrawInfo[0];
        public Point WordsCenter { get; set; } = new Point(0, 0);
    }
}