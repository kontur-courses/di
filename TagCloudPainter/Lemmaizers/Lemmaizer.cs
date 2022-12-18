using MystemHandler;

namespace TagCloudPainter.Lemmaizers;

public class Lemmaizer : ILemmaizer
{
    private readonly MystemMultiThread mt;
    public Mystem.Net.Mystem mystem;

    public Lemmaizer(string pathToMyStem)
    {
        mt = new MystemMultiThread(1, pathToMyStem);
        mystem = new Mystem.Net.Mystem();
    }

    public string GetMorph(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            throw new ArgumentNullException();

        return mystem.Mystem.Analyze(word).Result[0].AnalysisResults[0].Grammeme.Split(',', '=')[0];
    }

    public string GetLemma(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            throw new ArgumentNullException();

        return mt.StemOneWord(word);
    }
}