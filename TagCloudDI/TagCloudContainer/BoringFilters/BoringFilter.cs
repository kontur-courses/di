using MystemHandler;
using System.Net;
using TagCloudContainer.Interfaces;

namespace TagCloudContainer.BoringFilters
{
    public class BoringFilter : IBoringWordsFilter
    {
        public IEnumerable<string> FilterText(string text)
        {
            DownloadRuntimeIfNotExist();

            MystemMultiThread mystem = new(1, @"mystem.exe");
            return mystem.StemWords(text)!
                .Where(l => !l.IsSlug)
                .Select(l => l.Lemma);
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            DownloadRuntimeIfNotExist();

            MystemMultiThread mystem = new(1, @"mystem.exe");
            return words
                .SelectMany(s => mystem.StemWords(s)!)
                .Where(l => !l.IsSlug)
                .Select(l => l.Lemma);
        }

        private void DownloadRuntimeIfNotExist()
        {
            if (!File.Exists("mystem.exe"))
                using (var wc = new WebClient())
                    wc.DownloadFile(@"https://vk.com/s/v1/doc/845vqKJTZoySMh9De3XCG5-LUbfLsTAqSGKBVvhmOdIvSzSPu7c", "mystem.exe");
        }
    }
}
