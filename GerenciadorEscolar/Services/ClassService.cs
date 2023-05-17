using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoClass;
using GerenciadorEscolar.Models;
using GerenciadorEscolar.Repository.RepositoryClass;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.Data;

namespace GerenciadorEscolar.Services;

public class ClassService
{
    private IClassRepository _classRepository;
    private IMapper _mapper;
    public ClassService(IClassRepository classRepository, IMapper mapper)
    {
        _classRepository = classRepository;
        _mapper = mapper;
    }

    public ClassModel CreateClass(CreateClassDto classDto)
    {
        ClassModel classModel = _mapper.Map<ClassModel>(classDto);
        _classRepository.AddClassInDatabase(classModel);
        return classModel;
    }

    public IEnumerable<ReadClassDto> GetPagingClass(int skip, int take)
    {
        return _mapper.Map<List<ReadClassDto>>(_classRepository.GetPagingInClassDatabase(skip, take).ToList());
    }

    public ClassModel GetIdClass(int id)
    {
        var findId =_classRepository.FindClassByIdInDatabase(id);
        return findId;
    }

    public ReadClassDto MappingTheGetIdClassInDto(int id)
    {
        var idClass = GetIdClass(id);
        var classDto = _mapper.Map<ReadClassDto>(idClass);
        return classDto;

    }
}
