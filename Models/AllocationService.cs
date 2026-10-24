namespace Bai07.Models;

using Bai07.Interfaces;

public class AllocationService<T>
 where T : TeachingDevice
{
    private readonly IRepository<T> repository;
    private ISelectionStrategy<T> strategy;
    public AllocationService(IRepository<T> repository, ISelectionStrategy<T> strategy)
    {
        repository = repository ?? throw new ArgumentNullException(nameof(repository));
        strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }
    public void ChangeStrategy(ISelectionStrategy<T> newStrategy);
    public T Allocate(Predicate<T> additionalCondition);
}