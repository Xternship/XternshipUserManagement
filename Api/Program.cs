using ApiGateway.Middleware;
using ApiGateway.Services;
using ApiGateway.Settings;
using ApiGateway.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc().AddJsonTranscoding();


builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddAutoMapper(typeof(MappingsProfile));
builder.Services.AddGrpcClients(builder.Configuration);
builder.Services.AddScoped<IUserEmailService, UserEmailService>();
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
