namespace TagCloudContainer.Additions.Interfaces;

public interface IWordConfig
{
    public IEnumerable<string> Validate(IEnumerable<string> lines);
}