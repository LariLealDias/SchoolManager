using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoStudent;
using GerenciadorEscolar.Models;

namespace GerenciadorEscolar.Profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<CreateStudentDto, StudentModel>();

        CreateMap<StudentModel, ReadStudentDto>()
            //.ForMember(readStudentDto => readStudentDto.readClassDto,
            //opts => opts.MapFrom(student => student.ClassModel))
            ;

        CreateMap<UpdateStudentDto, StudentModel>();
        CreateMap<StudentModel, UpdateStudentDto>();
    }
}
