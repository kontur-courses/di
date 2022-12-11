using TagCloud.Abstractions;

namespace TagCloud;

public class FuncWordsProcessor : IWordsProcessor
{
    private readonly Func<IEnumerable<string>, IEnumerable<string>> process;

    public FuncWordsProcessor(Func<IEnumerable<string>, IEnumerable<string>> process)
    {
        this.process = process;
    }

    public IEnumerable<string> Process(IEnumerable<string> words)
    {
        return process(words);
    }
}