using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Enum;

namespace RedeSocial.Domain.DTOs
{
    public class ListarUsuarioDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public byte[]? FotoPerfil { get; set; }
        public string? Biografia { get; set; }
        public List<Publicacao>? Publicacoes { get; set; }
        public List<Amizade>? Amizades { get; set; }
        public VisibilidadeEnum Visibilidade { get; set; }
        public bool ReceberNotificacoes { get; set; }
        public PermissaoEnum Permissao { get; set; }
    }
}
