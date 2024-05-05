namespace d9.sbg.examples.baghchal;
public static class BaghChalActions
{
    public static BaghChalAction PlaceSheepAt(Point<int> p)
        => new($"Place sheep at {p}",
               (state) => state.With(sheepPlaced: 1, differences: [(p, BaghChalPlayer.Sheep)]),
               [BaghChalActionValidators.MustHaveUnplacedSheep,
                BaghChalActionValidators.MustBeEmpty(p)]);
    public static BaghChalAction Move(BaghChalPlayer player, Point<int> source, Point<int> destination)
        => new($"Move {player} from {source} to {destination}",
               (state) => state.With(differences: [(source, null), (destination, player)]),
               [BaghChalActionValidators.PlayerMustBe(player, BaghChalPlayer.Sheep, BaghChalPlayer.Wolf),
                BaghChalActionValidators.MustContainPlayer(source, player),
                BaghChalActionValidators.MustBeEmpty(destination),
                BaghChalActionValidators.MustBeAdjacent(source, destination)]);
    public static BaghChalAction Capture(Point<int> source, Point<int> target, Point<int> destination)
        => new($"Capture {target} with {source} -> {destination}",
               (state) => state.With(sheepCaptured: 1, differences: [(source, null), (target, null), (destination, BaghChalPlayer.Wolf)]),
               [BaghChalActionValidators.MustContainPlayer(source, BaghChalPlayer.Wolf),
                BaghChalActionValidators.MustContainPlayer(target, BaghChalPlayer.Sheep),
                BaghChalActionValidators.MustBeEmpty(destination),
                BaghChalActionValidators.MustBeInALine(source, target, destination)]);
}
