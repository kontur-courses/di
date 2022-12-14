using TagsCloud.Core.WordFilters;
using TagsCloud.Core.WordReaders;
using TagsCloud.Core.WordTransformers;

namespace TagsCloud.Core.TagContainersProviders.TagsPreprocessors;

public class TagPreprocessor : ITagsPreprocessor
{
	private readonly IWordFiltersComposer wordFiltersComposer;
	private readonly IWordReader wordReader;
	private readonly IWordTransformersComposer wordTransformersComposer;

	public TagPreprocessor(IWordReader wordReader, IWordFiltersComposer wordFiltersComposer,
		IWordTransformersComposer wordTransformersComposer)
	{
		this.wordReader = wordReader;
		this.wordFiltersComposer = wordFiltersComposer;
		this.wordTransformersComposer = wordTransformersComposer;
	}

	public IEnumerable<Tag> GetTags(int? count = null)
	{
		var tags = new Dictionary<string, int>();
		var words = wordFiltersComposer.Filter(wordReader.ReadWords());
		words = wordTransformersComposer.Transform(words);

		foreach (var word in words)
		{
			tags.TryAdd(word, 0);
			tags[word]++;
		}

		return tags
			.OrderByDescending(pair => pair.Value)
			.Take(count ?? tags.Count)
			.Select(pair => new Tag(pair.Key, pair.Value));
	}
}