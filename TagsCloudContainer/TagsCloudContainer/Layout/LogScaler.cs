using System;

namespace TagsCloudContainer.Layout
{
    public class LogScaler : IScaler
    {
        private readonly IScaler innerScaler;

        public LogScaler(IScaler innerScaler)
        {
            this.innerScaler = innerScaler;
        }

        public float GetValue(int x) => (float)Math.Log(innerScaler.GetValue(x));
    }
}