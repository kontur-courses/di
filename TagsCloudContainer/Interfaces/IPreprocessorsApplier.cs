namespace TagsCloudContainer.Interfaces;

public interface IPreprocessorsApplier
{
    IEnumerable<string> ApplyPreprocessors(IEnumerable<string> words);
}
