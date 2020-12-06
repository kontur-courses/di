using System.Drawing;

namespace TagsCloudLayouters.Contracts
{
    public interface IColorizer
    {
        public string Name { get; }
        public Color Paint();
    }
}