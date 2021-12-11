using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Infrastructure
{
    public class CloudSettings
    {
        public ITagPainter Painter { get; set; }
        public ISpiral Spiral { get; set; }
    }
}
