using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudForm.CircularCloudLayouterSettings;

namespace TagsCloudForm.WordFilters
{
    public class BoringWordsFilter : IWordsFilter
    {
        public IEnumerable<string> Filter(ICircularCloudLayouterWithWordsSettings settings,
            IEnumerable<string> words)
        {
            HashSet<string> boringWords;
            try
            {
                var settingsFilename = settings.BoringWordsFile;
                boringWords = File.ReadAllLines(settingsFilename).ToHashSet(StringComparer.OrdinalIgnoreCase);
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось загрузить файл с boring words");
            }

            return words.Where(x => !boringWords.Contains(x));
        }

        public IEnumerable<string> Filter(HashSet<string> boringWords, IEnumerable<string> words)
            {
                return words.Where(x => !boringWords.Contains(x));
            }
        }
}
