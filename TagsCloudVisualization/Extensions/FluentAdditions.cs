using System;

namespace TagsCloudVisualization
{
    public static class Calling
    {
        public static Action ThisLambda(this Action act) => act;
    }
}