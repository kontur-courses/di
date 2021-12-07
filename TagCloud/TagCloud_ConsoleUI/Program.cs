using TagCloud;

namespace TagCloud_ConsoleUI
{
    public static class Program
    {
        public static void Main()
        {
            var tagCloud = new TagCloud.TagCloud(new DefaultDrawerSettings(), new DefaultTextProcessingSettings());
            tagCloud.FromFile("dataSample_4.txt")
                .Draw();
        }
    }
}
