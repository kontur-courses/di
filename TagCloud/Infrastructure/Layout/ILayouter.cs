namespace TagCloud.Infrastructure.Layout
{
    public interface ILayouter<in TIn, out TOut>
    {
        public TOut GetPlace(TIn item);
    }
}