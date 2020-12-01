using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization
{
    public class TxtWordProvider : IWordProvider
    {
        private readonly IWordsCleaner wordsCleaner;
        
        public TxtWordProvider(IWordsCleaner wordsCleaner)
        {
            this.wordsCleaner = wordsCleaner;
        }
        
        public List<string> GetWords(string filepath)
        {
            var words = new List<string>();
            
            if(!File.Exists(filepath))
                throw new FileNotFoundException();
            
            using (var sr = new StreamReader(filepath))
            {
                string word;
                while ((word = sr.ReadLine()) != null)
                {
                    words.AddRange(word.Split(' '));//TODO Rewrite normal word parse
                }
            }

            return wordsCleaner.CleanWords(words);
        }
    }
}