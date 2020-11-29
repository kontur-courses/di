using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudContainer
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var writer = new TextWriter();
            var root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            var path = $"{root}\\TagsCloudContainer\\Texts";
            writer.WriteText(File.ReadAllText($"{path}\\Song.txt"), $"{path}\\ParsedSong.txt");

            var stopWords = new HashSet<string> {"a", "not", "to", "an", "are"};
            var parser = new TextParser(stopWords);
            var wordsEntry = parser.GetParsedText(File.ReadAllText($"{path}\\ParsedSong.txt"));
            var center = new Point(200, 200);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TagsCloudForm(wordsEntry, new CircularCloudLayouter(center)));
        }
    }
}