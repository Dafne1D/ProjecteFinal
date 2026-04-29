using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ICategoryRepository
{
    List<Categoria> GetAll();
    Categoria? GetByNom(string nom);
    void Insert(Categoria categoria);
    bool Delete(string nom);
}