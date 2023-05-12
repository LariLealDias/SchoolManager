using GerenciadorEscolar.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace SchoolManager.Data;

public class SchoolManagerContext : DbContext
{
    public SchoolManagerContext(DbContextOptions<SchoolManagerContext> opts) : base(opts)
    {

    }

    //Config n:n relating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Config key (x,y)
        modelBuilder.Entity<ClassScheduleModel>()
             .HasKey(classScheduleModel => new {
                                         classScheduleModel.TeacherModelId,
                                         classScheduleModel.ClassModelId
                                                 });
        modelBuilder.Entity<ClassScheduleModel>()
           .HasOne(classScheduleModel => classScheduleModel.ClassModel)
           .WithMany(classModel => classModel.ClassesSchedules)
           .HasForeignKey(classScheduleModel => classScheduleModel.ClassModelId);
        modelBuilder.Entity<ClassScheduleModel>()
          .HasOne(classScheduleModel => classScheduleModel.TeacherModel)
          .WithMany(teacherModel => teacherModel.ClassesSchedules)
          .HasForeignKey(classScheduleModel => classScheduleModel.TeacherModelId);
    }

    public DbSet<ClassModel> Classes { get; set; }
    public DbSet<StudentModel> Students { get; set; }
    public DbSet<SubjectModel> Subjects { get; set; }
    public DbSet<TeacherModel> Teachers { get; set; }

}
