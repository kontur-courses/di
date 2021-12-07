using System;

namespace TagsCloudContainer
{
    public interface ITagsCloudDirector : IDisposable
    {
        void Render();
    }
}