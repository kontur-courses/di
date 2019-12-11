using System;

namespace TagCloudContainer
{
    public class CliElementAttribute : Attribute
    {
        public string CliName;
        public Type TargetType;

        public CliElementAttribute(string cliName, Type type)
        {
            CliName = cliName;
            TargetType = type;
        }
    }
}