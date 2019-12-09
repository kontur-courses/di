using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Spire.Doc;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Handlers
{
    public class TextHandler
    {
        private readonly Dictionary<string, Func<string, string>> fileOpener 
            = new Dictionary<string, Func<string, string>>
        {
            {".txt", name =>
                {
                    using (var file = new StreamReader(name))
                        return file.ReadToEnd().Replace(Environment.NewLine, " ");
                }
            },
            {".doc", name =>
                {
                    var doc = new Document(name, FileFormat.Doc);
                    return doc.GetText().Replace(Environment.NewLine, " ");
                }
            },
            {".docx", name =>
                {
                    var doc = new Document(name, FileFormat.Docx);
                    return doc.GetText().Replace(Environment.NewLine, " ");
                }
            },
        };

        private readonly TextSettings textSettings;

        public TextHandler(TextSettings textSettings)
        {
            this.textSettings = textSettings;
        }

        public Dictionary<string, int> GetFrequencyDictionary()
        {
            var result = new Dictionary<string, int>();
            var text = fileOpener[textSettings.FileExtention ?? throw new InvalidOperationException()](textSettings.PathToFile);
            foreach (var value in textSettings.Filter.GetFilteredValues(text))
                result[value] = result.TryGetValue(value, out var frequency) ? ++frequency : 1;

            return result.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
        }
    }
}
    