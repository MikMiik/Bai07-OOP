namespace Bai07.Models;

using Bai07.Interfaces;

public class InMemoryRepository<T> : IRepository<T>
 where T : IIdentifiable
{
    public List<T> TeachingDevices { get; } = new();

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item);
        if (TeachingDevices.Contains(item))
        {
            throw new InvalidOperationException($"Thiết bị {item.Id} đã tồn tại trong kho lưu trữ.");
        }
        TeachingDevices.Add(item);
        Console.WriteLine($"Đã thêm thiết bị {item.Id} vào kho lưu trữ.");
    }

    public bool Remove(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        var item = TeachingDevices.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return false;
        }
        TeachingDevices.Remove(item);
        return true;
    }

    public T? FindById(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return TeachingDevices.FirstOrDefault(x => x.Id == id);
    }

    public IReadOnlyList<T> GetAll()
    {
        return TeachingDevices.AsReadOnly();
    }

    public IReadOnlyList<T> FindAll(Predicate<T> condition)
    {
        ArgumentNullException.ThrowIfNull(condition);
        return TeachingDevices.Where(x => condition(x)).ToList();
    }
}