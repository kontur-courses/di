namespace TagCloudContainer.Core.Interfaces;

public interface IWordValidator
{
    public IEnumerable<string> Validate(IEnumerable<string> lines);
}