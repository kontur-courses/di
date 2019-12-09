using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenNLP;
using OpenNLP.Tools.PosTagger;
using TagsCloudForm.CircularCloudLayouter;

namespace TagsCloudForm.WordFilters
{
    public class PartOfSpeechFilter : IWordsFilter
    {
        public IEnumerable<string> Filter(HashSet<string> POStoFilter, IEnumerable<string> words)
        {
            var modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", "EnglishPOS.nbin");
            var tagDictDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", "tagdict");
            var posTagger = new EnglishMaximumEntropyPosTagger(modelPath, tagDictDir);
            var outList = new List<string>();
            var wordsEnumerator = words.GetEnumerator();
            wordsEnumerator.MoveNext();
            int counter = 0;
            while(wordsEnumerator.Current!=null)
            {
                var speechPart = posTagger.Tag(new []{wordsEnumerator.Current});
                if (!POStoFilter.Contains(speechPart[0]))
                    outList.Add(wordsEnumerator.Current);
                wordsEnumerator.MoveNext();
                counter++;
            }
            return outList;
        }


        public Result<IEnumerable<string>> Filter(CircularCloudLayouterWithWordsSettings settings, IEnumerable<string> words)
        {
            HashSet<string> POStoFilter;
            try
            {
                var settingsFilename = settings.filterPOSfile;
                POStoFilter = File.ReadAllLines(settingsFilename).ToHashSet(StringComparer.OrdinalIgnoreCase);
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<string>>("Не удалось загрузить файл с POS to Filter", words);
            }
            var modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", "EnglishPOS.nbin");
            var tagDictDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", "tagdict");
            var posTagger = new EnglishMaximumEntropyPosTagger(modelPath, tagDictDir);
            var outList = new List<string>();
            var wordsEnumerator = words.GetEnumerator();
            wordsEnumerator.MoveNext();
            int counter = 0;
            while (wordsEnumerator.Current != null)
            {
                var speechPart = posTagger.Tag(new[] { wordsEnumerator.Current });
                if (!POStoFilter.Contains(speechPart[0]))
                    outList.Add(wordsEnumerator.Current);
                wordsEnumerator.MoveNext();
                counter++;
            }
            return Result.Ok<IEnumerable<string>>(outList);
        }
    }
}
