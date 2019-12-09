using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Extensions;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Bases
{
    public abstract class FactoryBase<TFactorial> : IFactory<TFactorial>
        where TFactorial : IFactorial
    {
        protected readonly Dictionary<string, TFactorial> factorials;
        protected readonly IFactorySettings factorySettings;
        protected readonly Func<IFactorySettings, string> getSingleFactorialId;
        protected readonly Func<IFactorySettings, string[]> getFactorialsIdsArray;

        public FactoryBase(
            TFactorial[] factorials,
            IFactorySettings factorySettings,
            Func<IFactorySettings, string> getSingleFactorialId, 
            Func<IFactorySettings, string[]> getFactorialsIdsArray)
        {
            this.factorials = factorials.ToDictionary(f =>
                f
                .GetType()
                .GetFirstAttributeObj<FactorialAttribute>()
                .FactorialId);
            this.factorySettings = factorySettings;
            this.getSingleFactorialId = getSingleFactorialId;
            this.getFactorialsIdsArray = getFactorialsIdsArray;
        }

        public virtual TFactorial[] CreateArray() => 
            getFactorialsIdsArray(factorySettings)
            .Select(filterName => factorials[filterName])
            .ToArray();

        public virtual TFactorial CreateSingle() => factorials[getSingleFactorialId(factorySettings)];
    }
}