using System.Text;

namespace TagsCloudVisualization;

public class MystemDullWordChecker : IDullWordChecker
{
    private HashSet<string> removedPartOfSpeech;
    private HashSet<string> excludedWords;

    public MystemDullWordChecker(HashSet<string> removedPartOfSpeech, string excludedWordsFile)
    {
        this.removedPartOfSpeech = removedPartOfSpeech;
        excludedWords =
            new HashSet<string>(File.ReadAllText(excludedWordsFile, Encoding.UTF8).Split(Environment.NewLine));
    }

    public bool Check(WordAnalysis wordAnalysis)
    {
        return removedPartOfSpeech.Any(dullPart => wordAnalysis.GrammarAnalysis.StartsWith(dullPart))
               || excludedWords.Contains(wordAnalysis.Lexema.ToLower());
    }
}