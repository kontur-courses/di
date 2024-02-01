namespace TagsCloudPainter.FileReader;

public class TextFileReader : IFormatFileReader<string>
{
    private readonly IEnumerable<IFileReader> fileReaders;

    public TextFileReader(IEnumerable<IFileReader> fileReaders)
    {
        this.fileReaders = fileReaders;
    }

    public string ReadFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException();

        var fileExtension = Path.GetExtension(path);
        var fileReader =
            fileReaders.FirstOrDefault(fileReader => fileReader.SupportedExtensions.Contains(fileExtension));

        return fileReader is not null
            ? fileReader.ReadFile(path)
            : throw new ArgumentException($"Incorrect file extension {fileExtension}. " +
                                          $"Supported file extensions: txt, doc, docx");
    }
}