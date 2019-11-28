using System;
using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud.FileReader
{
    class TxtReader : IFileReader
    {
        private IPathValidator pathValidator;

        public TxtReader(IPathValidator pathValidator)
        {
            this.pathValidator = pathValidator;
        }

        public FileStream ReadFile(string path)
        {
            if (pathValidator.ValidatePath(path))
                return new FileStream(path, FileMode.Open);
            throw new ArgumentException("File not exist");
        }
    }
}
