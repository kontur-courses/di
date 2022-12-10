using System.Drawing;

namespace TagCloud.CloudGenerator;

public interface ICloudGenerator
{
    public Image GenerateCloud(string filepath);
}