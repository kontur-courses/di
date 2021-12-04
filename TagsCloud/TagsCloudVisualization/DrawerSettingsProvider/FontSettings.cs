using System;

namespace TagsCloudVisualization.DrawerSettingsProvider
{
    public class FontSettings
    {
        private readonly int _maxSize = 20;
        public string Family { get; init; } = "Arial";

        public int MaxSize
        {
            get => _maxSize;
            init
            {
                if (value <= 0) throw new ArgumentException($"Expected positive value, but was {value}");
                _maxSize = value;
            }
        }
    }
}