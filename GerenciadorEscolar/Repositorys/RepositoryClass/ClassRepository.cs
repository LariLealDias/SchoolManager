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
    public void AddClassInDatabase(ClassModel classModel)
    {
        _contex.Classes.Add(classModel);
        _contex.SaveChanges();
    }

    public IEnumerable<ClassModel> GetPagingInClassDatabase(int skip = 0, int take = 10)
    {
        return _contex.Classes.Skip(skip).Take(take);
    }
}
