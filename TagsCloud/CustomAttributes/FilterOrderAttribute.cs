namespace TagsCloud.CustomAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class FilterOrderAttribute : Attribute
{
    public FilterOrderAttribute(int order)
    {
        Order = order;
    }

    public int Order { get; }
}