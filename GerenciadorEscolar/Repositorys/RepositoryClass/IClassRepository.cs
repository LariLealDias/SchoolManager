using GerenciadorEscolar.Models;

namespace GerenciadorEscolar.Repository.RepositoryClass;

public interface IClassRepository
{
    void AddClassInDatabase(ClassModel classModel);

}
