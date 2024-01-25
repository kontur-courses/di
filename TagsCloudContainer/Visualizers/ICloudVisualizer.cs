using System.Drawing;

namespace TagsCloudContainer.Visualizers;

public interface ICloudVisualizer
{
    public Image GenerateImage();
    
    public void SaveImage();

    public void VisualizeTag(Tag tag);

    public void VisualizeTags(IEnumerable<Tag> tags);
}