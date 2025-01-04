namespace RedeSocial.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
