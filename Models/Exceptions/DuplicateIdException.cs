namespace Bai07.Models;

public class DuplicateIdException : Exception
{
    public string DuplicateId { get; }

    public DuplicateIdException(string duplicateId) : base($"Trùng ID '{duplicateId}'.")
    {
        DuplicateId = duplicateId;
    }

}