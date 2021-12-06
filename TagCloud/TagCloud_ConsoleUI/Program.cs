namespace TagCloud_ConsoleUI
{
    public static class Program
    {
        public static void Main()
        {
            var tagCloud = new TagCloud.TagCloud();
            tagCloud.FromFile("dataSample_2.txt")
                .Draw();
        }
    }
}
