using System;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.WordsColoringAlgorithms
{
    public class WordsColoringFactory
    {
            private readonly Func<IUi> settingsProvider;
            public WordsColoringFactory(Func<IUi> settingsProvider)
            {
                this.settingsProvider = settingsProvider;
            }
            public IWordsPainter Create()
            {
                var actualSettings = settingsProvider.Invoke();
                return actualSettings.WordsColoringAlgorithm switch
                {
                    "d" => new DefaultWordsPainter(),
                    "gd" => new GradientDependsOnSizePainter(),
                    "g" => new GradientWordsPainter(),
                    _ => throw new ArgumentException("Wrong coloring algorithm")
                };
            
            }
    }
}