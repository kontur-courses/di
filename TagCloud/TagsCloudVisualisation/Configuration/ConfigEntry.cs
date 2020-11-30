namespace TagsCloudVisualisation.Configuration
{
    public class ConstantConfigEntry<T> : IConfigEntry<T>
    {
        private readonly T value;

        public ConstantConfigEntry(T value)
        {
            this.value = value;
        }

        public T GetValue(string _)
        {
            return value;
        }

        public static implicit operator T(ConstantConfigEntry<T> entry)
        {
            return entry.value;
        }

        public static explicit operator ConstantConfigEntry<T>(T value)
        {
            return new ConstantConfigEntry<T>(value);
        }
    }
}