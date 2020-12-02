using System.Collections.Generic;
using System.IO;
using System.Linq;
using CloudContainer.ConfigCreators;
using TagsCloudVisualization;

namespace CloudContainer
{
    public class Process
    {
        private readonly IWordProvider provider;
        private readonly IWordsCleaner cleaner;
        private readonly IConfigCreator configCreator;
        private readonly string[] args;
        private readonly IWordConverter converter;
        private readonly IConfig config;
        
        public Process(IWordProvider provider, IWordsCleaner cleaner, IConfigCreator configCreator, 
            string[] args, IWordConverter converter, IConfig config)
        {
            this.provider = provider;
            this.cleaner = cleaner;
            this.configCreator = configCreator;

            this.args = args;
            this.converter = converter;
            this.config = config;
        }
        
        public void Run()
        {
            var directory = Directory.GetCurrentDirectory();
            var path = directory + "\\text.txt"; //TODO

            var words = provider.GetWords(path);
            var cleanedWords = cleaner.CleanWords(words);
            
            configCreator.CreateConfig(args, config);

            var cloudTags = converter.ConvertWords(cleanedWords);
            
            var image = Drawer.DrawImage(cloudTags,config);
            var imageSaver = new PngSaver();
            imageSaver.SaveImage(image, "newfile");
        }
    }
}