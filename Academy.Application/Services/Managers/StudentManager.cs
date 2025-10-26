using Academy.Application.Constants;
using Academy.Application.Dtos.StudentDtos;
using Academy.Application.Services.Interfaces;
using Academy.Domain.Entities;
using Academy.Domain.Repositories;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Academy.Application.Services.Managers
{
    public class StudentManager : CrudManager<StudentDto, CreateStudentDto, UpdateStudentDto, Student>, IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IImageService _imageService;
        private readonly FilePathConstants _filePathConstants;

        public StudentManager(IRepositoryAsync<Student> repositoryAsync, IMapper mapper, IStudentRepository studentRepository, IImageService imageService, IOptions<FilePathConstants> options) : base(repositoryAsync, mapper)
        {
            _studentRepository = studentRepository;
            _imageService = imageService;
            _filePathConstants = options.Value;
        }

        public override async Task<StudentDto?> CreateAsync(CreateStudentDto createDto)
        {
            createDto.ImageName = await _imageService.SaveImageAsync(createDto.ImageFile!, _filePathConstants.StudentImagesPath);
           
            return await base.CreateAsync(createDto);
        }
    }
}
