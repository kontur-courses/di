namespace TagCloud;

public class CloudProperties
{
    public Point Center { get; set; }
    public double Density { get; set; }
    public List<string>? ExcludedWords = new();
}