using System.Collections.Generic;
using TagCloud.PointGetters;

namespace TagCloud.CloudLayoters
{
    public static class CloudLayoterAssosiation<T> where T: IPointGetter, new()
    {
        //private static readonly Dictionary<string, ICloudLayoter> cloudLayoters =
        //    new Dictionary<string, ICloudLayoter>
        //    {
        //        ["density"] = new DensityCloudLayouter(),
        //        ["identity"] = new IdentityCloudLayoter()
        //    };

        public static ICloudLayoter GetCloudLayoter(string nameLayoter, string namePointGetter)
        {
            var pointGetter = PointGetterAssosiation.GetPointGetter(namePointGetter);
            if (pointGetter == null)
                return null;
            if (nameLayoter == "density")
                return new DensityCloudLayouter(pointGetter);
            if (nameLayoter == "identity")
                return new DensityCloudLayouter(pointGetter);
            return null;
        } 
    }
}
