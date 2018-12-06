using System;

namespace TagsCloudContainer.Preprocessing
{
    public class WordsPreprocessorSettings
    {
        //я хотел сначала сделать настройки препроцессинга через эти две функции, 
        //  но я не знаю как это тогда через ui настраивать
        //public Func<string, bool> WordsWhich;
        //public Func<string, string> WordsSelector;

        public string[] ExcludedWords { get; set; } = new string[0];
        public bool BringInTheInitialForm { get; set; }
    }
}