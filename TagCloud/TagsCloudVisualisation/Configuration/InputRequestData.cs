using System;

namespace TagsCloudVisualisation.Configuration
{
    public readonly struct InputRequestData
    {
        public readonly string Description;
        public readonly Type RequiredType;

        public InputRequestData(string description, Type requriredType)
        {
            Description = description;
            RequiredType = requriredType;
        }
    }
}