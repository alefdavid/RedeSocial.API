using RedeSocial.Domain.Enum;

namespace RedeSocial.Domain.Entities
{
    public class Publicacao : Entity
    {
        public string Texto { get; set; }
        public VisibilidadeEnum Visibilidade { get; set; }
        public int UsuarioId { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<Curtida> Curtidas { get; set; }
        public bool FlAtivo { get; set; }
    }
}
