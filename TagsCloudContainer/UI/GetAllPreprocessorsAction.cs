using System;
using System.IO;
using System.Linq;
using TagsCloudContainer.Common;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Preprocessors;

namespace TagsCloudContainer.UI
{
    public class GetAllPreprocessorsAction : ConsoleUiAction
    {
        public override string Category => "Preprocessors";
        public override string Name => "GetAllPreprocessors";
        public override string Description { get; }

        public GetAllPreprocessorsAction(TextReader reader, TextWriter writer)
            : base(reader, writer)
        {
        }

        public override void Perform()
        {
            var preprocessors = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.FullName.Contains("TagsCloudContainer"))
                .GetTypes()
                .Where(t => t.IsInstanceOf<IPreprocessor>())
                .ToArray();
            foreach (var p in preprocessors)
            {
                var prop = p.GetProperty(nameof(State));
                var status = (State) prop.GetValue(null);
                writer.WriteLine($@"{p.Name} is {status}");
            }
        }
    }
}