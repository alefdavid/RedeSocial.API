namespace RedeSocial.Domain.DTOs
{
    public class CurtidasPublicacaoDTO
    {
        public PublicacaoDTO Publicacao { get; set; }
        public List<CurtidaDTO> Curtidas { get; set; }
    }
}
