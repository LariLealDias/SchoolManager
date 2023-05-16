using System.ComponentModel.DataAnnotations;

namespace GerenciadorEscolar.Models;

public class SubjectModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Fild Title is required")]
    [StringLength(30, ErrorMessage = "Fild Title could only aceppt till 30 characters")]
    public string Title { get; set; }



    //Relating 1 Teacher to 1 Subject
    public virtual TeacherModel TeacherModel { get; set; }


    //Accessing relationship 1 Class to multiple Subjects
    //public int? ClassModelId { get; set; }
    //public virtual ClassModel ClassModel { get; set; }
}
