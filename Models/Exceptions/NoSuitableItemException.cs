namespace Bai07.Models;

public class NoSuitableItemException : Exception
{
    public NoSuitableItemException() : base($"Không có thiết bị phù hợp với điều kiện.")
    {

    }

}