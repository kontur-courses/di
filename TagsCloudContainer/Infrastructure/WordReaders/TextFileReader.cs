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

        public Result TryReadWords(string filename, out string[] words)
        {
            words = Array.Empty<string>();

            if (!File.Exists(filename))
                return new Result() { Success = false, Message = $"'{filename}' doesn't exist" };

            string extension;
            if ((extension = Path.GetExtension(filename)) != fileExtension)
                return new Result() { Success = false, Message = $"File was wrong extension: should be: '{fileExtension}', but have '{extension}'" };

            try
            {
                var wordList = new List<string>();
                using var stream = new StreamReader(filename);
                while (!stream.EndOfStream)
                    wordList.Add(stream.ReadLine()!);

                words = wordList.ToArray();
                return Result.Ok;
            }
            catch(Exception e)
            {
                return new Result() { Success = false, Message = e.Message };
            }
            
        }
    }
}