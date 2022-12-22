using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer;

public class WordReaderConfig : IWordReaderConfig
{
    public string FilePath { get; set; } = Path.Combine(GetMainDirectoryPath(), "words.txt");
    public string ExcludeWordsFilePath { get; set; } = Path.Combine(GetMainDirectoryPath(), "boring_words.txt");
    public bool NeedValidate { get; set; } = true;
    
    public void SetFilePath(string fileName)
    {
        FilePath = Path.Combine(GetMainDirectoryPath(), fileName);
    }
    
    public void SetExcludeWordsFilePath(string fileName)
    {
        ExcludeWordsFilePath = Path.Combine(GetMainDirectoryPath(), fileName);
    }

    public static string GetMainDirectoryPath()
    {
        return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
    }
}