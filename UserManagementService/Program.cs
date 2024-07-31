using Microsoft.EntityFrameworkCore;
using System;
using UserManagementService.Data;
using UserManagementService.Data.Repositories;
using UserManagementService.Profiles;
using UserManagementService.Services;

namespace UserManagementService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<UserManagementDbContext>(options =>
                   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddGrpc();
            builder.Services.AddGrpcReflection();
            builder.Services.AddScoped<IUserManagementService, UserManagementServiceImpl>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
         
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); 
            }, AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();



            app.MapGrpcService<UserManagementServiceImpl>();
            app.MapGrpcReflectionService();
            app.Run();
        }
    }
}
