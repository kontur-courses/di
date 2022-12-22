namespace TagCloudContainer.Additions.Interfaces;

public interface IWordValidator
{
    public IEnumerable<string> Validate(IEnumerable<string> lines);
}