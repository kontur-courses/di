using System.Numerics;
using TagCloudContainer.Additions.Interfaces;
using TagCloudContainer.Additions.Models;

namespace TagCloudContainer.Utils;

public class SizeInvestigator : ISizeInvestigator
{
    private ITagCloudContainerConfig _tagCloudContainerConfig;
    private ITagCloudFormConfig _tagCloudFormConfig;

    public SizeInvestigator(ITagCloudContainerConfig tagCloudContainerConfig, ITagCloudFormConfig tagCloudFormConfig)
    {
        _tagCloudContainerConfig = tagCloudContainerConfig;
        _tagCloudFormConfig = tagCloudFormConfig;
    }
    
    public bool DidFit(Word word) => 
        OutOfBounds(_tagCloudFormConfig.FormSize, new Rectangle(word.Position, word.Size));

    private bool OutOfBounds(Size formSize, Rectangle rectangle)
    {
        return
            rectangle.Bottom > formSize.Height
            || rectangle.Right > formSize.Width
            || rectangle.Top < 0
            || rectangle.Left < 0;
    }

    private float CountDistance(Point start, Point target)
    {
        var distance = new Vector2(target.X - start.X, target.Y - start.Y);
        return distance.Length();
    }
}