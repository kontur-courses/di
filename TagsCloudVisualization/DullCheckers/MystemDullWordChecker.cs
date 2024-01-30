using System.Text;

namespace TagsCloudVisualization;

public class MystemDullWordChecker : IDullWordChecker
{
    private HashSet<string> removedPartOfSpeech;
    private HashSet<string> excludedWords = new();

    public MystemDullWordChecker(TagLayoutSettings tagLayoutSettings)
    {
        removedPartOfSpeech = tagLayoutSettings.RemovedPartOfSpeech;
        var excludedWordsFile = tagLayoutSettings.ExcludedWordsFile;
        if (excludedWordsFile is null)
            return;

        try
        {
            excludedWords =
                new HashSet<string>(File.ReadAllText(excludedWordsFile, Encoding.UTF8).Split(Environment.NewLine));
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"Could not find specified excluded words file {excludedWordsFile}. " +
                              $"No words will be excluded.");
        }
    }

    public bool Check(WordAnalysis wordAnalysis)
    {
        return removedPartOfSpeech.Any(dullPart => wordAnalysis.GrammarAnalysis.StartsWith(dullPart))
               || excludedWords.Contains(wordAnalysis.Lexema.ToLower());
    }
}