using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace GerenciadorEscolar.Models;

public class StudentModel
{
    [Key]
    //[Required]
    public int Id { get; set; }

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


    ////Relacionando 1 Student para 1 Class
    //public int ClassModelId { get; set; }
    //public virtual ClassModel ClassModel { get; set; }

    //public virtual ICollection<ClassModel> ClassModel { get; set; }
    //public int ClassId { get; set; }
    //public ClassModel ClassModel { get; set; }


}
