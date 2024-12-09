using MembukuAPI.Authors;
using MembukuAPI.Auths;
using MembukuAPI.Books;
using MembukuAPI.Data;
using MembukuAPI.HighlightedBooks;
using MembukuAPI.Images;
using MembukuAPI.Middlewares;
using MembukuAPI.Reviews;
using MembukuAPI.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;

namespace MembukuAPI;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<MembukuContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Register the repository and service for DI
        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        builder.Services.AddScoped<IAuthorService, AuthorService>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IHighlightedBookRepository, HighlightedBookRepository>();
        builder.Services.AddScoped<IHighlightedBookService, HighlightedBookService>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        builder.Services.AddScoped<IReviewService, ReviewService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IImageService, ImageService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        //Auto Mapper
        builder.Services.AddAutoMapper(
            typeof(AuthorProfile),
            typeof(BookProfile),
            typeof(HighlightedBookProfile),
            typeof(UserProfile),
            typeof(ReviewProfile));

        builder.Services.AddControllers().AddJsonOptions(x => {
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value)
            ),
                ValidateIssuer = false,
                ValidateAudience = false
            }
    );

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
                Description = "Standard Authorization header using the bearer scheme (bearer {token})",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        //CORS
        builder.Services.AddCors(options => {
            options.AddPolicy("AllowAngularApp",
                builder => {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });


        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        //ENABLE CORS
        app.UseCors("AllowAngularApp");

        app.Run();
    }
}
