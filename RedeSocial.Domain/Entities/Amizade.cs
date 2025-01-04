namespace RedeSocial.Domain.Entities
{
    public class Amizade : Entity
    {
        public int UsuarioId { get; set; }
        public int AmigoId { get; set; }
        public bool FlAtivo { get; set; }
    }
}
