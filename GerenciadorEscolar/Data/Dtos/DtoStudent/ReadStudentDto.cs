using System.ComponentModel.DataAnnotations;

namespace GerenciadorEscolar.Data.Dtos.DtoStudent;

public class ReadStudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public float PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }

    public DateTime? DateTimeStamp { get; } = DateTime.Now;
}
