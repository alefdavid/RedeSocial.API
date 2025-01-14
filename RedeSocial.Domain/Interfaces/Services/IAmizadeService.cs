using RedeSocial.Domain.DTOs;

namespace RedeSocial.Domain.Interfaces.Services
{
    public interface IAmizadeService : IService
    {
        Task<AmizadeDTO> Post(AmizadeDTO amizadeDTO);
        Task<AmizadeDTO> GetById(int id);
        Task<List<AmizadeDTO>> GetAll();
        Task<bool> Delete(int id);
    }
}
