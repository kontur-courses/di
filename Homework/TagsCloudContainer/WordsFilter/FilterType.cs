using System;

namespace TagsCloudContainer.WordsFilter
{
    [Flags]
    public enum FilterType
    {
        SpeechPart = 0x0,
        Length = 0x1,
    }
}