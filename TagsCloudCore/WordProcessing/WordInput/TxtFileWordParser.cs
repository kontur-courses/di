using TagsCloudCore.Common.Enums;

namespace TagsCloudCore.WordProcessing.WordInput;

public class TxtFileWordParser : IWordProvider
{
    public string[] GetWords(string resourceLocation)
    {
        string[] line;
        try
        {
            line = File.ReadAllLines(resourceLocation);
        }
        catch (Exception e)
        {
            throw new IOException(
                $"Failed to read from file {resourceLocation} Most likely the file path is incorrect or the file is corrupted.",
                e);
        }

        return line;
    }

    public WordProviderType Info => WordProviderType.Txt;
}