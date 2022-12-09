namespace TagsCloud.Core.Interfaces;

public interface IWordFilter
{
	public IEnumerable<string> Filter(IEnumerable<string> words);
}