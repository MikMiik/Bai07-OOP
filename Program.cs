using Bai07.Models;
using Bai07.Enums;

/*  
202418946
Phạm Văn Minh
*/

// 3 điều khiển máy chiếu có mức pin khác nhau. 
ProjectorRemote projectorRemote1 = new("PR01", "Máy chiếu 1", "Kho 1", 80);
ProjectorRemote projectorRemote2 = new("PR02", "Máy chiếu 2", "Kho 2", 50);
ProjectorRemote projectorRemote3 = new("PR03", "Máy chiếu 3", "Kho 1", 10);

// 2 điều khiển điều hòa với tập phòng tương thích khác nhau. 
AirConditionerRemote airConditionerRemote1 = new("AC01", "Điều hòa 1", "Phòng 1");
AirConditionerRemote airConditionerRemote2 = new("AC02", "Điều hòa 2", "Phòng 3");

//  3 micro có pin và chất lượng tín hiệu khác nhau. 
WirelessMicrophone microphone1 = new("MIC01", "Micro 1", "Kho 1", 90, 80);
WirelessMicrophone microphone2 = new("MIC02", "Micro 2", "Kho 2", 60, 70);
WirelessMicrophone microphone3 = new("MIC03", "Micro 3", "Kho 1", 30, 50);

// 1. Thêm thiết bị vào repository. 
Console.WriteLine("1. Thêm thiết bị vào kho lưu trữ");
InMemoryRepository<TeachingDevice> repository1 = new InMemoryRepository<TeachingDevice>();
repository1.Add(projectorRemote1);
// 2. Thử thêm hai thiết bị trùng mã.
Console.WriteLine("2. Thử thêm hai thiết bị trùng mã");
repository1.Add(projectorRemote2);
try
{
    repository1.Add(projectorRemote2);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Lỗi: {ex.Message}");
}
// 3. Tìm thiết bị theo mã.
Console.WriteLine("3. Tìm thiết bị theo mã");
var foundDevice = repository1.FindById("PR01");
if (foundDevice != null)
{
    Console.WriteLine($"Tìm thấy thiết bị: {foundDevice.Name}");
}
else
{
    Console.WriteLine("Không tìm thấy thiết bị");
}
// 4. Lọc các thiết bị sẵn sàng.
Console.WriteLine("4. Lọc các thiết bị sẵn sàng");
var availableDevices = repository1.FindAll(device => device.IsReadyForUse());
Console.WriteLine($"Có {availableDevices.Count} thiết bị sẵn sàng: {string.Join(", ", availableDevices.Select(d => d.Name))}");
// 5. Chọn điều khiển máy chiếu có điểm cao nhất.
Console.WriteLine("5. Chọn điều khiển máy chiếu có điểm cao nhất");
HighestPriorityStrategy<ProjectorRemote> strategySelectHighestPRScore = new(d => d.GetDefaultPriorityScore());
var selectedProjector = strategySelectHighestPRScore.Select(new List<ProjectorRemote> { projectorRemote1, projectorRemote2, projectorRemote3 });
Console.WriteLine($"Điều khiển máy chiếu được chọn: {selectedProjector.Name} với điểm ưu tiên: {selectedProjector.GetDefaultPriorityScore()}");
// 6. Chọn điều khiển điều hòa tương thích với một phòng.
Console.WriteLine("6. Chọn điều khiển điều hòa tương thích với một phòng.");
var repository2 = new InMemoryRepository<AirConditionerRemote>();
var compatibleRooms = new HashSet<string> { "Phòng 1", "Phòng 2", "Phòng 3" };
AirConditionerRemote airConditionerRemote3 = new("AC03", "Điều hòa 3", "Kho 1", compatibleRooms);
AirConditionerRemote airConditionerRemote4 = new("AC04", "Điều hòa 4", "Kho 3", compatibleRooms);
AirConditionerRemote airConditionerRemote5 = new("AC05", "Điều hòa 5", "Kho 4", compatibleRooms);
AirConditionerRemote airConditionerRemote6 = new("AC06", "Điều hòa 6", "Kho 2", compatibleRooms);
repository2.Add(airConditionerRemote3);
repository2.Add(airConditionerRemote4);
repository2.Add(airConditionerRemote5);
repository2.Add(airConditionerRemote6);

var condition = new Predicate<AirConditionerRemote>(device => device.IsCompatibleWith("Phòng 2"));
var result = repository2.FindAll(condition);
Console.WriteLine($"Có {result.Count} điều khiển điều hòa tương thích với Phòng 2: {string.Join(", ", result.Select(d => d.Name))}");

// 7. Chọn micro đầu tiên sẵn sàng.
Console.WriteLine("7. Chọn micro đầu tiên sẵn sàng");
FirstAvailableStrategy<WirelessMicrophone> firstAvailableStrategyMicrophone = new();
var selectedMicro = firstAvailableStrategyMicrophone.Select(new List<WirelessMicrophone> { microphone1, microphone2, microphone3 });
Console.WriteLine($"Micro được chọn: {selectedMicro.Name}");

// 8. Đổi chiến lược trong thời gian chạy và quan sát kết quả.
Console.WriteLine("8. Đổi chiến lược trong thời gian chạy và quan sát kết quả");
HighestPriorityStrategy<TeachingDevice> strategySelectHighestPRScore2 = new(d => d.GetDefaultPriorityScore());
FirstAvailableStrategy<TeachingDevice> availableStrategy2 = new();

AllocationService<TeachingDevice> allocationService = new(repository1, strategySelectHighestPRScore2);
allocationService.ChangeStrategy(availableStrategy2);
// 9. Thử cấp phát khi không có ứng viên phù hợp.
Console.WriteLine("9. Thử cấp phát khi không có ứng viên phù hợp");
try
{
    var allocatedDevice = allocationService.Allocate(device => device.StorageLocation == "Kho 9");
    Console.WriteLine($"Đã cấp phát: {allocatedDevice.Name}");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Lỗi: {ex.Message}");
}
// 10. Chứng minh thiết bị được chọn chuyển sang Borrowed.
Console.WriteLine("10. Chứng minh thiết bị được chọn chuyển sang Borrowed.");
Console.WriteLine($"Trạng thái trước khi cấp phát: {projectorRemote1.Name} - {projectorRemote1.Status}");
var device = allocationService.Allocate(device => device.StorageLocation == "Kho 1");
Console.WriteLine($"Thiết bị được cấp phát: {device.Name}, trạng thái hiện tại: {device.Status}");
// 11. Chứng minh repository tổng quát có thể dùng cho ít nhất hai loại thiết bị mà không
// sao chép mã. 
Console.WriteLine("11. Chứng minh repository tổng quát có thể dùng cho ít nhất hai loại thiết bị mà không sao chép mã.");
var repository3 = new InMemoryRepository<TeachingDevice>();

repository3.Add(airConditionerRemote1);
repository3.Add(airConditionerRemote2);
repository3.Add(microphone1);

Console.WriteLine($"Tổng số thiết bị trong repository1: {repository3.GetAll().Count}");

