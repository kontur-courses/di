using System.Drawing;
using System.Xml;

namespace TagsCloudContainer.CloudLayouters
{
    public class SquareSpiral : ISpiral
    {
        private Point RightShift; 
        private Point LeftShift; 
        private Point DownShift; 
        private Point UpShift;
        private Point spiralCenter;
        private Point currentShift;
        private Point CurrentLocation;
        private const int delta = 10;
        private int needToGo;
        private int currentDistance;
        private int stepCounter;

        public SquareSpiral(Point spiralCenter)
        {
            this.spiralCenter = spiralCenter;
            RightShift = new Point(1, 0);
            LeftShift = new Point(-1, 0);
            DownShift = new Point(0, 1);
            UpShift = new Point(0, -1);
            CurrentLocation = spiralCenter;
            currentShift = RightShift;
            needToGo += 10;
            currentDistance = 0;
        }

        public Point GetNextCurvePoint()
        {
            if (currentDistance < needToGo)
            {
                CurrentLocation = CurrentLocation.AddShift(currentShift);
                currentDistance++;
            }
            else
            {
                needToGo += delta;
                currentShift = GetNextShift();
                CurrentLocation = CurrentLocation.AddShift(currentShift);
                currentDistance = 1;
            }

            return CurrentLocation;
        }

        private Point GetNextShift()
        {
            if (currentShift == RightShift) return UpShift;
            if (currentShift == UpShift) return LeftShift;
            if (currentShift == LeftShift) return DownShift;
            return RightShift;
        }
    }
}
