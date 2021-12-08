using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Abstractions;

public interface IRequiredSettingsProvider : ICliSettingsProvider
{
    bool IsSet { get; }
}
