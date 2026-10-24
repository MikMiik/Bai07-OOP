namespace Bai07.Interfaces;

public interface ISelectionStrategy<T>
{
    T Select(IReadOnlyList<T> candidates);
}