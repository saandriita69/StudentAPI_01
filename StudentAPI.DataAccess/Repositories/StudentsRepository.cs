using Newtonsoft.Json;
using StudentAPI.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPI.DataAccess.Repositories
{
    public class StudentsRepository
    {
        private readonly StudentContext _context;

        public StudentsRepository(StudentContext context)
        {

            _context = context;

            if(_context.Students.Count()==0)
            {
                _context.Students.AddRange((IEnumerable<Student>)(GetStudentsFromFile()));

                _context.SaveChanges();
            }

        }

        public List<Student> GetStudents() =>
               _context.Students.OrderBy(s => s.SurName).ToList();


        public bool TryGetStudent(Guid id, out Student student)
        {
            student = _context.Students.Find(id);

            return (student != null);
        }

        public async Task<int> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }

        public async Task<int> RemoveStudentAsync(Guid id)
        {

            var s = _context.Students.Where(s => s.ID == id);
            _context.Students.Remove(_context.Students.Single(s => s.ID == id));
            int rowsAffected = await  _context.SaveChangesAsync();
            return rowsAffected;
        }

        public async Task<int> UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;

        }

        private List<Student> GetStudentsFromFile()
        {
            string path = @"C:\Projects\StudentAPI_01\StudentAPI.DataAccess\Data\student.json";
            using (StreamReader jsonStream = File.OpenText(path))
            {
                var json = jsonStream.ReadToEnd();

                Guid id = new Guid();

                var list = JsonConvert.DeserializeObject<Students>(json); ;
                Students studentList = JsonConvert.DeserializeObject<Students>(json);
                return studentList.Student.ToList();
            }

        }

    }
}
