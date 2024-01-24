using TagsCloud.Entities;

namespace TagsCloud.CustomAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class ColorizerNameAttribute : Attribute
{
    public ColorizerNameAttribute(ColoringStrategy strategy)
    {
        Strategy = strategy;
    }

    public ColoringStrategy Strategy { get; }
}