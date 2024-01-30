using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Extensions.DependencyInjection;

namespace TagsCloudVisualization;

public class TagLayoutSettings
{
    public TagLayoutSettings(Algorithm algorithm, 
        HashSet<string> removedPartOfSpeech, 
        string? excludedWordsFile)
    {
        Algorithm = algorithm;
        RemovedPartOfSpeech = removedPartOfSpeech;
        ExcludedWordsFile = excludedWordsFile;
    }

    public Algorithm Algorithm { get; }
    public HashSet<string> RemovedPartOfSpeech { get; }
    public string? ExcludedWordsFile { get; }
    
}