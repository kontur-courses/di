public static class TagCloudHelpers
{
    public static double GetMultiplier(int value, int min, int max)
    {
        return ((double)value - min) / (max - min);
    }
}

