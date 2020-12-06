namespace MyStem.Wrapper.Impl
{
    public interface IMyStemBuilder
    {
        IMyStem Create(params MyStemOptions[] args);
    }
}