using System.Collections;

namespace TagsCloudContainer.CloudVisualizers.BitmapMakers
{
    public static class BitmapMakers
    {
        public static IBitmapMaker TryGetBitmapMaker(string name)
        {
            var lower = name.ToLower();
            switch (lower)
            {
                default:
                    return null;
                case "default":
                case "standard":
                case "def":
                {
                    return new DefaultBitmapMaker();
                }
            }
        }
    }
}