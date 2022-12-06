using System.Drawing;
using TagsCloudVisualization;
using Autofac;
using TagsCloudVisualization.CloudDrawer;
using TagsCloudVisualization.TextInput;

AppContainer.Configure();

using (var scope = AppContainer.GetScope())
{
    var textInput = scope.Resolve<ITextInput>();
    var text = textInput.GetString();

    var generator = scope.Resolve<ICloudGenerator>();
    var cloud = generator.GenerateCloud(text);

    var drawer = scope.Resolve<ICloudDrawer>();
    drawer.Draw(cloud);
}