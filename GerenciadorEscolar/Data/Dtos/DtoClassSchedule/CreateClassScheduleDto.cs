using System.ComponentModel.DataAnnotations;

namespace GerenciadorEscolar.Data.Dtos.DtoClassSchedule;

public class CreateClassScheduleDto
{
    [Required(ErrorMessage = "Fild ClassTime is required -- Follow the type HH:mm")]
    [DataType(DataType.Time)]
    public DateTime ClassTime { get; set; }
    public int? TeacherModelId { get; set; }
    public int? ClassModelId { get; set; }

    public DateTime? CreatedAt { get; } = DateTime.Now;

}
