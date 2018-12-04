namespace TagsCloudContainer.Settings
{
    public class TextSettings
    {
        public int CountWords { get; set; }

        public TextSettings(int countWords)
        {
            this.CountWords = countWords;
        }
    }
}