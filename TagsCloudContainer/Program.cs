using System.Linq;
using TagsCloudContainer.WordProcessing;
using TagsCloudContainer.WordProcessing.Converting;
using TagsCloudContainer.WordProcessing.Filtering;
using TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying;
using TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying.CommandsExecuting;
using TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying.MyStem;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var ps = new WordProcessor(new ToLowerWordConverter(),
                new ExcludingBoringWordsFilter(new MyStemPartOfSpeechQualifier(new CmdCommandExecutor(),
                    new MyStemResultParser())));
            var result = ps.ProcessWords(new[] {"мои", "слова", "сейчас", "к", "месту"}).ToArray();
        }
    }
}
