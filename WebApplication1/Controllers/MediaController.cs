using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Grpc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        [HttpPost("RecibirArchivo")]
        public async Task<ActionResult<Respuesta>> RecibirArchivo([FromBody] MediaDTO video)
        {
            Respuesta respuesta = new Respuesta();
            respuesta = await GrpcServices.MandarGrpc(video);
            return respuesta;
        }
    }
}
