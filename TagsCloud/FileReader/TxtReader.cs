using System;
using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud.FileReader
{
    public class TxtReader : IFileReader
    {
        private readonly IPathValidator pathValidator;

        public TxtReader(IPathValidator pathValidator)
        {
            this.pathValidator = pathValidator;
        }

        public string ReadFile(string path)
        {
            if (!pathValidator.ValidatePath(path))
                throw new ArgumentException("File not exist");
            using (var sr = new StreamReader(path))
            {
                return sr.ReadToEnd();
            }        
        }
    }
}
