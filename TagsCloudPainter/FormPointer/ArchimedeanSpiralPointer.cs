﻿using System.Drawing;
using TagsCloudPainter.Settings;

namespace TagsCloudPainter.FormPointer;

public class ArchimedeanSpiralPointer : IFormPointer
{
    private readonly CloudSettings cloudSettings;
    private readonly SpiralPointerSettings spiralPointerSettings;
    private double сurrentDifference;

    public ArchimedeanSpiralPointer(CloudSettings cloudSettings, SpiralPointerSettings spiralPointerSettings)
    {
        if (spiralPointerSettings.Step <= 0
            || spiralPointerSettings.RadiusConst <= 0
            || spiralPointerSettings.AngleConst <= 0)
            throw new ArgumentException("either step or radius or angle is not possitive");
        this.cloudSettings = cloudSettings ?? throw new ArgumentNullException(nameof(cloudSettings));
        this.spiralPointerSettings = spiralPointerSettings ?? throw new ArgumentNullException(nameof(spiralPointerSettings));
        сurrentDifference = 0;
    }

    private double Angle => сurrentDifference * spiralPointerSettings.AngleConst;
    private double Radius => сurrentDifference * spiralPointerSettings.RadiusConst;

    public Point GetNextPoint()
    {
        сurrentDifference += spiralPointerSettings.Step;
        var x = cloudSettings.CloudCenter.X + (int)(Radius * Math.Cos(Angle));
        var y = cloudSettings.CloudCenter.Y + (int)(Radius * Math.Sin(Angle));

        return new Point(x, y);
    }

    public void Reset()
    {
        сurrentDifference = 0;
    }
}