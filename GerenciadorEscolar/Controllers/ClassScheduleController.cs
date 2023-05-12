using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoClassSchedule;
using GerenciadorEscolar.Models;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Data;

namespace GerenciadorEscolar.Controllers;

[ApiController]
[Route("[Controller]")]
public class ClassScheduleController : ControllerBase
{
    private IMapper _mapper;
    private SchoolManagerContext _contex;

    public ClassScheduleController(IMapper mapper, SchoolManagerContext contex)
    {
        _mapper = mapper;
        _contex = contex;
    }

    [HttpPost]
    public IActionResult CreateClassSchedule([FromBody] CreateClassScheduleDto classScheduleDto)
    {
        try
        {
            ClassScheduleModel classScheduleModel = _mapper.Map<ClassScheduleModel>(classScheduleDto);
            _contex.ClassSchedules.Add(classScheduleModel);
            _contex.SaveChanges();
            return CreatedAtAction(nameof(GetClassScheduleByTwoIds), 
                                    new {
                                        teacherModelId = classScheduleModel.TeacherModelId,
                                        classModelId = classScheduleModel.ClassModelId
                                    },
                                    classScheduleModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IEnumerable<ReadClassScheduleDto> GetClassSchedule([FromQuery] int skip = 0,
                                                              [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadClassScheduleDto>>(_contex.ClassSchedules.Skip(skip).Take(take).ToList());
    }

    [HttpGet("{TeacherModelId}/{ClassModelId}")]
    public IActionResult GetClassScheduleByTwoIds(int teacherModelId, int classModelId)
    {
        try
        {
            ClassScheduleModel findByTwoId = _contex.ClassSchedules.FirstOrDefault(
                                                    classSchedule => classSchedule.TeacherModelId == teacherModelId &&
                                                                     classSchedule.ClassModelId == classModelId);
            if (findByTwoId != null) 
            {
                ReadClassScheduleDto ClassScheduleDto = _mapper.Map<ReadClassScheduleDto>(findByTwoId);
                return Ok(ClassScheduleDto);
            }
                return NotFound();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
