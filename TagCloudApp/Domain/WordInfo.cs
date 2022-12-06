namespace TagCloudApp.Domain;

public readonly record struct WordInfo(string Word, int Count)
{
    public bool Equals(WordInfo other) =>
        Word == other.Word && Count == other.Count;

    public override int GetHashCode() =>
        HashCode.Combine(Word, Count);
}