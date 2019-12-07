using System;

namespace TagsCloudGenerator
{
    public static class Calling
    {
        public static Action ThisLambda(this Action act) => act;
    }
}