using System.Drawing;
using TagsCloudVisualization;
using Autofac;
using TagsCloudVisualization.CloudDrawer;

var text = "test test";

AppContainer.Configure();

using (var scope = AppContainer.GetScope())
{
    var generator = scope.Resolve<ICloudGenerator>();
    var cloud = generator.GenerateCloud(text);
    var drawer = scope.Resolve<ICloudDrawer>();
    drawer.Draw(cloud);
}