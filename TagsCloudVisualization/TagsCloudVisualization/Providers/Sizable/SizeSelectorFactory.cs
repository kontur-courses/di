using System;
using TagsCloudVisualization.Providers.Sizable.Interfaces;

namespace TagsCloudVisualization.Providers.Sizable
{
    internal class SizeSelectorFactory
    {
        private readonly ISizableSelector logSelector;
        private readonly ISizableSelector sqrtSelector;
        private readonly ISizableSelector sqrtLengthSelector;

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
                case SizeSelectorType.SqrtL:
                    return sqrtLengthSelector;
                default: throw new ArgumentException("Cant resolve type");
            }
        }
    }
}