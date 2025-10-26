namespace Academy.Application.Dtos.AuthenticationDtos
{
    public class JwtRequestModel
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public List<string> Roles { get; set; } = [];
    }
}
