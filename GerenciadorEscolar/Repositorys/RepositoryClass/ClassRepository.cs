using AutoMapper;
using GerenciadorEscolar.Data.Dtos.DtoClass;
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

    //Reusable methods
    public void SaveChanges()
    {
        _contex.SaveChanges();
    }

    public void Remove(ClassModel idClass)
    {
        _contex.Remove(idClass);
    }

    public ClassModel FindClassById(int id)
    {
        var findClassById = _contex.Classes.FirstOrDefault(x => x.Id == id);
        return findClassById;
    }

    //Specific methods for Verbs HTTP
    ////POST
    public void AddClass(ClassModel classModel)
    {
        _contex.Classes.Add(classModel);
        SaveChanges();
    }

    ////GET
    public IEnumerable<ClassModel> GetPagingToClass(int skip = 0, int take = 10)
    {
        return _contex.Classes.Skip(skip).Take(take);
    }
}
