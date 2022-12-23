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
            return Result.OkIf(File.Exists(filename), $"File '{filename}' doesn't exist")
                         .Bind(() => ValidateFileExtension(filename))
                         .Bind(() => Result.Try(() => ReadWords(filename), e => new Error("Can't read words").CausedBy(e)));
        }

        private string[] ReadWords(string filename)
        {
            return File.ReadAllLines(filename);
        }

        private static Result ValidateFileExtension(string filename)
        {
            string extension;
            return Result.OkIf((extension = Path.GetExtension(filename)) == fileExtension, $"File was wrong extension: should be: '{fileExtension}', but have '{extension}'");
        }
    }
}