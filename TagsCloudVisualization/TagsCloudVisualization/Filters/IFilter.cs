namespace TagsCloudVisualization.Filters
{
    interface  IFilter
    {
        bool IsValidValue(string value, string valueForFilte);
    }
}
