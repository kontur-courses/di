namespace TagCloudGUIClient
{
    public class MenuItem<T>
    {
        public readonly string Name;
        public readonly T Value;

        public MenuItem(T value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}