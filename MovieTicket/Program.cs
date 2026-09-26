using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MovieTicket.Data;
using MovieTicket.Interface.Auth;
using MovieTicket.Repository;
using MovieTicket.Services;

var builder=WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options=>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")
        ));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters=new TokenValidationParameters
                    {
                        ValidateIssuer=true,
                        ValidateAudience=true,
                        ValidateLifetime=true,
                        ValidateIssuerSigningKey=true,

                        ValidIssuer=builder.Configuration["jwt:Issuer"],
                        ValidAudience=builder.Configuration["jwt:Audience"],

                        IssuerSigningKey=new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                        )
                        
                    };
                });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1",new OpenApiInfo{
        Title="Move Ticket Booking app",
        Version="v1",
        Description="Api for managing the movies,cinemas, showtimes,bookings"
});
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name="Authorization",
    Type=SecuritySchemeType.Http,
    Scheme="Bearer",
    BearerFormat="Jwt",
    In=ParameterLocation.Header,
    Description="Enter your JWT token"
});
});

builder.Services.AddScoped<IAuthRepository,  AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();


var app=builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json","Movie Ticket Booking Api v1");
        options.RoutePrefix="swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();