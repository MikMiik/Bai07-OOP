namespace Bai07.Models;

using Bai07.Enums;

public class WirelessMicrophone : TeachingDevice
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
    public int SignalQuality
    {
        get;
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(SignalQuality), "Signal quality must be between 0 and 100.");
            }

            field = value;
        }

    }

    public WirelessMicrophone(string id, string name, string storageLocation, int batteryLevel, int signalQuality)
        : base(id, name, storageLocation)
    {
        BatteryLevel = batteryLevel;
        SignalQuality = signalQuality;
    }
    public override bool IsReadyForUse()
    {
        return Status == DeviceStatus.Available && BatteryLevel > 30 && SignalQuality > 40;
    }
    public override double GetDefaultPriorityScore()
    {
        return (BatteryLevel + SignalQuality) / 2;
    }
}
