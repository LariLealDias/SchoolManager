using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoSubject;
using GerenciadorEscolar.Models;

namespace GerenciadorEscolar.Profiles;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<CreateSubjectDto, SubjectModel>();

        CreateMap<SubjectModel, ReadSubjectDto>();

        CreateMap<UpdateSubjectDto, SubjectModel>();
        CreateMap<SubjectModel, UpdateSubjectDto>();
    }
}
