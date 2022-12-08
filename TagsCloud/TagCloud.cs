using System;
using System.IO;
using TagsCloud.TextWorkers;

namespace TagsCloud
{
    public class TagCloud
    {
        private readonly PrintSettings printSettings;
        private readonly string filePath;

        public TagCloud(PrintSettings printSettings, string filePath)
        {
            if (!File.Exists(filePath) || filePath == null) 
                throw new FileNotFoundException("Файл с текстом отсутствует");

            this.printSettings = printSettings;
            this.filePath = filePath;
        }

        public void PrintTagCloud(string path, string extension)
        {
            var fullPath = path + extension;

            var wordsFreq = MorphsParser.GetMorphs(filePath);

            var relativeWordsSize = WordsRectanglesScaler.ConvertFreqToPropotions(wordsFreq);

            var bitmap = new Bitmapper(1024, 720, printSettings);
            bitmap.DrawWords(relativeWordsSize);
            bitmap.SaveFile(fullPath);
        }
    }
}
