using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoSubject;
using GerenciadorEscolar.Data.Dtos.DtoTeacher;
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Criar uma Disciplina ao banco de dados e retornar informações no Header e o objeto criado no Body
    /// </summary>
    /// <param name="subjectDto">Objeto com os campos necessários para criação de uma Disciplina</param>
    /// <returns>retornar seus dados no Body e com Location no Header </returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    /// <response code="400">Erro ao criar Disciplina</response>
    #endregion
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Retorna uma lista de Disciplinas do banco de dados conforme a paginação
    /// </summary>
    /// <param name="skip">primeiro dado para a paginação </param>
    /// <param name="take">ultimo dado para a paginação</param>
    /// <returns> lista mapeada com todas as instancias </returns>
    /// <response code="200">Caso o retorno seja feito com sucesso</response>
    #endregion
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadSubjectDto> GetSubjects([FromQuery] int skip = 0,
                                                   [FromQuery] int take = 10) 
    {
        try
        {
            return _mapper.Map<List<ReadSubjectDto>>(_contex.Subjects.Skip(skip).Take(take).ToList());
        }
        catch (Exception ex) 
        {
            return null;
        }
    }


    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Retorna 1 objeto de Disciplina do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontra a Disciplina especifica no banco de dados </param>
    /// <returns> dado de uma única Disciplina </returns>
    /// <response code="200">Caso o retorno seja feito com sucesso</response>
    /// <response code="400">Erro ao criar a Disciplina</response>
    #endregion
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetSubjectById(int id)
    {
        try
        {
            var findSubjectById = _contex.Subjects.FirstOrDefault(x => x.Id == id);
            if(findSubjectById == null)
            {
                return NotFound();
            }
            var subjectDto = _mapper.Map<ReadTeacherDto>(findSubjectById);
            return Ok(subjectDto);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Atualiza parcialmente o objeto de Disciplina do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontrar uma Disciplina especifica no banco de dados </param>
    /// <param name="patch"> dado para configurar a modificação parcial do objeto </param>
    /// <returns> dados de uma única Disciplina que foi modificado </returns>
    /// <response code="200">Caso a modificação seja feito com sucesso</response>
    /// <response code="400">Erro modificar Disciplina</response>
    #endregion
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Deleta objeto de Disciplina do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontra a Disciplina especifica no banco de dados </param>
    /// <returns> sem contéudo para retornar </returns>
    /// <response code="204">Caso a operação deletar seja feito com sucesso</response>
    /// <response code="400">Erro ao deletar a Disciplina</response>
    #endregion
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
