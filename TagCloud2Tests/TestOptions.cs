using TagCloud2;

namespace TagCloud2Tests
{
    public class TestOptions : IOptions
    {
        public string Path { get; set; }
        public string InputFormat { get; set; }
        public string OutputFormat { get; set; }
        public string OutputName { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double LinearSpeed { get; set; }
        public double AngleSpeed { get; set; }
        public string ExcludePath { get; set; }
        public string ColoringMode { get; set; }
        public string BoringMode { get; set; }

        public void SetToDefault()
        {
            InputFormat = "txt";
            OutputFormat = "bitmap";
            OutputName = "output.bmp";
            FontName = "Arial";
            FontSize = 12;
            X = 1000;
            Y = 1000;
            LinearSpeed = 0.032;
            AngleSpeed = 0.108;
            ColoringMode = "bw";
            BoringMode = "short";
        }
    }
}
