using GerenciadorEscolar.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace SchoolManager.Data;

public class SchoolManagerContext : DbContext
{
    public SchoolManagerContext(DbContextOptions<SchoolManagerContext> opts) : base(opts)
    {

    }
    public DbSet<ClassModel> Classes { get; set; }
    public DbSet<StudentModel> Students { get; set; }

}
