namespace MyStem.Wrapper.Impl
{
    public interface IMyStemBuilder
    {
        IMyStem Create(MyStemOutputFormat outputFormat, params MyStemOptions[] args);
    }
}