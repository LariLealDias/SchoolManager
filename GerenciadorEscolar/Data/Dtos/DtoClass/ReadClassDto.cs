using GerenciadorEscolar.Data.Dtos.DtoSubject;
using GerenciadorEscolar.Models;

namespace GerenciadorEscolar.Data.Dtos.DtoClass;

public class ReadClassDto
{
    public int Id { get; set; }
    public string Grade { get; set; }
    public string Section { get; set; }
    public string Shift { get; set; }
    public DateTime? DateTimeStamp { get; } = DateTime.Now;

    public ICollection<ReadSubjectDto> ReadSubjectsDto { get; set; }

}
