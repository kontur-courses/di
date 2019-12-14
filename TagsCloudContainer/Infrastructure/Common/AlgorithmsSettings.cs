namespace TagsCloudContainer.Infrastructure.Common
{
    public class AlgorithmsSettings
    {
        public bool Centering { get; private set; }

        public AlgorithmsSettings(bool centering)
        {
            Centering = centering;
        }
    }
}