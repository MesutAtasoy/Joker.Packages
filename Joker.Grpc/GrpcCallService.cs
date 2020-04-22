using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Serilog;

namespace Joker.Grpc
{
    public static class GrpcCallService
    {
     public static async Task<TResponse> CallServiceAsync<TResponse>(string urlGrpc, Func<GrpcChannel, Task<TResponse>> func)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

            var channel = GrpcChannel.ForAddress(urlGrpc);

            Log.Information($"Sending gRPC request to {urlGrpc} , target Url {channel.Target}");

            try
            {
                return await func(channel);
            }
            catch (RpcException e)
            {
                Log.Error($"gRPC Calling Error via grpc: {e.Status} - {e.Message}");
                return default;
            }
            finally
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", false);
            }
        }

        public static async Task CallServiceAsync(string urlGrpc, Func<GrpcChannel, Task> func)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

            var channel = GrpcChannel.ForAddress(urlGrpc);

            Log.Information($"Sending gRPC request to {urlGrpc} , target Url {channel.Target}");

            try
            {
                await func(channel);
            }
            catch (RpcException e)
            {
                Log.Error($"gRPC Calling Error via grpc: {e.Status} - {e.Message}");
            }
            finally
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", false);
            }
        }
    }
}