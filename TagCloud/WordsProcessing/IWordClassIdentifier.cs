﻿namespace TagCloud.WordsProcessing
{
    public interface IWordClassIdentifier
    {
        WordClass GetWordClass(string word);
    }
}
