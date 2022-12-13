namespace TagsCloud.Core.WordReaders;

public class WordReaderFromTxt : IWordReader
{
	public WordReaderFromTxt(string path)
	{
		this.path = path;
	}

	private readonly string path;

	public IEnumerable<string> ReadWords()
	{
		return File.ReadLines(path);
	}
}