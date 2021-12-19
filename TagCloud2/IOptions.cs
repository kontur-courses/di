namespace TagCloud2
{
    public interface IOptions
    {
        string Path { get; set; }

        string InputFormat { get; set; }

        string OutputFormat { get; set; }

        string OutputName { get; set; }

        string FontName { get; set; }

        int FontSize { get; set; }

        int X { get; set; }

        int Y { get; set; }

        double LinearSpeed { get; set; }

        double AngleSpeed { get; set; }

        string ExcludePath { get; set; }

        string ColoringMode { get; set; }

        string BoringMode { get; set; }
    }
}
