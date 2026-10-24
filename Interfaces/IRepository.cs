namespace Bai07.Interfaces;

public interface IRepository<T>
 where T : IIdentifiable
{
    void Add(T itemId);
    bool Remove(string id);
    T? FindById(string id);
    IReadOnlyList<T> GetAll();
    IReadOnlyList<T> FindAll(Predicate<T> condition);
}