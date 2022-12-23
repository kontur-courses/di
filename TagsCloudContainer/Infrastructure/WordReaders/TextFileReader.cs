using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.WordReaders
{
    public class TextFileReader : IWordReader
    {
        private const string fileExtension = ".txt";

        public Result<string[]> TryReadWords(string filename)
        {
            if (!File.Exists(filename))
                return Result.Fail<string[]>($"File '{filename}' doesn't exist");

            string extension;
            if ((extension = Path.GetExtension(filename)) != fileExtension)
                return Result.Fail<string[]>($"File was wrong extension: should be: '{fileExtension}', but have '{extension}'");

            try
            {
                var wordList = new List<string>();
                using var stream = new StreamReader(filename);
                while (!stream.EndOfStream)
                    wordList.Add(stream.ReadLine()!);

                var words = wordList.ToArray();
                return Result.Ok(words);
            }
            catch(Exception e)
            {
                return Result.Fail<string[]>(new Error("Can't read words").CausedBy(e));
            }
        }
    }
}