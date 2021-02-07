using Api.Core.Contracts.Services;
using Api.Core.DTOs;
using Api.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/cliente/{idCliente}/endereco")]
    public class EnderecoController : ControllerBase
    {
        private readonly ILogger<EnderecoController> _logger;
        private readonly IEnderecoClienteService _enderecoService;

        public EnderecoController(ILogger<EnderecoController> logger, IEnderecoClienteService enderecoService)
        {
            _logger = logger;
            _enderecoService = enderecoService;
        }

        [HttpGet()]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(EnderecoClienteDTO))]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int idCliente)
        {
            try
            {
                if (idCliente < 1)
                    return BadRequest("Dados informados inválidos");

                _logger.LogInformation($"Consulta realizada para o Cliente {idCliente}");

                var enderecos = await _enderecoService.BuscaEnderecosPorIdClienteAsync(idCliente);
                if (!enderecos.Any())
                    return NotFound();
                var result = enderecos.Select(endereco => new EnderecoClienteDTO(endereco));
                return Ok(enderecos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Consulta de endereços do Cliente {idCliente} falhou com o seguinte erro: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu um erro ao processar a solicitação");
            }
        }

        [HttpGet("{idEndereco}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(EnderecoClienteDTO))]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] long idCliente, int idEndereco)
        {
            try
            {
                if (idCliente < 1 || idEndereco < 1)
                    return BadRequest("Dados informados inválidos");

                _logger.LogInformation($"Consulta realizada para o Cliente {idCliente}");

                EnderecoCliente endereco = await _enderecoService.BuscaEnderecoPorIdAsync(idCliente,idEndereco);
                if (endereco == null)
                    return NotFound();
                var result = new EnderecoClienteDTO(endereco);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Consulta de endereços do Cliente {idCliente} falhou com o seguinte erro: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu um erro ao processar a solicitação");
            }
        }

        [HttpPut("{idEndereco}")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, Type = typeof(EnderecoClienteDTO))]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] long idCliente, int idEndereco)
        {
            try
            {
                if (idCliente < 1)
                    return BadRequest("Dados informados inválidos");

                _logger.LogInformation($"Consulta realizada para o Cliente {idCliente}");

                var enderecos = await _enderecoService.BuscaEnderecosPorIdClienteAsync(idCliente);
                if (!enderecos.Any())
                    return NotFound();
                var result = enderecos.Select(endereco => new EnderecoClienteDTO(endereco));
                return Ok(enderecos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Consulta de endereços do Cliente {idCliente} falhou com o seguinte erro: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu um erro ao processar a solicitação");
            }
        }
    }
}
