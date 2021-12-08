
namespace TagsCloudContainer.Registrations;

[AttributeUsage(AttributeTargets.Method)]
public class RegisterAttribute : Attribute
{
    public bool IsKeyed { get; set; } = false;
}