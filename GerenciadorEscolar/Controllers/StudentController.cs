using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoStudent;
using GerenciadorEscolar.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Data;

namespace GerenciadorEscolar.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    //Dependence Injection
    private SchoolManagerContext _contex;
    private IMapper _mapper;
    private readonly ILogger<StudentController> _log;


    public StudentController(SchoolManagerContext contex, IMapper mapper, ILogger<StudentController> log)
    {
        _contex = contex;
        _mapper = mapper;
        _log = log;
    }


    [HttpPost]
    public IActionResult CreateStudent([FromBody] CreateStudentDto studentDto)
    {
        try
        {
            StudentModel studentModel = _mapper.Map<StudentModel>(studentDto);
            _contex.Students.Add(studentModel);
            _contex.SaveChanges();
            return CreatedAtAction(nameof(GetStudentById), 
                                    new { id = studentModel.Id},
                                    studentModel);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IEnumerable<ReadStudentDto> GetStudent([FromQuery] int skip = 0,
                                                  [FromQuery] int take = 10)
    {
        try
        {
            //_log.LogInformation("Fui chamado");

            var allStudents = _mapper.Map<List<ReadStudentDto>>(_contex.Students.Skip(skip).Take(take).ToList());

            // Verifica se a lista de estudantes não está vazia
            if (allStudents.Count == 0)
            {
                string errorMessage = "  A consulta não retornou estudantes válidos.";
                _log.LogError(errorMessage);
            }

            return _mapper.Map<List<ReadStudentDto>>(allStudents);
        }
        catch (Exception ex)
        {
            return null;
        }
    }



    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        try
        {
            var findStudentById = _contex.Students.FirstOrDefault(x => x.Id == id);
            if (findStudentById == null)
            {
                return NotFound();
            }
            var studentDto = _mapper.Map<ReadStudentDto>(findStudentById);

            return Ok(studentDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public IActionResult PartiallyUpdateStudent(int id, JsonPatchDocument<UpdateStudentDto> patch)
    {
        try
        {
            var findStudentById = _contex.Students.FirstOrDefault(x => x.Id == id);

            if (findStudentById == null)
            {
                return NotFound();
            }

            var StudentToUpdate = _mapper.Map<UpdateStudentDto>(findStudentById);
            patch.ApplyTo(StudentToUpdate, ModelState);

            if (!TryValidateModel(StudentToUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(StudentToUpdate, findStudentById);
            _contex.SaveChanges();
            return Ok(_mapper.Map<ReadStudentDto>(findStudentById));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        try
        {
            var findStudentById = _contex.Students.FirstOrDefault(x => x.Id == id);

            if (findStudentById == null)
            {
                return NotFound();
            }
            _contex.Remove(findStudentById);
            _contex.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
