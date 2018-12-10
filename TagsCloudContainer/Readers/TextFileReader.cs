using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.Readers
{
    public class TextFileReader: IEnumerable<string>
    {
        private readonly List<string> words;

        public TextFileReader(string nameFile)
        {
            try
            {
                words = File.ReadAllLines(nameFile).ToList();
            }
            catch (Exception exception)
            {
                throw new ArgumentException($"This text file ({nameFile}) is incorrect");
            }
        }

        public IEnumerator<string> GetEnumerator()
            => words.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}