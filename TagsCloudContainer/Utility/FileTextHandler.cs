using Spire.Doc;

namespace TagsCloudContainer.utility;

public class FileTextHandler(string filePath) : ITextHandler
{
    public string ReadText()
    {
        if (filePath.Contains("doc"))
        {
            var document = new Document();
            document.LoadFromFile(filePath);
            return document.GetText();
        }
        
        return File.ReadAllText(filePath);
    }
}