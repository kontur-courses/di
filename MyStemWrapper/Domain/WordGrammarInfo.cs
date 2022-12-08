namespace MyStemWrapper.Domain;

public class WordGrammarInfo
{
    public string SourceWord { get; }
    public string OriginalForm { get; }
    public SpeechPart SpeechPart { get; }

    public WordGrammarInfo(string sourceWord, string originalForm, SpeechPart speechPart)
    {
        SourceWord = sourceWord;
        OriginalForm = originalForm;
        SpeechPart = speechPart;
    }
}