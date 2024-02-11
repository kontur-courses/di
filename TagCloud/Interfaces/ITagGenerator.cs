namespace TagCloud.Interfaces;

public interface ITagGenerator
{
    List<Tag> Generate(List<string> words);
}