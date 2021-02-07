using Api.Core.Contracts.Facades;
using Api.Core.Contracts.Services;
using Api.Core.Models;
using Api.Core.Models.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/servicos/consulta/")]
    public class CepController : ControllerBase
    {
        private readonly ILogger<CepController> _logger;
        private readonly ICepFacade _cepFacade;

        public CepController(ILogger<CepController> logger, ICepFacade cepFacade)
        {
            _logger = logger;
            _cepFacade = cepFacade;
        }

        [HttpGet("cep/{cep}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(EnderecoCep))]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(string cep)
        {
            try
            {
                _logger.LogInformation($"Consulta realizada para o cep {cep}");
                var endereco = await _cepFacade.Buscar(cep);

                if (endereco == null)
                    return NotFound();

                return Ok(endereco);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Consulta ao Cpf: {cep} falhou com o seguinte erro: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu um erro ao processar a solicitação");
            }
        }
    }
}
