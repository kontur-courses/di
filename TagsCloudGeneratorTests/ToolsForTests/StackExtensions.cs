using System.Collections.Generic;

namespace TagsCloudGeneratorTests.ToolsForTests
{
    internal static class StackExtensions
    {
        public static T NextToTop<T>(this Stack<T> stack)
        {
            var top = stack.Pop();
            var nextToTop = stack.Peek();
            stack.Push(top);

            return nextToTop;
        }
    }
}