using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoClass;
using GerenciadorEscolar.Models;
using GerenciadorEscolar.Services;
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
    //private ClassService _classService;
    public ClassController(SchoolManagerContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
        //_classService = classService;
    }


    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Criar uma Turma ao banco de dados e retornar informações no Header e o objeto criado no Body
    /// </summary>
    /// <param name="classDto">Objeto com os campos necessários para criação de uma Turma</param>
    /// <returns> retornar seus dados no Body e com Location no Header </returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    /// <response code="400">Erro ao criar Turma</response>
    #endregion
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


            //var createdClass = _classService.CreateClass(classDto);
            //var resourceUrl = Url.Action("GetClassById", new { id = createdClass.Id });

            //return Created(resourceUrl, createdClass);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Retorna uma lista de Turmas do banco de dados conforme a paginação
    /// </summary>
    /// <param name="skip">primeiro dado para a paginação </param>
    /// <param name="take">ultimo dado para a paginação</param>
    /// <returns> lista mapeada com todas as instancias </returns>
    /// <response code="200">Caso o retorno seja feito com sucesso</response>
    #endregion
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadClassDto> GetClass([FromQuery] int skip = 0,
                                            [FromQuery] int take = 10)
    {
        return _mapper.Map< List<ReadClassDto> >(_contex.Classes.Skip(skip).Take(take).ToList());
    }



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Retorna 1 objeto de Turma do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontra a Turma especifica no banco de dados </param>
    /// <returns> dado de uma única Turma </returns>
    /// <response code="200">Caso o retorno seja feito com sucesso</response>
    /// <response code="400">Erro ao criar o aluno</response>
    #endregion
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Atualiza parcialmente um objeto de Turma do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontrar uma Turma especifica no banco de dados </param>
    /// <param name="patch"> dado para configurar a modificação parcial do objeto </param>
    /// <returns> dados de uma única Turma que foi modificado </returns>
    /// <response code="200">Caso a modificação seja feito com sucesso</response>
    /// <response code="400">Erro ao modificar Turma</response>
    #endregion
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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



    #region ----------------------   Ducumentação API ---------------------
    /// <summary>
    /// Deleta objeto de Turma do banco de dados conforme seu Id
    /// </summary>
    /// <param name="id"> dado para encontra a Turma especifica no banco de dados </param>
    /// <returns> sem contéudo para retornar </returns>
    /// <response code="204">Caso a operação deletar seja feito com sucesso</response>
    /// <response code="400">Erro ao deletar a Turma</response>
    #endregion
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
