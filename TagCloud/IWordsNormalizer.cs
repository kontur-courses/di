﻿namespace TagCloud;

public interface IWordsNormalizer
{
    List<string> NormalizeWords(List<string> words);
}