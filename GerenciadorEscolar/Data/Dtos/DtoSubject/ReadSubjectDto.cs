namespace GerenciadorEscolar.Data.Dtos.DtoSubject;

public class ReadSubjectDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime? DateTimeStamp { get; } = DateTime.Now;
}
