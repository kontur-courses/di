using System;

namespace TagsCloudVisualization.Exceptions
{
    public class OutOfPermissibleRangeException : Exception
    {
        public OutOfPermissibleRangeException(string massage) : base(massage)
        {

        }
    }
}