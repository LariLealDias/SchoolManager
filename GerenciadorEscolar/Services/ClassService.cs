using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoClass;
using GerenciadorEscolar.Models;
using GerenciadorEscolar.Repository.RepositoryClass;
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


}
