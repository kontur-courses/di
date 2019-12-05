using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm
{
    public class BoringWordsFilter
    {
        public IEnumerable<string>  Filter(AppSettings appSettings, IEnumerable<string> words)
        {
            HashSet<string> boringWords;
            try
            {
                var settingsFilename = appSettings.BoringWordsFile;
                boringWords = File.ReadAllLines(settingsFilename).ToHashSet();
            }
            catch (Exception e)
            {
                throw new FileLoadException(e.Message);
            }
            return words.Where(x=>!boringWords.Contains(x));
        }
    }
}
