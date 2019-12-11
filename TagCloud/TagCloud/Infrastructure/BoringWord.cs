namespace TagCloud
{
    public class BoringWord : ICheckable
    {
        public bool IsChecked { get; set; }

        public string Name { get; private set; }

        public BoringWord(string name)
        {
            IsChecked = true;
            Name = name;
        }
    }
}
