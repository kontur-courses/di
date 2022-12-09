namespace TagCloudPainter.Interfaces;

public interface ITagParser
{
    Dictionary<string, int> ParseTags(string tag);
}