using Academy.Application.Dtos.StudentDtos;
using Academy.Domain.Entities;

namespace Academy.Application.Services.Interfaces
{
    public interface IStudentService : ICrudServiceAsync<StudentDto, CreateStudentDto, UpdateStudentDto, Student>
    {

    }
}
