using TagsCloudVisualization.Providers.Sizable.Interfaces;

namespace TagsCloudVisualization.Providers.Sizable
{
    internal class SizeSelectorFactory
    {
        private readonly ISizableSelector logSelector;
        private readonly ISizableSelector sqrtLengthSelector;
        private readonly ISizableSelector sqrtSelector;

        public SizeSelectorFactory()
        {
            logSelector = new LogSizeSelector();
            sqrtSelector = new SqrtSizeSelector();
            sqrtLengthSelector = new SqrtLengthSizeSelector();
        }

        public ISizableSelector GeSelector(SizeSelectorType type)
        {
            switch (type)
            {
                case SizeSelectorType.Sqrt:
                    return sqrtSelector;
                case SizeSelectorType.Log:
                    return logSelector;
                default:
                    return sqrtLengthSelector;
            }
        }
    }
}