namespace Bai07.Models;

using Bai07.Interfaces;

public class FirstAvailableStrategy<T> : ISelectionStrategy<T>
{
    public T Select(IReadOnlyList<T> candidates)
    {
        if (candidates == null || candidates.Count == 0)
        {
            throw new ArgumentException("Danh sách không được rỗng.", nameof(candidates));
        }

        return candidates[0];
    }
}