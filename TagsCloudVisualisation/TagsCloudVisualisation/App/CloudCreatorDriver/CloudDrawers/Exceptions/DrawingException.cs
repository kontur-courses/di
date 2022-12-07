using System;

namespace TagsCloudVisualisation.App.CloudDrawers.Exceptions
{
    public class DrawingException : Exception
    {
        public DrawingException(string message)
            : base(message){}
        
        public  DrawingException (string message, Exception e)
            : base(message, e){}
    }
}