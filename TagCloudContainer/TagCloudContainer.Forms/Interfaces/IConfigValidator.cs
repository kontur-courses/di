using TagCloudContainer.Core;

namespace TagCloudContainer.Forms.Interfaces;

public interface IConfigValidator<T>
{
    public Result<T> Validate(T config);
}