namespace TagsCloudContainer.Interfaces;

public interface IFunnyWordsSelector
{
    IReadOnlyCollection<CloudWord> RecognizeFunnyCloudWords(IReadOnlyCollection<string> allWords);
}