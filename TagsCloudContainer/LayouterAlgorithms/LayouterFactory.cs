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

        public ICloudLayouterAlgorithm Create()
        {
            var actualSettings = settingsProvider.Invoke();
            return actualSettings.Layouter switch
            {
                "d" => new CircularCloudLayouter(new Spiral(actualSettings)),
                _ => throw new ArgumentException("Wrong algorithm name")
            };
        }
    }
}