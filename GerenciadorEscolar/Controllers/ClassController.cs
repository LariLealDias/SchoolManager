using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoClass;
using GerenciadorEscolar.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Data;

namespace GerenciadorEscolar.Controllers;

[ApiController]
[Route("[controller]")]
public class ClassController : ControllerBase
{
    //Dependence Injection
    private SchoolManagerContext _contex;
    private IMapper _mapper;
    public ClassController(SchoolManagerContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreateClass([FromBody] CreateClassDto classDto)
    {
        try
        {
            ClassModel classModel = _mapper.Map<ClassModel>(classDto);
            _contex.Classes.Add(classModel);
            _contex.SaveChanges();
            return CreatedAtAction(nameof(GetClassById),
                                    new { id = classModel.Id },
                                    classModel);
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet]
    public IEnumerable<ReadClassDto> GetClass([FromQuery] int skip = 0,
                                            [FromQuery] int take = 10)
    {
        return _mapper.Map< List<ReadClassDto> >(_contex.Classes.Skip(skip).Take(take).ToList());
    }


    [HttpGet("{id}")]
    public IActionResult GetClassById(int id)
    {
        try
        {
            var findClassById = _contex.Classes.FirstOrDefault(x => x.Id == id);
            if (findClassById == null)
            {
                return NotFound();
            }
            var classDto = _mapper.Map<ReadClassDto>(findClassById);
            return Ok(classDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPatch("{id}")]
    public IActionResult PartiallyUpdateClass(int id, JsonPatchDocument<UpdateClassDto> patch)
    {
        try
        {
            var findClassById = _contex.Classes.FirstOrDefault(x => x.Id == id);
            if (findClassById == null)
            {
                return NotFound();
            }

            var ClassToUpdate = _mapper.Map<UpdateClassDto>(findClassById);
            patch.ApplyTo(ClassToUpdate, ModelState);

            if (!TryValidateModel(ClassToUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(ClassToUpdate, findClassById);
            _contex.SaveChanges();
            return Ok(_mapper.Map<ReadClassDto>(findClassById));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteClass(int id)
    {
        try
        {
            var findClassById = _contex.Classes.FirstOrDefault(x => x.Id == id);
            if (findClassById == null)
            {
                return NotFound();
            }
            _contex.Remove(findClassById);
            _contex.SaveChanges();
            return NoContent();
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

}
