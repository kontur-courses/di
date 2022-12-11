namespace TagCloud.Abstractions;

public interface IWordsTagger
{
    IEnumerable<ITag> ToTags(IEnumerable<string> words);
}