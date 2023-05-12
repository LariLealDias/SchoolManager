﻿using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoTeacher;
using GerenciadorEscolar.Models;

namespace GerenciadorEscolar.Profiles;

public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        CreateMap<CreateTeacherDto, TeacherModel>();

        CreateMap<TeacherModel, ReadTeacherDto>()
            .ForMember(readTeacherDto => readTeacherDto.ReadSubjectDto,
            opts => opts.MapFrom(teacher => teacher.SubjectModel))
            .ForMember(readTeacherDto => readTeacherDto.ReadClassDto, 
            opts => opts.MapFrom(teacher => teacher.Classes))
            ;

        CreateMap<UpdateTeacherDto, TeacherModel>();
        CreateMap<TeacherModel, UpdateTeacherDto>();
    }
}
