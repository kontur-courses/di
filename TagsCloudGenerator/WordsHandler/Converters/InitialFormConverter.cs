using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;

namespace TagsCloudGenerator.WordsHandler.Converters
{
    public class InitialFormConverter : IConverter
    {
        private readonly string pathToAff;
        private readonly string pathToDictionary;

        public InitialFormConverter(string pathToAff, string pathToDictionary)
        {
            this.pathToAff = pathToAff;
            this.pathToDictionary = pathToDictionary;
        }

        public Dictionary<string, int> Convert(Dictionary<string, int> wordToCount)
        {
            if (wordToCount == null)
                throw new ArgumentNullException();

            var converted = new Dictionary<string, int>();

            try
            {
                using (var hunspell = new Hunspell(pathToAff, pathToDictionary))
                {
                    foreach (var word in wordToCount)
                    {
                        var stem = hunspell.Stem(word.Key).LastOrDefault();

                        if (stem == null)
                            continue;

                        if (!converted.ContainsKey(stem))
                            converted.Add(stem, 0);

                        converted[stem] += word.Value;
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("failed to convert words to initial form: " + e.Message);
                return wordToCount;
            }

            return converted;
        }
    }
}