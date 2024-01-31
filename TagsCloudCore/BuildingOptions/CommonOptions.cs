using TagsCloudCore.Common.Enums;
using WordProviderInfo = TagsCloudCore.WordProcessing.WordInput.WordProviderInfo;

namespace TagsCloudCore.BuildingOptions;

public record CommonOptions(
    WordProviderInfo WordProviderInfo,
    WordColorerAlgorithm WordColorer,
    CloudBuildingAlgorithm CloudBuildingAlgorithm);