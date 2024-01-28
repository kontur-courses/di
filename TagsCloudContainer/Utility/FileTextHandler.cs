using Spire.Doc;

namespace TagsCloudContainer.utility;

public class FileTextHandler : ITextHandler
{
    public string ReadText(string filePath)
    {
        if (filePath.Contains("doc"))
        {
            var document = new Document();
            document.LoadFromFile(filePath);
            var text = document.GetText();
            return text[(text.IndexOf('\n') + 1)..].Trim();
        }

        return File.ReadAllText(filePath);
    }
}