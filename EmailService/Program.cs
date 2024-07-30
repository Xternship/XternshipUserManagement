using EmailService.Services;
using AutoMapper;

namespace EmailService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc();
            builder.Services.AddScoped<IEmailService, EmailServiceImpl>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

         
            var app = builder.Build();

            app.MapGrpcService<EmailServiceImpl>();
            app.Run();
        }
    }
}
