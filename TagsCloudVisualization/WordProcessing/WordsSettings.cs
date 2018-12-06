namespace TagsCloudVisualization.WordProcessing
{
    public class WordsSettings
    {
        public string PathToFile { get; set; }
        public WordAnalyzer WordAnalyzer { get; set; }

        public WordsSettings()
        {
            WordAnalyzer = new WordAnalyzer(this);
        }
    }
}