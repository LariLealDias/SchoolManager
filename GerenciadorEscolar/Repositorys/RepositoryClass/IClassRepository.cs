using GerenciadorEscolar.Data.Dtos.DtoClass;
using GerenciadorEscolar.Models;

namespace GerenciadorEscolar.Repository.RepositoryClass;

public interface IClassRepository
{
    void AddClassInDatabase(ClassModel classModel);

    IEnumerable<ClassModel> GetPagingInClassDatabase(int skip = 0, int take = 10);

    ClassModel FindClassByIdInDatabase(int id);

    void SaveChanges();
    void Remove(ClassModel idClass);


}
