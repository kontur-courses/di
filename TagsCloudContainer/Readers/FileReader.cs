using System.ComponentModel;
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

        static FileType GetFileType(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath)?.ToLower();

            var fileTypes = Enum.GetValues(typeof(FileType)).Cast<FileType>();

            foreach (var fileType in fileTypes)
            {
                var descriptionAttribute = GetEnumDescription(fileType);
                if (descriptionAttribute != null && descriptionAttribute.Equals(fileExtension))
                {
                    return fileType;
                }
            }

            throw new InvalidOperationException("Unsupported file extension");
        }

        static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return attribute?.Description;
        }
    }
}
