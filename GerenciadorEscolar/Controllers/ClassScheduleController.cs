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

    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Criar uma Aula ao banco de dados e retornar informações no Header
    /// </summary>
    /// <param name="classScheduleDto">Objeto com os campos necessários para criação de um Aula</param>
    /// <returns> retornar seus dados com Location no Header </returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    /// <response code="400">Erro ao criar o Turma</response>
    #endregion
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Retorna uma lista de Aulas do banco de dados conforme a paginação
    /// </summary>
    /// <param name="skip">primeiro dado para a paginação </param>
    /// <param name="take">ultimo dado para a paginação</param>
    /// <returns> lista mapeada com todas as instancias </returns>
    /// <response code="200">Caso o retorno seja feito com sucesso</response>
    #endregion
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadClassScheduleDto> GetClassSchedule([FromQuery] int skip = 0,
                                                              [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadClassScheduleDto>>(_contex.ClassSchedules.Skip(skip).Take(take).ToList());
    }


    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Retorna 1 objeto de Aula do banco de dados conforme o Id do Professor e o Id da Turma 
    /// </summary>
    /// <param name="teacherModelId"> dado para encontra o Professor relacionado a Aula no banco de dados </param>
    /// <param name="classModelId"> dado para encontra a Turma relacionada a Aula no banco de dados </param>
    /// <returns> dado de uma única Aula </returns>
    /// <response code="200">Caso o retorno seja feito com sucesso</response>
    /// <response code="400">Erro ao criar a Turma</response>
    #endregion
    [HttpGet("{TeacherModelId}/{ClassModelId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
