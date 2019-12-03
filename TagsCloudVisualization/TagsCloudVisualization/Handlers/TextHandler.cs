using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Spire.Doc;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Stemmers;

namespace TagsCloudVisualization.Handlers
{
    class TextHandler
    {
        private readonly Dictionary<string, Action<string>> rewriteIntoTxtFile = new Dictionary<string, Action<string>>
        {
            {".txt", name =>
                {
                    using (var file = new StreamReader(name, Encoding.Default))
                    {
                        File.WriteAllText(@"YandexStem/input.txt",file.ReadToEnd());
                    }
                }
            },
            {".doc", name =>
                {
                    var doc = new Document(name, FileFormat.Doc);
                    doc.SaveToTxt(@"YandexStem/input.txt", Encoding.UTF8);
                }
            },
            {".docx", name =>
                {
                    var doc = new Document(name, FileFormat.Docx);
                    doc.SaveToTxt(@"YandexStem/input.txt", Encoding.UTF8);
                }
            },
        };

        private readonly TextSettings textSettings;
        private readonly IStemmer stemmer;

        public TextHandler(TextSettings textSettings, IStemmer stemmer)
        {
            this.textSettings = textSettings;
            this.stemmer = stemmer;
        }

        public Dictionary<string, int> GetFrequencyDictionary()
        {
            var result = new Dictionary<string, int>();
            rewriteIntoTxtFile[textSettings.FileExtention ?? throw new InvalidOperationException()](textSettings.PathToFile);
            foreach (var (value, valueForFilter) in stemmer.GetStemmedString())
            {
                if (!textSettings.Filter.IsValidValue(value, valueForFilter))
                    continue;
                if (!result.ContainsKey(value))
                    result.Add(value, 0);
                result[value]++;
            }
            return result.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
        }
    }
}
