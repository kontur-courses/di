using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Models;

namespace TagCloud.Utility.Data
{
    public interface ILogger
    {
        void Log(object obj);

        CloudItem[] Items { get; }
        Bitmap Picture { get; }
        Options LastRunningOptions { get; }
        List<Exception> Exceptions { get; }
    }
}