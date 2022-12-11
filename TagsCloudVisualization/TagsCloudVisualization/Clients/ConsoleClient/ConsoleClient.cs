using Autofac;
using CommandLine;
using TagsCloudVisualization.CloudDrawer;
using TagsCloudVisualization.TextInput;

namespace TagsCloudVisualization.Clients;

public class ConsoleClient
{
    private Options options;

    public ConsoleClient(params string[] args)
    {
        options = Parser.Default.ParseArguments<Options>(args).Value;
    }

    public void Run()
    {
        AppContainer.Configure(options);

        using (var scope = AppContainer.GetScope())
        {
            var textInput = scope.Resolve<ITextInput>();
            var text = textInput.GetInputString();

            var generator = scope.Resolve<ICloudGenerator>();
            var cloud = generator.GenerateCloud(text);

            var drawer = scope.Resolve<ICloudDrawer>();
            drawer.Draw(cloud);
        }
    }
}