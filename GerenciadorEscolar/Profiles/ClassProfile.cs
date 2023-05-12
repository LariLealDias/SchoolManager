using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoClass;
using GerenciadorEscolar.Models;

namespace GerenciadorEscolar.Profiles;

public class ClassProfile : Profile
{
    public ClassProfile()
    {
        CreateMap<CreateClassDto, ClassModel>();

        CreateMap<ClassModel, ReadClassDto>()
                            .ForMember(readClassDto => readClassDto.ReadSubjectsDto, 
                            opts => opts.MapFrom(classModel => classModel.Subjects))
                            ;


        CreateMap<UpdateClassDto, ClassModel>();
        CreateMap<ClassModel, UpdateClassDto>();
    }
}
