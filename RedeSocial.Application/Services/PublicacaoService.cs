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

        //Comentario e Curtida

        public async Task<ComentarioDTO> GetComentarioById(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido");

            var comentario = await _repositoryManager.ComentarioRepository.GetComentarioById(id);

            if (comentario == null)
            {
                throw new Exception("Comentário não encontrado");
            }

            var comentarioDTO = _mapper.Map<ComentarioDTO>(comentario);

            return comentarioDTO;
        }

        public async Task<ComentarioDTO> PostComentario(ComentarioDTO comentarioDTO)
        {
            if (comentarioDTO == null)
            {
                throw new ArgumentException(nameof(comentarioDTO));
            }

            var comentario = _mapper.Map<Comentario>(comentarioDTO);

            comentario.DataCriacao = DateTime.Now;
            comentario.FlAtivo = true;

            _repositoryManager.ComentarioRepository.Add(comentario);
            await _repositoryManager.Save();

            return _mapper.Map<ComentarioDTO>(comentario);
        }

        public async Task<ComentarioDTO> PutComentario(ComentarioDTO comentarioDTO, int id)
        {
            if (comentarioDTO == null)
            {
                throw new ArgumentException(nameof(comentarioDTO));
            }

            var comentarioExistente = await _repositoryManager.ComentarioRepository.GetComentarioById(id);

            if (comentarioExistente == null)
            {
                throw new Exception("Comentário não encontrado");
            }

            _mapper.Map(comentarioDTO, comentarioExistente);
            comentarioExistente.DataAlteracao = DateTime.Now;

            _repositoryManager.ComentarioRepository.Put(comentarioExistente);
            await _repositoryManager.Save();

            return _mapper.Map<ComentarioDTO>(comentarioExistente);
        }

        public async Task<bool> DeleteComentario(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido");
            }

            var comentario = await _repositoryManager.ComentarioRepository.GetComentarioById(id);

            if (comentario == null)
            {
                throw new Exception("Comentário não encontrado");
            }

            comentario.FlAtivo = false;
            comentario.DataAlteracao = DateTime.Now;

            _repositoryManager.ComentarioRepository.Put(comentario);
            await _repositoryManager.Save();

            return true;
        }


        public async Task<CurtidaDTO> GetCurtidaById(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido");

            var curtida = await _repositoryManager.CurtidaRepository.GetCurtidaById(id);

            if (curtida == null)
            {
                throw new Exception("Curtida não encontrada");
            }

            var curtidaDTO = _mapper.Map<CurtidaDTO>(curtida);

            return curtidaDTO;
        }
                
        public async Task<CurtidasPublicacaoDTO> GetCurtidasByPublicacao(int publicacaoId)
        {
            if (publicacaoId <= 0)
            {
                throw new ArgumentException("ID da publicação inválido");
            }

            var curtidas = await _repositoryManager.CurtidaRepository.GetCurtidasByPublicacao(publicacaoId);

            if (curtidas == null)
            {
                throw new Exception("Publicação não encontrada ou sem curtidas");
            }

            return new CurtidasPublicacaoDTO
            {
                Publicacao = _mapper.Map<PublicacaoDTO>(curtidas.PublicacaoId),
                Curtidas = _mapper.Map<List<CurtidaDTO>>(curtidas)
            };
        }

        public async Task<CurtidaDTO> PostCurtida(CurtidaDTO curtidaDTO)
        {
            if (curtidaDTO == null)
            {
                throw new ArgumentException(nameof(curtidaDTO));
            }

            var curtida = _mapper.Map<Publicacao>(curtidaDTO);

            curtida.DataCriacao = DateTime.Now;
            curtida.FlAtivo = true;

            _repositoryManager.PublicacaoRepository.Add(curtida);
            await _repositoryManager.Save();

            return _mapper.Map<CurtidaDTO>(curtida);
        }

        public Task<bool> DeleteCurtida(int id)
        {
            throw new NotImplementedException();
        }


    }
}
