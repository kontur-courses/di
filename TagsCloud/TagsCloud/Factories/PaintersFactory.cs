using TagsCloudGenerator.Bases;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Factories
{
    public class PaintersFactory : FactoryBase<IPainter>
    {
        public PaintersFactory(IPainter[] painters, IFactorySettings factorySettings) : 
            base(painters, factorySettings, s => s.PainterId, s => null)
        {}
    }
}