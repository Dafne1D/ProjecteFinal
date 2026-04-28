using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ICategoriaRepository
{
    List<Categoria> GetAll();
    Categoria? GetByNom(string nom);
    void Insert(Categoria categoria);
    void Update(Categoria categoria);
    bool Delete(string nom);
}