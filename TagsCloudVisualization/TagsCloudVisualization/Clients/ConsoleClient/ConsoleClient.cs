using Autofac;
using CommandLine;
using TagsCloudVisualization.CloudDrawer;
using TagsCloudVisualization.TextInput;

namespace TagsCloudVisualization.Clients;

public class ConsoleClient
{
    private string inputPath;

    public ConsoleClient(params string[] args)
    {
        var options = Parser.Default.ParseArguments<Options>(args);
        inputPath = options.Value.Path;
    }

    public void Run()
    {
        AppContainer.Configure();

        using (var scope = AppContainer.GetScope())
        {
            var textInput = scope.Resolve<ITextInput>();
            var text = textInput.GetInputString(inputPath);

            var generator = scope.Resolve<ICloudGenerator>();
            var cloud = generator.GenerateCloud(text);

            var drawer = scope.Resolve<ICloudDrawer>();
            drawer.Draw(cloud);
        }
    }
}