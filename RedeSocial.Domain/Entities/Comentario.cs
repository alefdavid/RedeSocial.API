namespace RedeSocial.Domain.Entities
{
    public class Comentario : Entity
    {
        public string Texto { get; set; }
        public int UsuarioId { get; set; }
        public int PublicacaoId { get; set; }
        public bool FlAtivo { get; set; }
        public Publicacao Publicacao { get; set; }
    }
}
