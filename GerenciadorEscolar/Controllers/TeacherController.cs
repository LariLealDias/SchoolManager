using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoTeacher;
using GerenciadorEscolar.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Data;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GerenciadorEscolar.Controllers;

[ApiController]
[Route("[Controller]")]
public class TeacherController : ControllerBase
{
    private IMapper _mapper;
    private SchoolManagerContext _contex;
    public TeacherController(IMapper mapper, SchoolManagerContext contex)
    {
        _mapper = mapper;
        _contex = contex;
    }

    [HttpPost]
    public IActionResult CreateTeacher([FromBody] CreateTeacherDto teacherDto)
    {
        try
        {
            TeacherModel teacherModel = _mapper.Map<TeacherModel>(teacherDto);
            _contex.Teachers.Add(teacherModel);
            _contex.SaveChanges();
            return CreatedAtAction(nameof(GetTeacherById),
                                    new {id = teacherModel.Id},
                                    teacherModel);
        }
        catch(Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet]
    public IEnumerable<ReadTeacherDto> GetTeachers([FromQuery] int skip = 0,
                                                   [FromQuery] int take = 10) 
    {
        try
        {
            return _mapper.Map<List<ReadTeacherDto>>(_contex.Teachers.Skip(skip).Take(take).ToList());
        }
        catch (Exception ex)
        {
            return null;
        }
    }



    [HttpGet("{id}")]
    public IActionResult GetTeacherById(int id)
    {
        try
        {
            var findTeacherById = _contex.Teachers.FirstOrDefault(x => x.Id == id);
            if(findTeacherById == null)
            {
                return NotFound();
            }

            var teacherDto = _mapper.Map<ReadTeacherDto>(findTeacherById);
            return Ok(teacherDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public IActionResult PartiallyUpdateTeacher(int id, JsonPatchDocument<UpdateTeacherDto> patch)
    {
        try
        {
            var findTeacherById = _contex.Teachers.FirstOrDefault(x => x.Id == id);
            if(findTeacherById == null)
            {
                return NotFound();
            }

            var teacherToUpdate = _mapper.Map<UpdateTeacherDto>(findTeacherById);

            patch.ApplyTo(teacherToUpdate, ModelState);
            if (!TryValidateModel(teacherToUpdate))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(teacherToUpdate, findTeacherById);
            _contex.SaveChanges();
            return Ok(_mapper.Map<ReadTeacherDto>(findTeacherById));

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTeacher(int id)
    {
        try
        {
            var findTeacherById = _contex.Teachers.FirstOrDefault(x => x.Id == id);
            if (findTeacherById == null)
            {
                return NotFound();
            }

            _contex.Remove(findTeacherById);
            _contex.SaveChanges();
            return NoContent();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
      
    }
}
