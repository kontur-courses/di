using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;

namespace TagsCloudGenerator.WordsHandler.Converters
{
    public class InitialFormConverter : IConverter
    {
        public Dictionary<string, int> Convert(Dictionary<string, int> wordToCount)
        {
            var converted = new Dictionary<string, int>();

            try
            {
                using (var hunspell = new Hunspell(@"en-GB/index.aff", @"en-GB/index.dic"))
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
                Console.WriteLine("failed to convert words to initial form: "+ e.Message);
                return wordToCount;
            }

            return converted;
        }
    }
}