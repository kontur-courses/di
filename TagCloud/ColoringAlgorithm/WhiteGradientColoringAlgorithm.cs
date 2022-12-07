using System.Drawing;

namespace TagCloud.ColoringAlgorithm;

public class WhiteGradientColoringAlgorithm : IColoringAlgorithm
{
    private readonly Color color;
    private readonly int stepCount;
    private int currentStep;

    public WhiteGradientColoringAlgorithm(Color color, int stepCount)
    {
        this.color = color;
        this.stepCount = stepCount;
    }
    
    public Color GetNextColor()
    {
        var redCurrent = color.R + (Color.White.R - color.R) * currentStep / stepCount;
        var greenCurrent = color.G + (Color.White.G - color.G) * currentStep / stepCount;
        var blueCurrent = color.B + (Color.White.B - color.B) * currentStep / stepCount;

        currentStep++;
        return Color.FromArgb(redCurrent, greenCurrent, blueCurrent);
    }
}