using System.ComponentModel.DataAnnotations;

namespace GerenciadorEscolar.Models;

public class SubjectModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Fild Title is required")]
    [StringLength(30, ErrorMessage = "Fild Title could only aceppt till 30 characters")]
    public string Title { get; set; }

    ////Relating 1 Teacher to 1 Subject
    public virtual TeacherModel TeacherModel { get; set; }
}
