namespace TagsCloudContainer.CircularCloudLayouter
{
    public class Direction : IDirection<double>
    {
        private readonly double _angleShift;
        private double _currentAlpha;

        public Direction(double angleShift = 0.01)
        {
            _angleShift = angleShift;
        }

        public double GetNextDirection()
        {
            var oldAlpha = _currentAlpha;
            _currentAlpha = (_currentAlpha + _angleShift).AngleToStandardValue();

            return oldAlpha;
        }
    }
}