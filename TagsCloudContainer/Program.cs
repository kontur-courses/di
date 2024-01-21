using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.TagsCloud;

namespace TagsCloudContainer
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var serviceProvider = Startup.ConfigureServices())
            {
                var tagCloudApp = serviceProvider.GetRequiredService<TagCloudApp>();
                tagCloudApp.Run("src/text.txt");
            }
        }
    }
}
