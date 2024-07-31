using Grpc.AspNetCore.Server;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Extensions
{
    public static class GrpcServerBuilderExtensions
    {
        public static IGrpcServerBuilder AddCustomJsonTranscoding(this IGrpcServerBuilder builder)
        {
            builder.Services.AddGrpc().AddJsonTranscoding();
            return builder;
        }
    }
}