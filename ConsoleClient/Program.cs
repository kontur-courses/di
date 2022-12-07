using System.Drawing;
using TagCloud;

Console.Write("Filename: ");
var filename = Console.ReadLine();
if (filename is null) throw new ArgumentException("Filename can't be null");
var words = new FileWordsLoader(filename).Load();
words = new FakeWordsProcessor().Process(words);
var drawer = new BaseCloudDrawer(new FontFamily("Arial"), 50, 10,
    new Size(800, 600), Color.Black);
var layouter = new CircularCloudLayouter(new Point(400, 300));
var bitmap = new DrawingCloudCreator(drawer, layouter).CreateTagCloud(words);
bitmap.Save("result.png");
