using System;
using System.IO;
using TagsCloud.Interfaces;
using TagsCloud.PathValidators;

namespace TagsCloud.FileReader
{
    public class TxtReader : ITextReader
    {
        private readonly PathValidator pathValidator;

        public TxtReader(PathValidator pathValidator)
        {
            this.pathValidator = pathValidator;
        }

        public string ReadFile(string path)
        {
            if (!pathValidator.IsValidPath(path))
                throw new ArgumentException("File not exist");
            using (var sr = new StreamReader(path))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
