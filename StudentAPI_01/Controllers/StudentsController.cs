using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DataAccess.Models;
using StudentAPI.DataAccess.Repositories;

namespace StudentAPI_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsRepository _repository;

        public StudentsController(StudentsRepository repository)
        {
            _repository = repository;
        }

        #region snippet_Get
        [HttpGet]
        public List<Student> Get() =>
            _repository.GetStudents();
        #endregion

        #region snippet_GetById
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> GetById(Guid id)
        {
            if (!_repository.TryGetStudent(id, out var student))
            {
                return NotFound();
            }

            return student;
        }
        #endregion

        #region snippet_CreateAsync
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Student>> CreateAsync(Student student)
        {
            await _repository.AddStudentAsync(student);

            return CreatedAtAction(nameof(GetById), new { id = student.ID }, student);
        }
        #endregion


        #region snippet_Put
        [HttpPut]
        public async Task<ActionResult<int>> UpdateAsync(Student student)
        {
            var s = await _repository.UpdateStudentAsync(student);
            return s;
        }
        #endregion


        #region snippet_Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(Guid id)
        {
            int result = await _repository.RemoveStudentAsync(id);
            return result;

        }
        #endregion

    }
}
