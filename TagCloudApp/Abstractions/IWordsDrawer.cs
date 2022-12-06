namespace TagCloudApp.Abstractions;

public interface IWordsDrawer
{
    public Size GetRectSizeFor(string word);
    
}