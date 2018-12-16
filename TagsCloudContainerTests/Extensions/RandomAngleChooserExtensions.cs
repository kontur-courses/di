using System.Collections.Generic;
using TagsCloudContainer.CircularCloudLayouters;

namespace TagsCloudContainerTests.Extensions
{
    public static class IAngleChooserExtensions
    {
        public static List<double> GetAngles(this IAngleChooser randomAngleChooser, int count)
        {
            var angles = new List<double>();
            for (int i = 0; i < count; i++)
            {
                randomAngleChooser.MoveNext();
                angles.Add(randomAngleChooser.Current);
            }

            return angles;
        }
    }
}