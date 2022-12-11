using System.Collections.Generic;
using TagCloud.ImageProcessing;

namespace TagCloud.AppConfig
{
    public class AppConfigProvider : IAppConfigProvider
    {
        private readonly IEnumerable<string> args;

        public AppConfigProvider(IEnumerable<string> args)
        {
            this.args = args;
        }

        public IAppConfig GetAppConfig()
        {
            return new AppConfig(@"C:\VIKTOR\Kontur\8_Dependency_Injection\TestText.txt", 
                                 @"C:\VIKTOR\Kontur\8_Dependency_Injection\TagCloudImages\WordCloud.png", 
                                 new ImageSettings()); //предусмотреть передачу GradientColoring

        }
    }
}
