namespace TagsCloudVisualization.Abstractions;

public interface IStyler
{
    IStyledTag Style(ITag source);
}