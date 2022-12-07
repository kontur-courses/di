using System;

namespace TagsCloudVisualisation.App.RectanglesLayouters.Exceptions
{
    public class CloudSettingsException : Exception
    {
        public CloudSettingsException(string message)
            : base(message) { }

        public CloudSettingsException(string message, Exception e)
            : base(message, e) {}
    }
}