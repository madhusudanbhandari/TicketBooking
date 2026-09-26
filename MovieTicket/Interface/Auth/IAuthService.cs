using MovieTicket.Dtos;

namespace MovieTicket.Interface.Auth;

public interface IAuthService
{
    Task<RegisterUserResponseDto> RegisterUserAsync(RegisterUserDto dto);
    Task<LoginUserResponseDto> LoginUserAsync(LoginUserDto dto);
}