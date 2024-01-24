namespace TagsCloud.CustomAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class SupportedExtensionAttribute : Attribute
{
    public SupportedExtensionAttribute(string fileExtension)
    {
        FileExtension = fileExtension;
    }

    public string FileExtension { get; }
}