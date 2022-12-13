namespace TagsCloudContainer.Core.Options.Interfaces
{
    public interface IFilterOptions
    {
        public string? MyStemLocation { get; set; }

        public IEnumerable<string> BoringWords { get; set; }
    }
}
