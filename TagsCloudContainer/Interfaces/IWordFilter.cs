﻿namespace TagsCloudContainer.Interfaces
{
    public interface IWordsFilter
    {
        public List<string> FilterWords(List<string> taggedWords,
            ICustomOptions options, List<string>? boringWords);
    }
}