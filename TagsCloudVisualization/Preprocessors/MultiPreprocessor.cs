namespace TagsCloudVisualization.Preprocessors;

public class MultiPreprocessor : IPreprocessor
{
    private readonly IReadOnlyCollection<IPreprocessor> preprocessors;

    public MultiPreprocessor(IReadOnlyCollection<IPreprocessor> preprocessors)
    {
        this.preprocessors = preprocessors;
    }

    public IEnumerable<string> Process(IEnumerable<string> text)
    {
        return preprocessors.Aggregate(text, (word, processor) => processor.Process(word));
    }
}