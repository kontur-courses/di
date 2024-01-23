using TagsCloud.Entities;

namespace TagsCloud.Contracts;

public interface IWordFilter
{
    public List<string> GetFilteredResult(List<string> words);
}