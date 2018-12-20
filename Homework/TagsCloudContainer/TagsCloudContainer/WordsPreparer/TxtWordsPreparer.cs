using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudBuilder
{
    public class TxtWordsPreparer : IWordsPreparer
    {
        private readonly string fileName;

        public TxtWordsPreparer(string fileName)
        {
            this.fileName = fileName;
        }

        public Dictionary<string, int> GetPreparedWords()
        {
            try
            {
                var wordsWithFrequency = new Dictionary<string, int>();

                foreach (var word in File.ReadAllLines(fileName).Where(word => word.Length > 0))
                {
                    var lowerWord = word.ToLower();
                    if (!wordsWithFrequency.ContainsKey(lowerWord))
                        wordsWithFrequency.Add(lowerWord, 1);
                    else
                        wordsWithFrequency[lowerWord] += 1;
                }

                return wordsWithFrequency;
            }
            catch (IOException e)
            {
                throw new IOException($"Something went wrong. Check the correctness of {fileName} path.", e);
            }
        }

        public static Dictionary<string, int> ReadAllLines(string fileName)
        {
            try
            {
                var wordsWithFrequency = new Dictionary<string, int>();

                foreach (var word in File.ReadAllLines(fileName))
                {
                    var lowerWord = word.ToLower();
                    if (!wordsWithFrequency.ContainsKey(lowerWord))
                        wordsWithFrequency.Add(lowerWord, 1);
                    else
                        wordsWithFrequency[lowerWord] += 1;
                }

                return wordsWithFrequency;
            }
            catch (IOException e)
            {
                throw new IOException($"Something went wrong. Check the correctness of {fileName} path.", e);
            }
        }
    }
}
