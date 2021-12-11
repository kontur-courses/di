namespace TagsCloudContainer.Interfaces;

public interface IFiltersApplier
{
    IEnumerable<string> ApplyFilters(IEnumerable<string> words);
}
