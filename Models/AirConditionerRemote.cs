
namespace Bai07.Models;

using Bai07.Enums;

public class AirConditionerRemote : TeachingDevice
{
    public IReadOnlySet<string> CompatibleRooms { get; } = new HashSet<string>();
    public AirConditionerRemote(string id, string name, string storageLocation)
       : base(id, name, storageLocation)
    {
    }

    public AirConditionerRemote(string id, string name, string storageLocation, IReadOnlySet<string> compatibleRooms)
        : base(id, name, storageLocation)
    {
        CompatibleRooms = compatibleRooms ?? throw new ArgumentNullException(nameof(compatibleRooms));
    }
    public bool IsCompatibleWith(string roomId)
    {
        if (string.IsNullOrWhiteSpace(roomId))
        {
            throw new ArgumentException("Room ID cannot be null or whitespace.", nameof(roomId));
        }

        return CompatibleRooms.Contains(roomId);
    }
    public override bool IsReadyForUse()
    {
        return Status == DeviceStatus.Available;
    }
    public override double GetDefaultPriorityScore()
    {
        return CompatibleRooms.Count > 0 ? 100 : 0;
    }
}