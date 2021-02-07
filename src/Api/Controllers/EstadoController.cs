using Api.Core.Contracts.Facades;
using Api.Core.Facades;
using Api.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NSwag.Annotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/servicos/consulta/estado")]
    public class EstadoController : ControllerBase
    {
        private readonly ILogger<EstadoController> _logger;
        readonly IEstadoFacade _estadoFacade;

        public EstadoController(ILogger<EstadoController> logger, IEstadoFacade estadoFacade)
        {
            _logger = logger;
            _estadoFacade = estadoFacade;
        }

        [HttpGet()]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(EstadoUF))]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.InternalServerError)]
        [OpenApiOperation(summary: "Estados", "Retorna um Endereço pelo CEP informado")]
        public async Task<IActionResult> Get()
        {
            var result = await _estadoFacade.ListarUFs();
            if (!result.Any())
                return BadRequest("Ocorreu um erro no processamento da solicitação");
            return Ok(result);
            //try
            //{
            //    _logger.LogInformation($"Consulta realizada para o cep {cep}");
            //    var endereco = await _cepFacade.Buscar(cep);

            //    if (endereco == null)
            //        return NotFound();

            //    return Ok(endereco);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Consulta ao Cpf: {cep} falhou com o seguinte erro: {ex}");
            //    return StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu um erro ao processar a solicitação");
            //}
        }
    }
}
