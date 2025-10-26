using Academy.Application.Dtos.AuthenticationDtos;

namespace Academy.Application.Services.Interfaces;

public interface IAuthService
{
    Task<JwtResponseModel> CreateToken(JwtRequestModel model);
}