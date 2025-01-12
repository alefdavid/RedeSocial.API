using AutoMapper;
using RedeSocial.Domain;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces.Services;

namespace RedeSocial.Application.Services
{
    public class PublicacaoService : IPublicacaoService
    {
        public readonly IRepositoryManager _repositoryManager;
        public readonly IMapper _mapper;
        private bool disposed;

        public PublicacaoService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;            
        }

        public async Task<PublicacaoDTO> GetById(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido");

            var publicacao = await _repositoryManager.PublicacaoRepository.GetPublicacaoById(id);

            if (publicacao == null)
            {
                throw new Exception("Publicacao não encontrada");
            }

            var publicacaoDTO = _mapper.Map<PublicacaoDTO>(publicacao);

            return publicacaoDTO;
        }

        public async Task<PublicacoesUsuarioDTO> GetPublicacoesByUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentException("ID do usuário inválido");
            }

            var publicacoes = await _repositoryManager.PublicacaoRepository.GetPublicacoesByUsuario(usuarioId);

            if (publicacoes == null)
            {
                throw new Exception("Usuário não encontrado ou sem publicações");
            }

            return new PublicacoesUsuarioDTO
            {
                Usuario = _mapper.Map<UsuarioDTO>(publicacoes.UsuarioId),
                Publicacoes = _mapper.Map<List<PublicacaoDTO>>(publicacoes)
            };
        }

        public async Task<PublicacaoDTO> Post(PublicacaoDTO publicacaoDTO)
        {
            if (publicacaoDTO == null)
            {
                throw new ArgumentException(nameof(publicacaoDTO));
            }

            var publicacao = _mapper.Map<Publicacao>(publicacaoDTO);

            publicacao.DataCriacao = DateTime.Now;
            publicacao.FlAtivo = true;

            _repositoryManager.PublicacaoRepository.Add(publicacao);
            await _repositoryManager.Save();

            return _mapper.Map<PublicacaoDTO>(publicacao);
        }

        public async Task<bool> Put(PublicacaoDTO publicacaoDTO, int id)
        {
            if (publicacaoDTO == null)
            {
                throw new ArgumentException(nameof (publicacaoDTO));                
            }

            var publicacaoExistente = await _repositoryManager.PublicacaoRepository.GetPublicacaoById(id);

            if (publicacaoExistente == null)
            {
                throw new Exception("Publicação não encontrada");
            }

            _mapper.Map(publicacaoDTO, publicacaoExistente);
            publicacaoExistente.DataAlteracao = DateTime.Now;

            _repositoryManager.PublicacaoRepository.Put(publicacaoExistente);
            await _repositoryManager.Save();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido");
            }

            var publicacao = await _repositoryManager.PublicacaoRepository.GetPublicacaoById(id);

            if (publicacao == null)
            {
                throw new Exception("Publicação não encontrada");
            }

            publicacao.FlAtivo = false;
            publicacao.DataAlteracao = DateTime.Now;

            _repositoryManager.PublicacaoRepository.Put(publicacao);
            await _repositoryManager.Save();

            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _repositoryManager.Dispose();
                }

                disposed = true;
            }
        }

        ~PublicacaoService()
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
