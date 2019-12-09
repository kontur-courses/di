using TagsCloudGenerator.Bases;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Factories
{
    public class PointsSearchersFactory : FactoryBase<IPointsSearcher>
    {
        public PointsSearchersFactory(IPointsSearcher[] searchers, IFactorySettings factorySettings) : 
            base(searchers, factorySettings, s => s.PointsSearcherId, s => null)
        {}
    }
}