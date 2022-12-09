using TagsCloud.Core.Interfaces;

namespace TagsCloud.Core;
public class TempFilter : IWordFilter
{
	public IEnumerable<string> Filter(IEnumerable<string> words)
	{
		return words.Select(word => word.ToLower());
	}
}