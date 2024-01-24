namespace TagsCloud.CustomAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class ColorizerNameAttribute : Attribute
{
    public ColorizerNameAttribute(string name)
    {
        Name = name;
    }

    public string? Name { get; }
}