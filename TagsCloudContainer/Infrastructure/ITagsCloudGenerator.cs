using FluentResults;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.Infrastructure
{
    public interface ITagsCloudGenerator
    {
        public Result<WordPlate[]> GeneratePlates(IEnumerable<string> words, PointF center, WordFontSettings fontSettings);
    }
}