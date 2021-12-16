namespace TagsCloudVisualization.WordProcessors
{
    public interface IWordByLineProcessor : IWordProcessor
    {
    }
    
    public interface ILiteraryWordProcessor : IWordProcessor
    {
    }
    
    public interface IWordProcessor
    {
        string ProcessWord(string word);
    }
}