﻿using TagCloud.Infrastructure.Common;

namespace TagCloud.App.UI;

public interface IUserInterface
{
    public void Run(IAppSettings settings);
}