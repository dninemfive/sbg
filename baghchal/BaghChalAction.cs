namespace d9.sbg.baghchal;
public class BaghChalAction(string name, TransitionFunction<BaghChalState> transitionFunction, params ActionValidator<BaghChalState>[] validators)
    : IGameAction<BaghChalState>
{
    public string Name { get; } = name;
    public IReadOnlyCollection<ActionValidator<BaghChalState>> Validators { get; } = validators.ToList();
    BaghChalState IGameAction<BaghChalState>.ApplyToInternal(BaghChalState state) => transitionFunction(state);
    public BaghChalAction(string name, TransitionFunction<BaghChalState> tf, IEnumerable<ActionValidator<BaghChalState>> validators)
        : this(name, tf, validators.ToArray()) { }
}