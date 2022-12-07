using System;

namespace TagsCloudVisualisation.App.WordsPreprocessor.Word
{
    public interface IWord : IEquatable<IWord>
    {
        public string Value { get; }
        public int Count { get; set; }
        public double Tf { get; set; }
    }
}