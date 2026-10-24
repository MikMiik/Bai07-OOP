namespace Bai07.Models;

using Bai07.Enums;

public class ProjectorRemote : TeachingDevice
{
    public int BatteryLevel
    {
        get;
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(BatteryLevel), "Battery level must be between 0 and 100.");
            }

            field = value;
        }
    }

    public ProjectorRemote(string id, string name, string storageLocation, int batteryLevel)
        : base(id, name, storageLocation)
    {
        BatteryLevel = batteryLevel;
    }
    public override bool IsReadyForUse()
    {
        return Status == DeviceStatus.Available && BatteryLevel > 20;
    }
    public override double GetDefaultPriorityScore()
    {
        return BatteryLevel;
    }
}
