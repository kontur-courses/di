using TagsCloudGenerator.Bases;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Factories
{
    public class SaversFactory : FactoryBase<ISaver>
    {
        public SaversFactory(ISaver[] savers, IFactorySettings factorySettings) : 
            base(savers, factorySettings, s => s.SaverId, s => null)
        {}
    }
}