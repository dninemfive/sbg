namespace bgp;
public enum SpaceState { Empty, Sheep, Wolf }
public readonly struct BaghChalBoard
{
    public readonly SpaceState[] Spaces;
    public BaghChalBoard(SpaceState[] spaces)
    {
        if (spaces.GetLength(0) != 5 || spaces.GetLength(1) != 5)
            throw new ArgumentException("A bagh chal board must be 5x5!", nameof(spaces));
        Spaces = spaces;
    }
    public static implicit operator BaghChalBoard(SpaceState[] spaces)
        => new(spaces);
}