namespace Bai07.Models;

using Bai07.Interfaces;

public class InMemoryRepository<T> : IRepository<T>
 where T : IIdentifiable
{
    public List<T> DiviceId { get; } = new();

    public void Add(T itemId)
    {
        ArgumentNullException.ThrowIfNull(itemId);
        if (DiviceId.Contains(itemId))
        {
            throw new InvalidOperationException($"Thiết bị {itemId.Id} đã tồn tại trong kho lưu trữ.");
        }
        DiviceId.Add(itemId);
        Console.WriteLine($"Đã thêm thiết bị {itemId.Id} vào kho lưu trữ.");
    }

    public bool Remove(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        var item = DiviceId.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            throw new InvalidOperationException($"Thiết bị {id} không tồn tại trong kho lưu trữ.");
        }
        DiviceId.Remove(item);
        Console.WriteLine($"Đã xóa thiết bị {item.Id} khỏi kho lưu trữ.");
        return true;
    }

    public T? FindById(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return DiviceId.FirstOrDefault(x => x.Id == id);
    }

    public IReadOnlyList<T> GetAll()
    {
        return DiviceId.AsReadOnly();
    }

    public IReadOnlyList<T> FindAll(Predicate<T> condition)
    {
        ArgumentNullException.ThrowIfNull(condition);
        return DiviceId.Where(x => condition(x)).ToList().AsReadOnly();
    }
}