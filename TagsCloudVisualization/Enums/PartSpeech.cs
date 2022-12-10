using System;

namespace TagsCloudVisualization.Enums
{
    [Flags]
    public enum PartSpeech
    {
        None = 2047,
        Noun = 1,
        Verb = 2,
        Pronoun = 4,
        Adjective = 8,
        ShortAdjective = 16,
        InfinitiveVerb = 32,
        Participle = 64,
        AdverbialParticiple = 128,
        ShortParticiple = 256,
        Adverb = 512,
        Unknown = 1024
    }
}