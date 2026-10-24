namespace Bai07.Models;

using Bai07.Enums;

public class DeviceNotReadyException : Exception
{
    public string DeviceId { get; }
    public DeviceStatus CurrentStatus { get; }

    public DeviceNotReadyException(string deviceId, DeviceStatus currentStatus)
        : base($"Thiết bị '{deviceId}' không sẵn sàng để sử dụng. Trạng thái hiện tại: {currentStatus}.")
    {
        DeviceId = deviceId;
        CurrentStatus = currentStatus;
    }
}