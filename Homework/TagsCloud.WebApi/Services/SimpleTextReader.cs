using TagsCloud.Visualization.WordsReaders;

namespace TagsCloud.WebApi.Services
{
    public class SimpleTextReader : IWordsReadService
    {
        private string words;

        public void SetText(string text)
        {
            words = text;
        }
        
        public string Read() => words;
    }
}