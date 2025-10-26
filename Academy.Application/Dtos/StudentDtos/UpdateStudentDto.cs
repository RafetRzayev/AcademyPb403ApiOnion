namespace Academy.Application.Dtos.StudentDtos;

public class UpdateStudentDto
{
    public int Id {  set; get; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int Age { get; set; }
    public int GroupId { get; set; }
}
