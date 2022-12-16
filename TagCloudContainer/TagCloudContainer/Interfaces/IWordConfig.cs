namespace TagCloudContainer;

public interface IWordConfig
{
    public IEnumerable<string> Validate(IEnumerable<string> lines);
}