namespace TagsCloud.Core.WordTransformers;

public class WordTransformersComposer : IWordTransformersComposer
{
	private readonly IOrderedEnumerable<IWordTransformer> transformers;

	public WordTransformersComposer(IEnumerable<IWordTransformer> transformers)
	{
		this.transformers = transformers.OrderBy(transformer => transformer.Priority);
	}

	public IEnumerable<string> Transform(IEnumerable<string> words)
	{
		return words.Select(ApplyTransform);
	}

	private string ApplyTransform(string word)
	{
		return transformers.Aggregate(word, (current, wordTransformer) => wordTransformer.Transform(current));
	}
}