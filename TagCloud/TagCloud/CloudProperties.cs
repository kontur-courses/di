namespace TagCloud;

public class CloudProperties
{
    public List<string>? ExcludedWords = new();
    public Point Center { get; set; }
    public double Density { get; set; }
}