namespace TagsCloud.CustomAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class SupportedExtensionAttribute : Attribute
{
    public string FileExtension { get; }

    public SupportedExtensionAttribute(string fileExtension)
    {
        FileExtension = fileExtension;
    }
}