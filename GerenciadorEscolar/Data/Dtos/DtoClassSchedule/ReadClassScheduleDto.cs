namespace GerenciadorEscolar.Data.Dtos.DtoClassSchedule;

public class ReadClassScheduleDto
{
    public DateTime ClassTime { get; set; }
    public int TeacherModelId { get; set; }
    public int ClassModelId { get; set; }
    public DateTime? DateTimeStamp { get; } = DateTime.Now;

}
