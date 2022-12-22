namespace TagCloudContainer.Additions.Interfaces;

public interface IWordReaderConfig
{
    public string FilePath { get; set; }
    public string ExcludeWordsFilePath { get; set; }
    public bool NeedValidate { get; set; }

    public void SetFilePath(string fileName);

    public void SetExcludeWordsFilePath(string fileName);
}