﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciadorEscolar.Models;

public class ClassModel
{
  
    [Key]
    //[Required]
    public int Id { get; set; }


    [Required(ErrorMessage = "Fild Grade is required")]
    [Range(1,9, ErrorMessage = "Fild Grade must be between 1 and 9")]
    [StringLength(1, ErrorMessage = "Fild Section could only aceppt 1 character")]
    //Serie / ano
    public string Grade { get; set; }



    [Required(ErrorMessage = "Fild Section is required")]
    [RegularExpression("^[a-zA-Z]$", ErrorMessage = "Fild Section must has only one alphabetical character.")]
    [StringLength(1, ErrorMessage = "Fild Section could only aceppt 1 character")]
    //A,B,C etc
    public string Section { get; set; }



    [Required(ErrorMessage = "Fild Shift is required")]
    [StringLength(30, ErrorMessage = "Fild Shift could only aceppt 30 characters")]
    //turno/ periodo
    public string Shift { get; set; }




    //Acessando o relacionamento 1 Student para 1 Class
    public virtual StudentModel StudentModel { get; set; }





}
