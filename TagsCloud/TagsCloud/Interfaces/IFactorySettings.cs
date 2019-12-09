namespace TagsCloudGenerator.Interfaces
{
    public interface IFactorySettings : IResettable
    {
        string PainterId { get; set; }
        string SaverId { get; set; }
        string PointsSearcherId { get; set; }
        string WordsParserId { get; set; }
        string WordsLayouterId { get; set; }
        string[] WordsFiltersIds { get; set; }
        string[] WordsConvertersIds { get; set; }
    }
}