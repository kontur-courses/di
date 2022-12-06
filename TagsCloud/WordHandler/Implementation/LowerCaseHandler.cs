namespace TagsCloud.WordHandler.Implementation;

public class LowerCaseHandler : IWordHandler
{
    public string[] ProcessWords(IEnumerable<string> words) => words.Select(ProcessWord).ToArray();

    public string ProcessWord(string word) => word.ToLower();
}