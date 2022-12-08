using System.Drawing;

namespace TagsCloud
{
    public class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\User\source\repos\DI\di\text.txt";
           
            var printSettings = new PrintSettings();
            printSettings.SetFont("Consolas", 64);
            printSettings.SetCentralPen(Color.White, 8);
            printSettings.SetSurroundPen(Color.FromArgb(249, 100, 0), 4);
            printSettings.SetBackgroudColor(Color.FromArgb(0, 34, 43));

            var cloud = new TagCloud(printSettings, path);
            cloud.PrintTagCloud(@"C:\Users\User\source\repos\DI\di\pictupe", @".png");
        }
    }
}
