namespace TagCloudPainter.Lemmaizers;

public interface ILemmaizer
{
    public string GetMorph(string word);
    public string GetLemma(string word);
}