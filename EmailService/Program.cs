using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmailService.Services;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        
        var emailSettings = context.Configuration.GetSection("EmailSettings");

        var senderEmail = emailSettings["DefaultFromEmail"] ?? "innovateeight8@gmail.com";
        var smtpServer = emailSettings["SMTPSetting:Host"] ?? "smtp.gmail.com";
        var portString = emailSettings["Port"] ?? "587";  
        var username = emailSettings["UserName"] ?? "innovateeight8@gmail.com";
        var password = emailSettings["Password"] ?? "lthn potu dozv xweh";

      
        int port = int.TryParse(portString, out int parsedPort) ? parsedPort : 587;

      
        services.AddFluentEmail(senderEmail)
            .AddSmtpSender(new SmtpClient(smtpServer)
            {
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(username, password)
            })
            .AddRazorRenderer(); 

        services.AddScoped<IEmailService, EmailServiceImpl>();

      
        services.AddGrpc().AddJsonTranscoding();

    
        services.AddLogging(configure => configure.AddConsole());
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure((context, app) =>
        {
            if (context.HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<EmailServiceImpl>();
            });
        });
    });

var app = builder.Build();
await app.RunAsync();
