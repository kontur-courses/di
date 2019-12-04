namespace TagsCloudContainer.Data
{
    public class Word
    {
        internal readonly string Value;
        internal readonly int Occurrences;
        internal readonly double Probability;

        internal Word(string value, int occurrences, double probability)
        {
            Value = value;
            Occurrences = occurrences;
            Probability = probability;
        }
    }
}