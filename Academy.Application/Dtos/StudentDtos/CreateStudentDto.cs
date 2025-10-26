using Microsoft.AspNetCore.Http;

namespace Academy.Application.Dtos.StudentDtos;

public class CreateStudentDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? ImageName { get; set; }
    public IFormFile? ImageFile { get; set; }
    public int Age { get; set; }
    public int GroupId {  get; set; }
}
