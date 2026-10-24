namespace Bai07.Models;

using Bai07.Interfaces;

public class HighestPriorityStrategy<T> : ISelectionStrategy<T>
{
    public Func<T, double> ScoreSelector { get; }
    public HighestPriorityStrategy(Func<T, double> scoreSelector)
    {
        ScoreSelector = scoreSelector;
    }
    public T Select(IReadOnlyList<T> candidates)
    {
        if (candidates == null || candidates.Count == 0)
        {
            throw new ArgumentException("Danh sách không được rỗng.", nameof(candidates));
        }

        T selectedCandidate = candidates[0];
        double highestScore = ScoreSelector(selectedCandidate);

        foreach (var candidate in candidates)
        {
            double score = ScoreSelector(candidate);
            if (score > highestScore)
            {
                highestScore = score;
                selectedCandidate = candidate;
            }
        }

        return selectedCandidate;
    }
}