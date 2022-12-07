using System.Drawing;

namespace CircularCloudLayouter.WeightedLayouter.SideLayouters;

public interface ISideLayouter
{
    double CalculateCoefficient();
    Rectangle GetNextRectangle(Size rectSize);
    public void UpdateWeights(Rectangle rect);
}