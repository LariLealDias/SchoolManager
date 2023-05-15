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


    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Criar um Aluno ao banco de dados e retornar informações no Header e o objeto criado no Body
    /// </summary>
    /// <param name="studentDto">Objeto com os campos necessários para criação de um Aluno</param>
    /// <returns> retornar seus dados no Body e com Location no Header </returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    /// <response code="400">Erro ao criar Aluno</response>
    #endregion
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Retorna uma lista de Alunos do banco de dados conforme a paginação
    /// </summary>
    /// <param name="skip">primeiro dado para a paginação </param>
    /// <param name="take">ultimo dado para a paginação</param>
    /// <returns> lista mapeada com todas as instancias </returns>
    /// <response code="200">Caso o retorno seja feito com sucesso</response>
    #endregion
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Retorna 1 objeto de Aluno do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontra o Aluno especifico no banco de dados </param>
    /// <returns> dado de um único Aluno </returns>
    /// <response code="200">Caso o retorno seja feito com sucesso</response>
    /// <response code="400">Erro ao criar o aluno</response>
    #endregion
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Atualiza parcialmente um objeto de Aluno do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontra o Aluno especifico no banco de dados </param>
    /// <param name="patch"> dado configurar a modificação parcial do objeto </param>
    /// <returns> dados de um único aluno que foi modificado </returns>
    /// <response code="200">Caso a modificação seja feito com sucesso</response>
    /// <response code="400">Erro ao modificar Aluno</response>
    #endregion
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Deleta objeto de Aluno do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontra o Aluno especifico no banco de dados </param>
    /// <returns> sem contéudo para retornar </returns>
    /// <response code="204">Caso a operação deletar seja feito com sucesso</response>
    /// <response code="400">Erro ao deletar o Aluno</response>
    #endregion
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent )]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
