﻿
namespace TagCloudGenerator
{
    public abstract class TextProcessorWrapper : ITextProcessor
    {
        protected readonly ITextProcessor textProcessor;
        public TextProcessorWrapper(ITextProcessor textProcessor) 
        { 
            this.textProcessor = textProcessor; 
        }

        public IEnumerable<string> ProcessText(IEnumerable<string> file)
        {
            throw new NotImplementedException();
        }

        //public virtual string[] ProcessText(string[] file)
        //{
        //   return textProcessor.ProcessText(file);
        //}  
    }
}