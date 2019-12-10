using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.ApplicationRunning.Commands
{
    public static class Check
    {
        public static void Argument<T>(T argument, params bool[] conditions)
        {
            var ok = conditions.All(c => c);
            if(!ok)
                throw new ArgumentException($"Not a valid argument '{argument}'");
        }
        
        public static void ArgumentsCountIs<T>(IEnumerable<T> arguments, int expectedCount)
        {
            var ok = arguments.Count() == expectedCount;
            if(!ok)
                throw new ArgumentException($"Incorrect arguments count! Expected {expectedCount}.");
        }
        
        public static void Argument<T>(T argument, string message, params bool[] conditions)
        {
            var ok = conditions.All(c => c);
            if(!ok)
                throw new ArgumentException(message);
        }
    }
}