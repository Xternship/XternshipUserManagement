using ApiGateway.Settings;
using ApiGateway.Services;
using ApiGateway.Protos; // Import the namespace for your gRPC clients
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Mail;
using ApiGateway.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Bind EmailSettings from configuration
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();

if (emailSettings == null)
{
    throw new Exception("Email settings are not configured properly.");
}

// Configure FluentEmail with SMTP
builder.Services.AddFluentEmail(emailSettings.DefaultFromEmail)
    .AddSmtpSender(new SmtpClient(emailSettings.SMTPSettingHost)
    {
        Port = emailSettings.Port,
        Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.Password),
        EnableSsl = true
    })
    .AddRazorRenderer();

// Register gRPC clients
builder.Services.AddGrpcClient<UserServiceProto.UserServiceProtoClient>(options =>
{
    options.Address = new Uri(builder.Configuration["Microservices:UserManagementServiceUrl"]);
});

builder.Services.AddGrpcClient<EmailServiceProto.EmailServiceProtoClient>(options =>
{
    options.Address = new Uri(builder.Configuration["Microservices:EmailServiceUrl"]);
});

builder.Services.AddScoped<IUserEmailService, UserEmailService>();
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(MappingsProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapUserEmailApi();
app.MapGrpcService<UserEmailService>();

app.Run();
