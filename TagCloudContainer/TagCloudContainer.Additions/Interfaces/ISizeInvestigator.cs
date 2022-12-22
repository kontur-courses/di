using TagCloudContainer.Additions.Models;

namespace TagCloudContainer.Additions.Interfaces;

public interface ISizeInvestigator
{
    public bool DidFit(Word word);
}