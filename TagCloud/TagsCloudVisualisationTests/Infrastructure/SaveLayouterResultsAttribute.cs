using System;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualisationTests.Infrastructure
{
    public class SaveLayouterResultsAttribute : Attribute
    {
        public readonly TestStatus[] ValidStatuses;

        public SaveLayouterResultsAttribute(params TestStatus[] validStatuses)
        {
            ValidStatuses = validStatuses;
        }
    }
}