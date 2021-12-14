using System.Drawing;

namespace Visualization.VisualizerProcessorFactory
{
    public class VisualizerFactorySettings
    {
        public SavingFormat SavingFormat { get; set; }
        public InputFileFormat InputFileFormat { get; set; }
        
        public Size ImageSize { get; set; }
        
        public Font TextFont { get; set; }
        
        public Color TextColor { get; set; }
        
        public Color BackgroundColor { get; set; }
        
        public Color StrokeColor { get; set; }
    }
}