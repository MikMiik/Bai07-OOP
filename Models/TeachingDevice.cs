using Bai07.Enums;
using Bai07.Interfaces;
namespace Bai07.Models;

public abstract class TeachingDevice : IIdentifiable
{
    public string Id { get; }
    public string Name { get; }
    public string StorageLocation { get; }
    public DeviceStatus Status { get; protected set; }

    public TeachingDevice(string id, string name, string storageLocation)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(storageLocation);

        Id = id;
        Name = name;
        StorageLocation = storageLocation;
        Status = DeviceStatus.Available;
    }
    public abstract bool IsReadyForUse();
    public abstract double GetDefaultPriorityScore();
    public void Borrow()
    {
        if (IsReadyForUse())
        {
            Status = DeviceStatus.Borrowed;
            Console.WriteLine($"Device {Id} is borrowed.");
        }
        else
        {
            throw new InvalidOperationException("Device is not available for borrowing.");
        }
    }
    public void Return()
    {
        if (Status == DeviceStatus.Borrowed)
        {
            Status = DeviceStatus.Available;
            Console.WriteLine($"Device {Id} is returned.");
        }
        else
        {
            throw new InvalidOperationException("Device is not currently borrowed.");
        }
    }
}