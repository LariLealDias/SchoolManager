using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoClass;
using GerenciadorEscolar.Models;

namespace GerenciadorEscolar.Profiles;

public class ClassProfile : Profile
{
    public ClassProfile()
    {
        CreateMap<CreateClassDto, ClassModel>();

        CreateMap<ClassModel, ReadClassDto>();

        CreateMap<UpdateClassDto, ClassModel>();
        CreateMap<ClassModel, UpdateClassDto>();
    }
}
