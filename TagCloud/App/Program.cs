using Autofac;
using TagCloud;
using TagsCloudLayouter;

namespace App;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var container = DiContainerBuilder.Build();
        var app = new ConsoleApplication.ConsoleApplication();
        app.Run(container, args);

        var drawer = container.Resolve<ICloudDrawer>();
        var text = container.Resolve<IFileLoader>().Load(container.Resolve<ApplicationProperties>().Path).ToLower();
        var words = container.Resolve<IWordsParser>().Parse(text);
        var wordsFrequency = FrequencyDictionary.GetWordsFrequency(words);
        var textBoxes = container.Resolve<TextWrapper>().Wrap(wordsFrequency);
        
        var layouter = container.Resolve<ICloudLayouter>();
        textBoxes = textBoxes.Select(t =>
        {
            t.Location = layouter.PutNextRectangle(t.PreferredSize).Location;
            return t;
        });
        layouter.Clear();
        
        const string path = @"Cloud.png";
        drawer.Draw(textBoxes).Save(path);

        Console.WriteLine($"Tag cloud visualization saved to file {path}");
    }
}