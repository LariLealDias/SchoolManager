using GerenciadorEscolar.Data.Dtos.DtoSubject;
using GerenciadorEscolar.Models;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorEscolar.Data.Dtos.DtoTeacher;

public class ReadTeacherDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public long PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DateTimeStamp { get; } = DateTime.Now;


    //Relating
    public ReadSubjectDto ReadSubjectDto { get; set; }
}
