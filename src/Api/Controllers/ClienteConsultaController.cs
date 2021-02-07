using Api.Core.Contracts.Services;
using Api.Core.Models;
using Api.Core.Models.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteService _clienteService;

        public ClienteController(ILogger<ClienteController> logger, IClienteService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        [HttpGet("{cpf}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(Cliente))]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(string cpf)
        {
            try
            {
                _logger.LogInformation($"Consulta realizada para o cpf {cpf}");

                if (!new ClienteCpfValidoSpec().IsSatisfiedBy(new Cliente() { CPF = cpf }))
                    return BadRequest($"Cpf {cpf} informado é invalido para consulta");

                var cliente = await _clienteService.BuscaPorCPF(cpf);
                if (cliente == null)
                    return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Consulta ao Cpf: {cpf} falhou com o seguinte erro: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu um erro ao processar a solicitação");
            }
        }
    }
}
