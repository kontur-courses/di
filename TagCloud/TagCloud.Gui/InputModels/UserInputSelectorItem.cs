namespace TagCloud.Gui.InputModels
{
    public class UserInputSelectorItem<T>
    {
        public UserInputSelectorItem(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public T Value { get; }
    }
}