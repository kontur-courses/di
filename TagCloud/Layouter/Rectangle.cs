namespace TagCloud.Layouter
{
    public class Rectangle
    {
        public Rectangle(Point center, Size size)
        {
            Center = center;
            Size = size;
        }

        public Point Center { get; set; }
        public Size Size { get; }

        public double Left => Center.X - Size.Width / 2;
        public double Right => Center.X + Size.Width / 2;
        public double Top => Center.Y + Size.Height / 2;
        public double Bottom => Center.Y - Size.Height / 2;

        public bool Intersects(Rectangle otherRectangle)
        {
            return !(Left > otherRectangle.Right
                     || Right < otherRectangle.Left
                     || Top < otherRectangle.Bottom
                     || Bottom > otherRectangle.Top);
        }

        public override string ToString()
        {
            return $"Rectangle (X: {Center.X}; Y: {Center.Y})";
        }
    }
}