namespace TagCloud.Infrastructure.Settings
{
    public interface IWordCountThresholdSettingProvider
    {
        public int WordCountThreshold { get; }
    }
}