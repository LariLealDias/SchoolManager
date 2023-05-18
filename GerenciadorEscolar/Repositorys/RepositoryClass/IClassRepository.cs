using GerenciadorEscolar.Data.Dtos.DtoClass;
using GerenciadorEscolar.Models;

namespace GerenciadorEscolar.Repository.RepositoryClass;

public interface IClassRepository
{
    void AddClass(ClassModel classModel);

    IEnumerable<ClassModel> GetPagingToClass(int skip = 0, int take = 10);

    ClassModel FindClassById(int id);

    void SaveChanges();
    void Remove(ClassModel idClass);
}
