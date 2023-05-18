using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoClass;
using GerenciadorEscolar.Models;
using GerenciadorEscolar.Repository.RepositoryClass;
using Microsoft.AspNetCore.JsonPatch;
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

    //Method for POST
    public ClassModel CreateClass(CreateClassDto classDto)
    {
        ClassModel classModel = _mapper.Map<ClassModel>(classDto);
        _classRepository.AddClassInDatabase(classModel);
        return classModel;
    }


    //Method for GET
    public IEnumerable<ReadClassDto> GetPagingClass(int skip, int take)
    {
        return _mapper.Map<List<ReadClassDto>>(_classRepository.GetPagingInClassDatabase(skip, take).ToList());
    }


    //Method to Find Class By ID
    public ClassModel GetIdClass(int id)
    {
        var findId =_classRepository.FindClassByIdInDatabase(id);
        return findId;
    }


    //Method for GET By ID
    public ReadClassDto MappingClassInReadDto(int id)
    {
        var idClass = GetIdClass(id);
        var classDto = _mapper.Map<ReadClassDto>(idClass);
        return classDto;
    }


    //Methods for PATCH
    public UpdateClassDto MappingClassInUpdateDto(int id)
    {
        var idClass = GetIdClass(id);
        var classDto = _mapper.Map<UpdateClassDto>(idClass);
        return classDto;
    }

    public ClassModel MappingTheObjectUpdatedIntoId(UpdateClassDto classDto, ClassModel IdUpdated)
    {
        var mapping = _mapper.Map(classDto, IdUpdated);
        SaveChanges();
        return mapping;
    }

    public void SaveChanges()
    {
        _classRepository.SaveChanges();
    }



}
