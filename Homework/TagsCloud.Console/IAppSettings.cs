using System.Collections.Generic;
using TagsCloudContainer.Layouter.PointsProviders;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsConverters;

namespace TagsCloud.Console
{
    public interface IAppSettings
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        
    }
}