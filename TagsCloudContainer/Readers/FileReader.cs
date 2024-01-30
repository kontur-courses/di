using System.IO;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Readers;

namespace TagsCloudContainer.TagsCloud
{
    public class FileReader
    {
        public IEnumerable<string> ReadFile(string filePath)
        {
            var fileReader = GetFileReader(filePath);
            return fileReader.ReadWords(filePath);
        }

        private IFileReader GetFileReader(string filePath)
        {
            FileType fileType = GetFileType(filePath);

            switch (fileType)
            {
                case FileType.Doc:
                    return new DocReader();
                case FileType.Docx:
                    return new DocxReader();
                case FileType.Txt:
                    return new TxtReader();
                default:
                    throw new InvalidOperationException("Unsupported file extension");
            }
        }

        private FileType GetFileType(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath)?.ToLower();

            switch (fileExtension)
            {
                case ".doc":
                    return FileType.Doc;
                case ".docx":
                    return FileType.Docx;
                case ".txt":
                    return FileType.Txt;
                default:
                    throw new InvalidOperationException("Unsupported file extension");
            }
        }
    }
}
