namespace Academy.Application.Dtos.StudentDtos;

public class CreateStudentDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int Age { get; set; }
    public int GroupId {  get; set; }
}
