using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TagsCloudContainer.Common;
using TagsCloudContainer.Preprocessors;

namespace TagsCloudContainer.UI
{
    public class GetAllPreprocessorsAction : IUiAction
    {
        private readonly TextWriter writer;
        public string Category => "Preprocessors";
        public string Name => "GetAllPreprocessors";
        public string Description { get; }

        public GetAllPreprocessorsAction(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Perform()
        {
            var preprocessor = typeof(IPreprocessor);
            var preprocessors = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.FullName.Contains("TagsCloudContainer"))
                .GetTypes()
                .Where(t => preprocessor.IsAssignableFrom(t))
                .ToArray();
            foreach (var p in preprocessors)
            {
                var status = p.GetCustomAttribute<StateAttribute>();
                writer.WriteLine($@"{p.Name} is {status.State}");
            }
        }
    }
}