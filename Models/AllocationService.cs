namespace Bai07.Models;

using Bai07.Interfaces;
using Bai07.Enums;

public class AllocationService<T>
 where T : TeachingDevice
{
    public readonly IRepository<T> Repository;
    public ISelectionStrategy<T> Strategy { get; private set; }
    public AllocationService(IRepository<T> repository, ISelectionStrategy<T> strategy)
    {
        Repository = repository;
        Strategy = strategy;
    }
    public void ChangeStrategy(ISelectionStrategy<T> newStrategy)
    {
        Strategy = newStrategy;
        Console.WriteLine($"Chiến lược phân bổ đã được thay đổi thành: {newStrategy.GetType().Name}");
    }
    public T Allocate(Predicate<T> additionalCondition)
    {
        var availableDevices = Repository.FindAll(device => device.Status == DeviceStatus.Available && device.IsReadyForUse() && additionalCondition(device));
        if (!availableDevices.Any())
        {
            throw new InvalidOperationException("Không có thiết bị nào sẵn sàng để phân bổ.");
        }
        var selectedDevice = Strategy.Select(availableDevices);
        selectedDevice.Borrow();
        return selectedDevice;
    }
}