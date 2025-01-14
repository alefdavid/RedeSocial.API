namespace RedeSocial.Domain.DTOs
{
    public class PublicacoesUsuarioDTO
    {
        public UsuarioDTO Usuario { get; set; }
        public List<PublicacaoDTO> Publicacoes { get; set; }
    }
}
