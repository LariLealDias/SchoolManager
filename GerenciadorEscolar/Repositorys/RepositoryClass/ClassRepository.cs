using AutoMapper;
using GerenciadorEscolar.Models;
using SchoolManager.Data;

namespace GerenciadorEscolar.Repository.RepositoryClass;

public class ClassRepository : IClassRepository
{
    //Dependence Injection
    private SchoolManagerContext _contex;
    public ClassRepository(SchoolManagerContext contex)
    {
        _contex = contex;
    }
    public void AddClassInDatabase(ClassModel classModel)
    {
        _contex.Classes.Add(classModel);
        _contex.SaveChanges();
    }
}
