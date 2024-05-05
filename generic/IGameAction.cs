using d9.sbg;

namespace d9.abg.generic;
public delegate T StateTransitionFunction<T>(T state);
public delegate Exception? GameActionValidator<T>(T state);
public interface IGameAction<T>
{
    public string Name { get; }
    protected StateTransitionFunction<T> TransitionFunction { get; }
    public IReadOnlyCollection<GameActionValidator<T>> Validators { get; }
    public virtual bool ValidFor(T state, out IEnumerable<Exception> exceptions)
    {
        exceptions = Validators.Select(x => x(state)).NonNullElements();
        return exceptions.Any();
    }
    public virtual T ApplyTo(T state)
    {
        if (!ValidFor(state, out IEnumerable<Exception> exceptions))
            throw new AggregateException(exceptions);
        return TransitionFunction(state);
    }
}