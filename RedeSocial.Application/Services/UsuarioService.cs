using AutoMapper;
using RedeSocial.Domain;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace RedeSocial.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        public readonly IRepositoryManager _repositoryManager;

        public readonly IMapper _mapper;

        private bool disposed;

        public UsuarioService (IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<ListarUsuarioDTO> GetById(int id)
        {
            if (id == 0)
                throw new Exception();

            var usuario = await _repositoryManager.UsuarioRepository.GetUsuarioAtivoById(id);   

            if (usuario == null)
                throw new Exception("Usuário não encontrado ou inativo.");

            var usuarioDTO = _mapper.Map<ListarUsuarioDTO>(usuario);

            return usuarioDTO;
        }

        public async Task<List<ListarUsuarioDTO>> GetAll(string? nome)
        {
            var listaUsuario = await _repositoryManager.UsuarioRepository.GetUsuario(nome);
            var listaUsuarioDTO = _mapper.Map<List<ListarUsuarioDTO>>(listaUsuario);

            return listaUsuarioDTO;
        }


        public async Task<UsuarioDTO> Post(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                throw new ArgumentNullException(nameof(usuarioDTO));

            var usuarioExistente = await _repositoryManager.UsuarioRepository.GetUsuarioByEmail(usuarioDTO.Email);
            if (usuarioExistente != null)
                throw new Exception("E-mail já cadastrado.");

            var usuario = _mapper.Map<UsuarioDTO, Usuario>(usuarioDTO);

            usuario.DataNascimento = usuarioDTO.DataNascimento.Date;
            usuario.DataCriacao = DateTime.Now;
            usuario.FlAtivo = true;

            usuario.Senha = EncryptSenha(usuarioDTO.Senha);

            _repositoryManager.UsuarioRepository.Add(usuario);
            await _repositoryManager.Save();

            return _mapper.Map<Usuario, UsuarioDTO>(usuario);
        }

        public async Task<bool> Put(UsuarioDTO usuarioDTO, int id)
        {
            if (usuarioDTO == null)
                throw new ArgumentNullException(nameof(usuarioDTO));

            var usuarioExistente = await _repositoryManager.UsuarioRepository.GetUsuarioByEmail(usuarioDTO.Email);
            if (usuarioExistente != null && usuarioExistente.Id != id)
                throw new Exception("E-mail já cadastrado.");

            var usuarioExiste = await _repositoryManager.UsuarioRepository.GetUsuarioAtivoById(id);

            if (usuarioExiste == null)
                throw new ArgumentNullException($"Não existe: {usuarioExiste}.");

            usuarioDTO.Senha = EncryptSenha(usuarioExiste.Senha);

            _mapper.Map(usuarioDTO, usuarioExiste);

            usuarioExiste.DataNascimento = usuarioDTO.DataNascimento.Date;
            usuarioExiste.DataAlteracao = DateTime.Now;

            _repositoryManager.UsuarioRepository.Put(usuarioExiste);

            await _repositoryManager.Save();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            var retornoUsuario = await _repositoryManager.UsuarioRepository.GetUsuarioAtivoById(id);

            if (retornoUsuario == null)
                throw new ArgumentNullException($"Não existe: {id}.");

            retornoUsuario.FlAtivo = false;
            retornoUsuario.DataAlteracao = DateTime.Now;

            _repositoryManager.UsuarioRepository.Put(retornoUsuario);

            await _repositoryManager.Save();

            return true;
        }

        private string EncryptSenha(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
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

        ~UsuarioService()
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

