namespace TagCloud.Infrastructure.Settings.UISettingsManagers
{
    public interface ISettingsManager
    {
        public string Title { get; }
        public string Help { get; }
        public bool TrySet(string value);
        public string Get();
    }
}