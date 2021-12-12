using System.Drawing;

namespace TagsCloudContainer
{
    internal static class AppSettings
    {
        public static string TextFilename { get; set; } = "..\\..\\..\\Tags\\startTags.txt";
        public static string ImageFilename { get; set; } = "..\\..\\..\\images\\image1.jpg";

        public static Size ImageSize { get; set; }
        public static FontFamily FontFamily { get; set; }
        public static Color BackgroundColor { get; set; }
        public static float MinMargin { get; set; }
        public static bool FillTags { get; set; }
    }
}