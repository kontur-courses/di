namespace TagCloud.selectors
{
    public class BoringChecker : IChecker<string>
    {
        public bool IsValid(string source) => source.Length > 3;
    }
}