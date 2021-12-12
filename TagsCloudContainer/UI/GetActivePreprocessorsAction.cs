using System.IO;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.UI
{
    public class GetActivePreprocessorsAction : IUiAction
    {
        private readonly TextWriter writer;
        public string Category => "Preprocessors";
        public string Name => "GetActivePreprocessors";
        public string Description { get; }

        public GetActivePreprocessorsAction(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Perform()
        {
            var preprocessors = PreprocessorsRegistrator.GetActivePreprocessors();
            foreach (var preprocessor in preprocessors) 
                writer.WriteLine(preprocessor.Name);
        }
    }
}