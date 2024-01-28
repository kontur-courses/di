
namespace TagCloudGenerator
{
    public abstract class TextProcessorWrapper : ITextProcessor
    {
        protected readonly ITextProcessor textProcessor;
        public TextProcessorWrapper(ITextProcessor textProcessor) 
        { 
            this.textProcessor = textProcessor; 
        }

        public virtual IEnumerable<string> ProcessText(IEnumerable<string> file)
        {
            return textProcessor.ProcessText(file);
        }
    }
}