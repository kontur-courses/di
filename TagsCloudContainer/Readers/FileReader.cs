using System.IO;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Readers;
using TagsCloudContainer.Utility;

namespace TagsCloudContainer.TagsCloud
{
    public class FileReader
    {
        public Result<IEnumerable<string>> ReadFile(string filePath)
        {
            var fileReaderResult = GetFileReader(filePath);

            if (fileReaderResult.IsSuccess)
            {
                var fileReader = fileReaderResult.Value;
                return fileReader.ReadWords(filePath);
            }
            else
            {
                return Result<IEnumerable<string>>.Failure(fileReaderResult.ErrorMessage);
            }
        }

        private Result<IFileReader> GetFileReader(string filePath)
        {
            try
            {
                FileType fileType = GetFileType(filePath);

                switch (fileType)
                {
                    case FileType.Doc:
                        return Result<IFileReader>.Success(new DocReader());
                    case FileType.Docx:
                        return Result<IFileReader>.Success(new DocxReader());
                    case FileType.Txt:
                        return Result<IFileReader>.Success(new TxtReader());
                    default:
                        return Result<IFileReader>.Failure("Unsupported file extension");
                }
            }
            catch (Exception ex)
            {
                return Result<IFileReader>.Failure($"Error getting file reader: {ex.Message}");
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
