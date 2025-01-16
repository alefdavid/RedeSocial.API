namespace RedeSocial.Domain.DTOs
{
    public class LoginUsuarioDTO
    {
        public LoginUsuarioDTO(string email, string token)
        {
            Email = email;
            Token = token;
        }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
