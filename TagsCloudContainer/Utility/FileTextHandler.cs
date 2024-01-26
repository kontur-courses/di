using Spire.Doc;

namespace TagsCloudContainer.utility;

public class FileTextHandler(string fileName) : ITextHandler
{
    public string ReadText()
    {
        if (fileName.Contains("doc"))
        {
            var document = new Document();
            document.LoadFromFile(Utility.GetRelativeFilePath($"src/{fileName}"));
            return document.GetText();
        }
        
        return File.ReadAllText(Utility.GetRelativeFilePath($"src/{fileName}"));
    }
}