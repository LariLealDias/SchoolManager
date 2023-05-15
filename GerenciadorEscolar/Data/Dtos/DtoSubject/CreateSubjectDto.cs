using GerenciadorEscolar.Models;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorEscolar.Data.Dtos.DtoSubject;

public class CreateSubjectDto
{
    [Required(ErrorMessage = "Fild Title is required")]
    [StringLength(30, ErrorMessage = "Fild Title could only aceppt till 30 characters")]
    public string Title { get; set; }

    public DateTime? CreatedAt { get; } = DateTime.Now;


    //relationship between 1 Class with N Subjects
    public int ClassModelId { get; set; }

}
