namespace TagsCloudContainer.App.Utils
{
    public static class DirectionUtils
    {
        private static readonly DirectionToMove[] Directions =
        {
            DirectionToMove.Up,
            DirectionToMove.Down,
            DirectionToMove.Left,
            DirectionToMove.Right
        };

        public static DirectionToMove[] GetAllDirections()
        {
            return Directions;
        }
    }

    public enum DirectionToMove
    {
        Up,
        Down,
        Left,
        Right
    }
}