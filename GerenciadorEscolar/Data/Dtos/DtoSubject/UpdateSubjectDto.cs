using System.ComponentModel.DataAnnotations;

namespace GerenciadorEscolar.Data.Dtos.DtoSubject;

public class UpdateSubjectDto
{
    [Required(ErrorMessage = "Fild Title is required")]
    [StringLength(30, ErrorMessage = "Fild Title could only aceppt till 30 characters")]
    public string Title { get; set; }
    public DateTime? UpdatedAt { get; } = DateTime.Now;
}
