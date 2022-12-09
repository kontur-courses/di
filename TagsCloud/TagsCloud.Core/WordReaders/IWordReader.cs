namespace TagsCloud.Core.WordReaders;

public interface IWordReader
{
	public IEnumerable<string> ReadWords();
}