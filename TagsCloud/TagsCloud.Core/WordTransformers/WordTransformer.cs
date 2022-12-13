namespace TagsCloud.Core.WordTransformers;

public abstract class WordTransformer : IWordTransformer
{
	protected WordTransformer(int priority)
	{
		Priority = priority;
	}

	public abstract string Transform(string word);

	public int Priority { get; }
}