﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public interface IColoringAlgorithm
    {
        public Color GetColor(Rectangle rectangle);
    }
}