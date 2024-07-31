using EmailService.Services;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Mail;

namespace EmailService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure FluentEmail with SMTP
            builder.Services.AddFluentEmail(builder.Configuration["EmailSettings:DefaultFromEmail"])
                .AddSmtpSender(new SmtpClient(builder.Configuration["EmailSettings:SMTPSettingHost"])
                {
                    Port = builder.Configuration.GetValue<int>("EmailSettings:Port"),
                    Credentials = new NetworkCredential(builder.Configuration["EmailSettings:UserName"], builder.Configuration["EmailSettings:Password"]),
                    EnableSsl = true
                })

                .AddRazorRenderer();

            builder.Services.AddScoped<IEmailService, EmailServiceImpl>();

            builder.Services.AddGrpc();
            builder.Services.AddGrpcReflection();

            var app = builder.Build();

            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Registered Services:");
            foreach (var service in builder.Services)
            {
                logger.LogInformation($"Service: {service.ServiceType.FullName}");
            }

            app.MapGrpcService<EmailServiceImpl>();
            app.MapGrpcReflectionService();

            app.Run();
        }
    }
}
