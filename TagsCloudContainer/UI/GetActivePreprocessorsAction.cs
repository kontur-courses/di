using System.IO;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.UI
{
    public class GetActivePreprocessorsAction : ConsoleUiAction
    {
        public override string Category => "Preprocessors";
        public override string Name => "GetActivePreprocessors";
        public override string Description { get; }

        public GetActivePreprocessorsAction(TextReader reader, TextWriter writer)
            : base(reader, writer)
        {
        }

        public override void Perform()
        {
            var preprocessors = PreprocessorsRegistrator.GetActivePreprocessors();
            foreach (var preprocessor in preprocessors) 
                writer.WriteLine(preprocessor.Name);
        }
    }
}