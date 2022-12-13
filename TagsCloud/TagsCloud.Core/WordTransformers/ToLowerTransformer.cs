namespace TagsCloud.Core.WordTransformers;

public class ToLowerTransformer : WordTransformer
{
	public ToLowerTransformer(int priority) : base(priority)
	{
	}

	public override string Transform(string word)
	{
		return word.ToLower();
	}
}