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


    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Criar um Professor ao banco de dados e retornar informações no Header  
    /// </summary>
    /// <param name="teacherDto">Objeto com os campos necessários para criação de um Professor</param>
    /// <returns> retornar seus dados com Location no Header </returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    /// <response code="400">Erro ao criar Professor</response>
    #endregion
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Retorna uma lista de Professores do banco de dados conforme a paginação
    /// </summary>
    /// <param name="skip">primeiro dado para a paginação </param>
    /// <param name="take">ultimo dado para a paginação</param>
    /// <returns> lista mapeada com todas as instancias </returns>
    /// <response code="200">Caso o retorno seja feito com sucesso</response>
    #endregion
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
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


    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Retorna 1 objeto de Professor do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontra o Professor especifico no banco de dados </param>
    /// <returns> dado de um único Professor </returns>
    /// <response code="200">Caso o retorno seja feito com sucesso</response>
    /// <response code="400">Erro ao retornar a Disciplina</response>
    #endregion
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Atualiza parcialmente o objeto de Professor do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontrar um Professor especifico no banco de dados </param>
    /// <param name="patch"> dado para configurar a modificação parcial do objeto </param>
    /// <returns> dados de um único Professor que foi modificado </returns>
    /// <response code="200">Caso o retorno seja feito com sucesso</response>
    /// <response code="400">Erro ao modificar a Disciplina</response>
    #endregion
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Deleta objeto de Professor do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontrar Professor especifico no banco de dados </param>
    /// <returns> dados de um único Professor que foi modificado </returns>
    /// <response code="204">Caso o retorno seja feito com sucesso</response>
    /// <response code="400">Erro ao deletar a Disciplina</response>
    #endregion
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
