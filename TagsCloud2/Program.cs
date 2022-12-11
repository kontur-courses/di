using System.Drawing;
using TagsCloud2;
using TagsCloud2.Lemmatizer;
using TagsCloud2.Manager;
using TagsCloudVisualization;

class Program
{
    static void Main(string[] args)
    {
        var mystemExePath = @"D:\шпора-2022\di\TagsCloud2\Lemmatizer\mystem.exe";
        var imageSaver = new ImageSaver();
        var frequencyCompiler = new FrequencyCompiler();
        var lemmatizer = new Lemmatizer(mystemExePath);
        var reader = new Reader();
        var tagsCloudMaker = new TagsCloudMaker();
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var bitmapMaker = new BitmapMaker(layouter);
        var sizeDefiner = new SizeDefiner();

        var manager = new ConsoleManager(reader, lemmatizer, frequencyCompiler, imageSaver,
            tagsCloudMaker, bitmapMaker, sizeDefiner);

        manager.Manage();

        //D:\\inputWords.txt
    }
}