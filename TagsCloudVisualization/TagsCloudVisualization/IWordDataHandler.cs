﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public interface IWordDataHandler
    {
        List<CloudWordData> GetDatas(ICloudLayouter cloud);
    }
}
