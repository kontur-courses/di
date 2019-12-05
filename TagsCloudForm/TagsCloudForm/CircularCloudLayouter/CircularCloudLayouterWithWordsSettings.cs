namespace TagsCloudForm.CircularCloudLayouter
{
    public class CircularCloudLayouterWithWordsSettings
    {
        public int CenterX { get; set; } = 300;

        public int CenterY { get; set; } = 300;

        public int Scale { get; set; } = 5;

        public string WordsSource { get; set; } = "words.txt";

        public bool Ordered { get; set; } = false;

        public LanguageEnum Language { get; set; } = LanguageEnum.English;

    }

    public enum LanguageEnum
    {
        Russian,
        English
    }
}
