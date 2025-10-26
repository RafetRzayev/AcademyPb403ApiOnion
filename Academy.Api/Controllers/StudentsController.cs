using Academy.Application.Dtos.StudentDtos;
using Academy.Application.Services.Interfaces;
using Academy.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Academy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filter, [FromQuery] string? includes)
        {
            Expression<Func<Student, bool>>? predicate = null;

            if (!string.IsNullOrEmpty(filter))
            {
                predicate = DynamicExpressionParser.ParseLambda<Student, bool>(
                    new ParsingConfig(), false, filter);
            }

            var students = await _studentService.GetAllAsync(predicate, includes!);
            
            return Ok(students);
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery]int page, int size)
        {
            var students = await _studentService.GetAllWithPaginationAsync(page: page, size : size);

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateStudentDto createStudentDto)
        {
            var createdStudent = await _studentService.CreateAsync(createStudentDto);

            return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            var updatedStudent = await _studentService.UpdateAsync(id, updateStudentDto);
            if (updatedStudent == null)
            {
                return NotFound();
            }
            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
