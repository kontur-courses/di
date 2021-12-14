using System;
using System.IO;
using Visualization;
using Visualization.VisualizerProcessorFactory;

namespace CloudTagVisualizer.ConsoleInterface
{
    public class InteractiveVisualizerProcessor
    {
        private TextReader inputStream;
        private TextWriter outputStream;
        private VisualizerFactorySettings settings;
        

        public InteractiveVisualizerProcessor(TextReader inputStream, TextWriter outputStream, VisualizerProcessor processor, VisualizerFactorySettings settings)
        {
            this.inputStream = inputStream;
            this.outputStream = outputStream;
            this.settings = settings;
        }

        public void Execute()
        {
            Console.WriteLine("I am executing");
        }
    }
}