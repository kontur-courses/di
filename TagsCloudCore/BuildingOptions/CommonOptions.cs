using TagsCloudCore.Common.Enums;
using WordProviderInfo = TagsCloudCore.WordProcessing.WordInput.WordProviderInfo;

namespace TagsCloudCore.BuildingOptions;

public class CommonOptions
{
    public CommonOptions(WordProviderInfo wordProviderInfo, WordColorerAlgorithm wordColorer,
        CloudBuildingAlgorithm cloudBuildingAlgorithm)
    {
        WordProviderInfo = wordProviderInfo;
        WordColorer = wordColorer;
        CloudBuildingAlgorithm = cloudBuildingAlgorithm;
    }

    public WordProviderInfo WordProviderInfo { get; }

    public WordColorerAlgorithm WordColorer { get; }

    public CloudBuildingAlgorithm CloudBuildingAlgorithm { get; }
}