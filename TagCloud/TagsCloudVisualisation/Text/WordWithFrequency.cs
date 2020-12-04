﻿namespace TagsCloudVisualisation.Text
{
    public readonly struct WordWithFrequency
    {
        public WordWithFrequency(string word, int frequency)
        {
            Word = word;
            Frequency = frequency;
        }

        public string Word { get; }
        public int Frequency { get; }
    }
}