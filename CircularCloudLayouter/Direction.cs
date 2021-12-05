namespace CloudLayouter;

[Flags]
internal enum Direction
{
    None = 0b0000,
    Left = 0b0001,
    Top = 0b0010,
    Right = 0b0100,
    Bottom = 0b1000,
    All = Left | Top | Right | Bottom,
}
