using System.Collections.Generic;
using System.Drawing;

namespace TagCloudContainer.Api
{
    public interface ILayoutProvider
    {
        List<Rectangle> Layout { get; }
    }
}