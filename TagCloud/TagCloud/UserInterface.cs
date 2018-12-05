using System;

namespace TagCloud
{
    abstract class UserInterface
    {
        protected readonly TagCloudCreator Creator;
        protected TagCloudOptions.Factory OptionsFactory;

        protected UserInterface(TagCloudCreator creator, TagCloudOptions.Factory optionsFactory)
        {
            this.Creator = creator;
            this.OptionsFactory = optionsFactory;
        }

        public abstract void Run(string[] startupArgs);
    }
}