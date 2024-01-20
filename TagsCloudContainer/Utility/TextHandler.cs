using Spire.Doc;

namespace TagsCloudContainer.utility;

public static class TextHandler
{
    public static string ReadText(string fileName)
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