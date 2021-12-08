namespace TagsCloudContainer.Abstractions;

public interface IStyler
{
    IStyledTag Style(ITag source);
}