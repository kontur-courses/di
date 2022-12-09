
using CircularCloudLayouter.Domain;

namespace CircularCloudLayouter.WeightedLayouter.SideLayouters;

public interface ISideLayouter
{
    double CalculateCoefficient();
    ImmutableRectangle GetNextRectangle(ImmutableSize rectSize);
    public void UpdateWeights(ImmutableRectangle rect);
}