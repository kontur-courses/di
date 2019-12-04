namespace TagsCloudVisualization.Filters
{
    interface  IFilter
    {
        (bool isValid, string value) Filter(string stemmedString);
    }
}
