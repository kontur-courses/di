using Autofac;
using TagsCloudContainer.Clients;
using TagsCloudContainer.ProjectSettings;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer
{
    class Program
    {
        
        private static void Main(string[] args)
        {
            var builder = ProjectSettingsGetter.GetSettings();
            var client = builder.Resolve<IClient>();
            client.Execute(args);
        }
    }
}
