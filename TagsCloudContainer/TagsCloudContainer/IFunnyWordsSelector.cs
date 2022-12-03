namespace TagsCloudContainer;

public interface IFunnyWordsSelector
{
    IReadOnlyCollection<CloudWord> RecognizeFunnyCloudWords(IReadOnlyCollection<string> allWords);
}