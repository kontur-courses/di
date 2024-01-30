using TagsCloudContainer.FileReader.Interfaces;

namespace TagsCloudContainer.FileReader;

public class FileReaderFactory
{
    public ITextReader GetReader(string filePath)
    {
        var fileExtension = Path.GetExtension(filePath);
        return fileExtension switch
        {
            ".docx" => new DocxFileReader(),
            ".txt" => new TxtFileReader(),
            _ => throw new ArgumentException($"Неверный формат файла: {fileExtension}")
        };
    }
}