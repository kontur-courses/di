using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Factories;

namespace TagsCloudVisualization.Visualization.Configurator
{
    public class WordsVisualizingConfigurator : IVisualizingConfigurator
    {
        private readonly IVisualizingTokenFactory tokenFactory;
        private readonly int fontSize;
        private readonly Func<string,Color> colorizer;

        public WordsVisualizingConfigurator(IVisualizingTokenFactory tokenFactory,
            Func<string, Color> colorizer, int fontSize)
        {
            this.tokenFactory = tokenFactory;
            this.colorizer = colorizer;
            this.fontSize = fontSize;
        }   
        
        public IEnumerable<IVisualizingToken> Configure(IEnumerable<string> visualizingValues)
        {
            var values = visualizingValues as string[] ?? visualizingValues.ToArray();
            
            var frequencyDict = GetFrequencyDict(values);
            
            return values.Select(
                    value => tokenFactory.NewToken(value,
                        fontSize + frequencyDict[value] - 1,
                        colorizer(value)))
                .ToArray();
        }

        private Dictionary<string, int> GetFrequencyDict(IEnumerable<string> visualizingValues)
        {
            var frequencyDict = new Dictionary<string, int>();
            foreach (var value in visualizingValues)
            {
                if (!frequencyDict.TryGetValue(value, out _))
                {
                    frequencyDict[value] = 0;
                }
                
                frequencyDict[value]++;
            }

            return frequencyDict;
        }
    }
}