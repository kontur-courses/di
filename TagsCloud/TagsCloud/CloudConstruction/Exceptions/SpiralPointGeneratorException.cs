using System;

namespace TagsCloud.CloudConstruction.Exceptions
{
    public class SpiralPointGeneratorException : Exception
    {
        public SpiralPointGeneratorException() : base("Failed searching suitable point")
        {
        }
    }
}