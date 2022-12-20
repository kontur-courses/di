using System;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.LayouterAlgorithms
{
    public class LayouterFactory
    {
        private readonly Func<IUi> settingsProvider;

        public LayouterFactory(Func<IUi> settingsProvider)
        {
            this.settingsProvider = settingsProvider;
        }

        public ICloudLayouterAlgorithm Create(Spiral spiral)
        {
            var actualSettings = settingsProvider.Invoke();
            return actualSettings.Layouter switch
            {
                "d" => new CircularCloudLayouter(spiral),
                _ => throw new ArgumentException("Wrong algorithm name")
            };
        }
    }
}