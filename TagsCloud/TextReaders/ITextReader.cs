namespace TagsCloud.TextReaders;

public interface ITextReader
{
    public Tuple<string,int>[] GetWords(string filePath);
}