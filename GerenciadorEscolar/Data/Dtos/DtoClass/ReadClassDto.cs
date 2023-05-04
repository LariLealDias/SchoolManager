﻿namespace GerenciadorEscolar.Data.Dtos.DtoClass;

public class ReadClassDto
{
    public string Grade { get; set; }
    public string Section { get; set; }
    public string Shift { get; set; }
    public DateTime? DateTimeStamp { get; } = DateTime.Now;
}
