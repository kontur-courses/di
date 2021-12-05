using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Infrastructure
{
    public class Settings
    {
        public Palette Palette { get; set; }
        public Font TextFont { get; set; }
        public Size ImageSize { get; set; }
        public IPreprocessor[] Preprocessors { get; set; }
    }
}
