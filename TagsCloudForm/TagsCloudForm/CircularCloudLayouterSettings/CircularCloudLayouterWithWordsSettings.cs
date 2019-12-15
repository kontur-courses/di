namespace TagsCloudForm.CircularCloudLayouterSettings
{
    public class CircularCloudLayouterWithWordsSettings:ICircularCloudLayouterWithWordsSettings
    {
        public int CenterX { get; set; } = 300;

        public int CenterY { get; set; } = 300;

        public int Scale { get; set; } = 5;

        public string WordsSource { get; set; } = "UserData/words.txt";

        public string BoringWordsFile { get; set; } = "UserData/boring.txt";

        public string PartOfSpeechToFilterFile { get; set; } = "UserData/filterPOS.txt";

        public bool Ordered { get; set; } = false;

        public bool UpperCase { get; set; } = true;

        public bool Frame { get; set; } = true;

        public bool Fill { get; set; } = true;


        public LanguageEnum Language { get; set; } = LanguageEnum.English;

    }

    public enum LanguageEnum
    {
        Chinese,
        Russian,
        English
    }
}
