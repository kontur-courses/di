using System.Drawing.Imaging;
using System.IO;
using TagCloud;

namespace ConsoleTagClouder
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var text = File.ReadAllText($"C:\\Users\\Avel\\Desktop\\hamlet.txt");
            var clouder = Clouder.CreateDefault();
            clouder.UpdateWith(text);
            using (var map = clouder.DrawCloud())
                map.Save(".\\btm.png",ImageFormat.Png);
        }
    }
}