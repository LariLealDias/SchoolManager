using System.ComponentModel.DataAnnotations;

namespace GerenciadorEscolar.Data.Dtos.DtoTeacher;

public class UpdateTeacherDto
{

    [Required(ErrorMessage = "Fild FirstName is required")]
    [StringLength(50, ErrorMessage = "Fild FistName could only aceppt till 50 characters")]
    public string FirstName { get; set; }


    [Required(ErrorMessage = "Fild LastName is required")]
    [StringLength(50, ErrorMessage = "Fild LastName could only aceppt till 50 characters")]
    public string LastName { get; set; }



    [Required(ErrorMessage = "Fild Email is required")]
    [EmailAddress(ErrorMessage = "Fild Email has an invalid email address")]
    public string Email { get; set; }


    [Required(ErrorMessage = "Fild PhoneNumber is required")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
    public long PhoneNumber { get; set; }


    [Required(ErrorMessage = "Fild BirthDate is required")]
    [DataType(DataType.Date, ErrorMessage = "Fild BirthDate has an invalid format. Follow the format:yyyy-MM-dd")]
    public DateTime BirthDate { get; set; }
    public DateTime? UpdatedAt { get; } = DateTime.Now;

}
