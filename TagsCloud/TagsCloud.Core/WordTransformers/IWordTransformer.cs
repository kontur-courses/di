namespace TagsCloud.Core.WordTransformers;

public interface IWordTransformer
{
	public string Transform(string word);

	public int Priority { get; }
}