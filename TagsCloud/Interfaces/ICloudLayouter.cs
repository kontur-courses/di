using System.Drawing;

namespace TagsCloud.Interfaces
{
	public interface ICloudLayouter
	{
		Rectangle PlaceNextRectangle(Size rectangleSize);
		void ResetState();
	}
}