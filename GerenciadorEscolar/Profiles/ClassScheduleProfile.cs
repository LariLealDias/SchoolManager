using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoClassSchedule;
using GerenciadorEscolar.Models;

namespace GerenciadorEscolar.Profiles;

public class ClassScheduleProfile : Profile
{
    public ClassScheduleProfile()
    {
        CreateMap<CreateClassScheduleDto, ClassScheduleModel>();

        CreateMap<ClassScheduleModel, ReadClassScheduleDto>();

        CreateMap<UpdateClassScheduleDto, ClassScheduleModel>();
        CreateMap<ClassScheduleModel, UpdateClassScheduleDto>();
    }
}
