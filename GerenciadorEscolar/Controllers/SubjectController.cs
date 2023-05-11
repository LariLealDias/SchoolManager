using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoSubject;
using GerenciadorEscolar.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Data;

namespace GerenciadorEscolar.Controllers;

[ApiController]
[Route("[controller]")]

public class SubjectController : ControllerBase
{
    private SchoolManagerContext _contex;
    private IMapper _mapper;
    private readonly ILogger<StudentController> _log;

    public SubjectController(SchoolManagerContext contex, IMapper mapper, ILogger<StudentController> log)
    {
        _contex = contex;
        _mapper = mapper;
        _log = log;
    }

    [HttpPost]
    public IActionResult CreateSuject([FromBody] CreateSubjectDto subjectDto)
    {
            //_log.LogInformation("Fui chamado");
        try
        {
            SubjectModel subjectModel = _mapper.Map<SubjectModel>(subjectDto);
            _contex.Subjects.Add(subjectModel);
            _contex.SaveChanges();
            return CreatedAtAction(nameof(GetSubjectById), 
                                    new {id = subjectModel.Id}, 
                                    subjectModel);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpGet]
    public IEnumerable<ReadSubjectDto> GetSubjects([FromQuery] int skip = 0,
                                                   [FromQuery] int take = 10) 
    {
        try
        {
            return _mapper.Map<List<ReadSubjectDto>>(_contex.Subjects.Skip(skip).Take(take));
        }
        catch (Exception ex) 
        {
            return null;
        }
    }



    [HttpGet("{id}")]
    public IActionResult GetSubjectById(int id)
    {
        try
        {
            var findSubjectById = _contex.Subjects.FirstOrDefault(x => x.Id == id);
            if(findSubjectById == null)
            {
                return NotFound();
            }
            var subjectDto = _mapper.Map<SubjectModel>(findSubjectById);
            return Ok(subjectDto);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPatch("{id}")]
    public IActionResult PartiallyUpdateSubject(int id, JsonPatchDocument<UpdateSubjectDto> patch)
    {
        try
        {
            var findSubjectById = _contex.Subjects.FirstOrDefault(x => x.Id == id);
            if(findSubjectById == null)
            {
                return NotFound();
            }

            var subjectToUpdate = _mapper.Map<UpdateSubjectDto>(findSubjectById);
            patch.ApplyTo(subjectToUpdate, ModelState);

            if (!TryValidateModel(subjectToUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(subjectToUpdate, findSubjectById);

            _contex.SaveChanges();
            return Ok(_mapper.Map<ReadSubjectDto>(findSubjectById));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id}")] 
    public IActionResult DeleteSubject(int id) 
    {
        try
        {
            var findSubjectById = _contex.Subjects.FirstOrDefault(x => x.Id == id);
            if(findSubjectById == null)
            {
                return NotFound();
            }
            _contex.Remove(findSubjectById);
            _contex.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
