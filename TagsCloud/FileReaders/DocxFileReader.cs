using TagsCloud.Contracts;
using Xceed.Words.NET;

namespace TagsCloud.FileReaders;

// TODO: Implement this reader in future
public class DocxFileReader : IFileReader
{
    public string SupportedExtension => "docx";

    public IEnumerable<string> ReadContent(string filename, IPostFormatter postFormatter = null)
    {
        using var document = DocX.Load(filename);
        var paragraphs = document.Paragraphs;

        return paragraphs.Select(para => postFormatter is null ? para.Text : postFormatter.Format(para.Text));
    }
}