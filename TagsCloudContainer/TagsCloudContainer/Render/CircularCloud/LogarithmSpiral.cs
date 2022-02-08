using SixLabors.ImageSharp;

namespace TagsCloudContainer.Render.CircularCloud;

public class LogarithmSpiral : ISpiral
{
    private readonly double _curveStartingRadius;
    private readonly double _minimumCurveAngleStep;
    private readonly double _curveAngleStepSlowDown;
    private readonly double _directionBetweenRoundsCoeff;
    private double _curveAngleStep;
    private double _currentCurveAngle;
    private readonly Point _spiralCenter;

    public LogarithmSpiral(CircularCloudRenderOptions options)
    {
        _curveStartingRadius = options.CurveStartingRadius;
        _minimumCurveAngleStep = options.MinimumCurveAngleStep;
        _curveAngleStepSlowDown = options.CurveAngleStepSlowDown;
        _directionBetweenRoundsCoeff = options.DirectionBetweenRoundsCoefficient;
        _curveAngleStep = options.CurveAngleStep;
        _spiralCenter = options.ImageCenter;
    }

    public Point GetNextCurvePoint()
    {
        _currentCurveAngle += _curveAngleStep;
        if (_curveAngleStep > _minimumCurveAngleStep)
            _curveAngleStep -= _curveAngleStepSlowDown;
        return new Point(
            Convert.ToInt32((_curveStartingRadius + _directionBetweenRoundsCoeff * _currentCurveAngle)
                            * Math.Cos(_currentCurveAngle)) + _spiralCenter.X,
            Convert.ToInt32((_curveStartingRadius + _directionBetweenRoundsCoeff * _currentCurveAngle)
                            * Math.Sin(_currentCurveAngle)) + _spiralCenter.Y);
    }
}