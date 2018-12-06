using System;
using System.Drawing;
using System.Threading;
using TagsCloudContainer.CircularCloudLayouter;


namespace TagsCloudContainer
{
    class Program
    {

        // Временный пример с некоторыми настройками
        static void Main(string[] args)
        {
            var wordsReader = new WordsReader();
            var filePath = "..\\..\\tmpTextFile";
            var wordStorage = wordsReader.ReadWords(filePath + ".txt", new WordStorage(new WordsCustomizer()));

            var layout = new WordLayouter(
                w => new Size(w.Count * 20 * w.Value.Length, w.Count * 20),
                new CircularCloudLayout(new RectangleStorage(new Point(), new Direction(1))));

            var settings = new DrawSettings<Word>(filePath);
            settings.SetImageSize(new Size(1000, 500));
            settings.SetItemPainter(i => TakeRandomColor());

            var itemsToDraw = layout.GetItemsToDraws(wordStorage);
            var drawer = new Drawer(settings);
            drawer.DrawItems(itemsToDraw);
        }

        private static Color TakeRandomColor()
        {
            var rnd = new Random();
            Thread.Sleep(20);
            var color = Color.FromArgb(rnd.Next());

            switch (rnd.Next() % 4)
            {
                case 0:
                    color = Color.Green;
                    break;
                case 1:
                    color = Color.Red;
                    break;
                case 2:
                    color = Color.Gold;
                    break;
                case 3:
                    color = Color.Aqua;
                    break;
                default:
                    color = Color.BlueViolet;
                    break;
            }

            return color;
        }
    }
}
