using System.ComponentModel.DataAnnotations;

namespace GerenciadorEscolar.Models;

public class ClassScheduleModel
{
    [Required(ErrorMessage = "Fild ClassTime is required -- Follow the type HH:mm")]
    [DataType(DataType.Time)]
    public DateTime ClassTime { get; set; }


    //Relating n:n
    public int? TeacherModelId { get; set; }
    public virtual TeacherModel TeacherModel { get; set; }

    public int? ClassModelId { get; set; }
    public virtual ClassModel ClassModel { get; set; }  

}
