namespace TagCloud;

public class CloudProperties
{
    public IReadOnlyList<string> ExcludedWords = new List<string>();
    public Point Center { get; set; }
    public double Density { get; set; }
}