using TagsCloudVisualisation.Output;

namespace TagsCloudVisualisation.Configuration
{
    public class ConfigEntry<T> : IConfigEntry<T>
    {
        private readonly T value;

        public ConfigEntry(T value)
        {
            this.value = value;
        }

        public T GetValue(string _)
        {
            return value;
        }
    }
}