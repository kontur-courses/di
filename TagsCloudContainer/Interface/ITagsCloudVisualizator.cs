﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public interface ITagsCloudVisualizator
    {
        Bitmap GetTagsCloudImage();
    }
}
