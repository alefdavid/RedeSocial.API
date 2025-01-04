namespace RedeSocial.Domain.Entities
{
    public class Notificacao : Entity
    {
        public int UsuarioId { get; set; }
        public string Mensagem { get; set; }
        public bool Lido { get; set; }
    }
}
