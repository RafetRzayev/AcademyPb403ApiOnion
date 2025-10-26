using Academy.Application.Dtos.StudentDtos;
using Academy.Application.Services.Interfaces;
using Academy.Domain.Entities;
using Academy.Domain.Repositories;
using AutoMapper;
using System.Linq.Expressions;

namespace Academy.Application.Services.Managers
{
    public class StudentManager : CrudManager<StudentDto, CreateStudentDto, UpdateStudentDto, Student>, IStudentService
    {
        private readonly IStudentRepository _studentRepository;
   
        public StudentManager(IRepositoryAsync<Student> repositoryAsync, IMapper mapper, IStudentRepository studentRepository) : base(repositoryAsync, mapper)
        {
            _studentRepository = studentRepository;
        }
    }
}
