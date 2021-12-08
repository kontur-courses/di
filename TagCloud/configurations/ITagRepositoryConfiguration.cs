using System.Drawing;

namespace TagCloud.configurations
{
    public interface ITagRepositoryConfiguration
    {
        float GetSize();
        Color GetColor();
        FontFamily GetFamilyFont();
    }
}