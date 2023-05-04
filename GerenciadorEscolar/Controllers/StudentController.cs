using GerenciadorEscolar.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorEscolar.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private static List<StudentModel> Students = new List<StudentModel>();
    private static int id = 1;
    [HttpPost]
    public IActionResult CreateStudent([FromBody] StudentModel student)
    {
        try
        {
            id++;
            id = student.Id;
            Students.Add(student);
            return CreatedAtAction(nameof(GetStudentById), 
                                    new { id = student.Id}, 
                                    Students);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IEnumerable<StudentModel> GetStudents()
    {
        return Students;
    }



    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        try
        {
            var findStudent = Students.FirstOrDefault(x => x.Id == id);
            if (findStudent == null)
            {
                return NotFound();
            }
            return Ok(findStudent);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
