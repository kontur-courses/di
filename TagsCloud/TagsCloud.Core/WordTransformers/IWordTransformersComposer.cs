namespace TagsCloud.Core.WordTransformers;

public interface IWordTransformersComposer
{
	public IEnumerable<string> Transform(IEnumerable<string> words);
}