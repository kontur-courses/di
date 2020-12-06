namespace MyStem.Wrapper
{
    public interface IGrammarAnalyser
    {
        AnalysisResultRaw[] GetRawResult(string text);
    }
}