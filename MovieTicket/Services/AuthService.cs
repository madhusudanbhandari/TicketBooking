using MovieTicket.Dtos;
using MovieTicket.Interface.Auth;
using MovieTicket.Model;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MovieTicket.Exceptions;

namespace MovieTicket.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IAuthRepository authRepository,IConfiguration configuration)
    {
        _authRepository=authRepository;
        _configuration=configuration;
    }
    public async Task<RegisterUserResponseDto> RegisterUserAsync(RegisterUserDto dto)
    {
        var user=new User
        {
            Name=dto.Name,
            Age=dto.Age,
            Gender=dto.Gender,
            Email=dto.Email,
            Password=BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role=dto.Role
        };

        var alreadyExisting=await _authRepository.GetUserAsync(dto.Email);

        if (alreadyExisting!=null)
        {
            throw new BadRequestException("User with this email already exist");
        }

        await _authRepository.AddUserAsync(user);
        await _authRepository.SaveChangesAsync();

        return new RegisterUserResponseDto
        {
            Id=user.Id,
            Name=user.Name,
            Age=user.Age,
            Email=user.Email,
            Gender=user.Gender,
            Role=user.Role
        };


    }

    public async Task<LoginUserResponseDto> LoginUserAsync(LoginUserDto dto)
    {
        var user= await _authRepository.GetUserAsync(dto.Email);

        if (user == null)
        {
            throw new NotFoundException("User with this email does not exist");
        }

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            throw new BadRequestException("Passwords did not match");
        }

        var token=GenerateToken(user);

        return new LoginUserResponseDto
        {
            Name=user.Name,
            Email=user.Email,
            Role=user.Role,
            Token=token,
        };
    }

    private string GenerateToken(User user)
    {
        var claims=new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key=new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!
            )
        );
        
        var credentials=new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token=new JwtSecurityToken(
            issuer:_configuration["Jwt:Issuer"],
            audience:_configuration["Jwt:Audience"],
            claims:claims,
            expires: DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<double>("Jwt:ExpirationMinutes")
            ),
            signingCredentials:credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
        
    }
}