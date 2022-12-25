using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Core.Models;

namespace TagCloudContainer.Core.Utils;

public class SizeInvestigator : ISizeInvestigator
{
    private ITagCloudContainerConfig _tagCloudContainerConfig;

    public SizeInvestigator(ITagCloudContainerConfig tagCloudContainerConfig)
    {
        _tagCloudContainerConfig = 
            tagCloudContainerConfig ?? throw new ArgumentNullException("Tag cloud config can't be null");
    }
    
    public bool DidFit(Word word) => 
        OutOfBounds(_tagCloudContainerConfig.ImageSize, new Rectangle(word.Position, word.Size));

    private bool OutOfBounds(Size formSize, Rectangle rectangle)
    {
        return
            rectangle.Bottom > formSize.Height
            || rectangle.Right > formSize.Width
            || rectangle.Top < 0
            || rectangle.Left < 0;
    }
}