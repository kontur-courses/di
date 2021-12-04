using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudApp
{
    public class RenderCommand
    {
        private readonly RenderOptions renderOptions;

        public RenderCommand(RenderOptions renderOptions)
        {
            this.renderOptions = renderOptions;
        }

        public void Render()
        {
            var tagsCloud = new TagsCloud();
            SetSettings(tagsCloud);
            tagsCloud.Render();
        }

        private void SetSettings(TagsCloud tagsCloud)
        {
            tagsCloud.Settings.FileLoading.FileName = renderOptions.InputPath;
            tagsCloud.Settings.Font.FontFamily = renderOptions.FontFamily;
            tagsCloud.Settings.Font.MaxFontSize = renderOptions.MaxFontSize;
            tagsCloud.Settings.Font.MinFontSize = renderOptions.MinFontSize;
            tagsCloud.Settings.Rendering.Background = new SolidBrush(renderOptions.BackgroundColor);
            tagsCloud.Settings.Rendering.Scale = renderOptions.ImageScale;
            tagsCloud.Settings.Rendering.DesiredImageSize = renderOptions.ImageSize;
            tagsCloud.Settings.Saving.OutputFile = renderOptions.OutputPath;
        }
    }
}