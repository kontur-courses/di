﻿namespace TagCloudContainer.Filters
{
    public interface IFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> textWords);
    }
}
