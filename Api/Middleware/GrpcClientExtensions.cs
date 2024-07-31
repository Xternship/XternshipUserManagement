using ApiGateway.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApiGateway.Middleware
{
    public static class GrpcClientExtensions
    {
        public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<UserServiceProto.UserServiceProtoClient>(c =>
            {
                c.Address = new Uri(configuration.GetValue<string>("Microservices:UserManagementServiceUrl"));
            });
            services.AddGrpcClient<EmailServiceProto.EmailServiceProtoClient>(c =>
            {
                c.Address = new Uri(configuration.GetValue<string>("Microservices:EmailServiceUrl"));
            });

            return services;
        }

    }
}
