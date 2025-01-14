using AutoMapper;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces.Repositories;
using RedeSocial.Domain.Interfaces.Services;

namespace RedeSocial.Application.Services
{
    public class AmizadeService : IAmizadeService
    {
        public readonly IAmizadeRepository _repository;

        public readonly IMapper _mapper;

        private bool disposed;

        public AmizadeService(IAmizadeRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AmizadeDTO> GetById(int id)
        {
            var amizade = await _repository.GetById(id);
            return _mapper.Map<Amizade, AmizadeDTO>(amizade);
        }

        public async Task<List<AmizadeDTO>> GetAll()
        {
            var listAmizades = await _repository.GetAll();
            var listAmizadesDTO = _mapper.Map<List<AmizadeDTO>>(listAmizades);

            return listAmizadesDTO;
        }

        public async Task<AmizadeDTO> Post(AmizadeDTO amizadeDTO)
        {
            if (amizadeDTO == null)
                throw new ArgumentNullException(nameof(amizadeDTO));

            var amizade = _mapper.Map<AmizadeDTO, Amizade>(amizadeDTO);

            _repository.Cadastrar(amizade);

            return _mapper.Map<Amizade, AmizadeDTO>(amizade);
        }

        public async Task<bool> Delete(int id)
        {
            var amizade = await _repository.GetById(id);
            if (amizade == null)
                throw new ArgumentNullException(nameof(amizade));

            await _repository.Delete(id);

            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    _repository.Dispose();

                disposed = true;
            }
        }

        ~AmizadeService()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
