using Grpc.Core;
using Grpc.Net.Client;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Channels;
using Google.Protobuf;
using System;
using TransferGrpc.Protos;
using WebApplication1.DTOs;

namespace WebApplication1.Grpc
{

    public static class GrpcServices
    {
        public static async Task<Respuesta> MandarGrpc(MediaDTO mediaDTO)
        {
            Respuesta respuesta = new Respuesta();
            using var channel = GrpcChannel.ForAddress("http://localhost:9090");
            var client = new Media.MediaClient(channel);
            var mediaRequest = new MediaRequest();
            mediaRequest.DataB64 = mediaDTO.Base64ParteDeVideo;
            mediaRequest.Ruta = mediaDTO.RutaArchivoSalida;
            mediaRequest.FinalPart = mediaDTO.parteFinal;
            try
            {
                var response = await client.TransferMediaAsync(mediaRequest);
                if (mediaDTO.parteFinal)
                {
                    respuesta.Mensaje = response.RutaAws;
                }
                respuesta.Mensaje = response.StringResponse;
                respuesta.RespuestaBool = response.Response;
                return respuesta;
            }
            catch (RpcException ex)
            {
                respuesta.Mensaje = $"Error en la llamada gRPC: {ex.Status.Detail}";
                respuesta.RespuestaBool = false;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = $"Error general: {ex.Message}";
                respuesta.RespuestaBool = false;
                return respuesta;
            }
        }



    }
}
