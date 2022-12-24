using TagCloudContainer.Core.Models;

namespace TagCloudContainer.Core.Interfaces;

public interface ISizeInvestigator
{
    public bool DidFit(Word word);
}