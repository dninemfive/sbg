namespace d9.sbg;
public delegate T TransitionFunction<T>(T state);
public delegate Exception? ActionValidator<T>(string actionName, T state);
public interface IGameAction<T>
{
    public string Name { get; }
    public IReadOnlyCollection<ActionValidator<T>> Validators { get; }
    public virtual bool ValidFor(T state, out IEnumerable<Exception> exceptions)
    {
        exceptions = Validators.Select(x => x(Name, state)).NonNullElements();
        return !exceptions.Any();
    }
    protected T ApplyToInternal(T State);
    public virtual T ApplyTo(T state)
    {
        if (!ValidFor(state, out IEnumerable<Exception> exceptions))
            throw new AggregateException(exceptions);
        return ApplyToInternal(state);
    }
}