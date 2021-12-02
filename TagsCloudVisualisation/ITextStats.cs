﻿namespace TagsCloudVisualization.Abstractions;

public interface ITextStats
{
    IReadOnlyDictionary<string, int> Statistics { get; }

    int TotalWordCount { get; }
}