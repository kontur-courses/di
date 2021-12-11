namespace TagsCloudVisualization
{
    public class Movement
    {
        public Movement(int distance, MovementDirection movementDirection)
        {
            Distance = distance;
            MovementDirection = movementDirection;
        }

        public int Distance { get; }
        public MovementDirection MovementDirection { get; }
    }
}