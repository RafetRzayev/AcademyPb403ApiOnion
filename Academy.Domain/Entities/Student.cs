namespace Academy.Domain.Entities;

public class Student : Entity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int Age {  get; set; }
    public string? ImageName {  get; set; }

    public int GroupId {  get; set; }
    public Group? Group { get; set; }
}