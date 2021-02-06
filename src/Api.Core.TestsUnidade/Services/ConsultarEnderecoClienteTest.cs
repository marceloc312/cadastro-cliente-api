using Api.Core.Contracts.Repositorys;
using Api.Core.Contracts.Services;
using Api.Core.DTOs;
using Api.Core.Models;
using Api.Core.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Api.Core.TestsUnidade
{
        [Trait(TRAIT_NAME, TRAIT_VALUE)]
    public class ConsultarEnderecoClienteTest
    {
        private const string TRAIT_NAME = "Endereço Cliente";
        private const string TRAIT_VALUE = "Consulta Endereço do cliente";

        public ConsultarEnderecoClienteTest()
        {
        }

        [Theory(DisplayName = "Deve encontrar lista de endereço do cliente pelo ID do Cliente")]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public async void DeveRetornarEnderecoDoClientePeloIdCliente(long idCliente, int qtdEnderecosExistente)
        {
            // Arrange
            var enderecos = MockJsonValues.Enderecos();
            var enderecoClienteRepository = new Mock<IEnderecoClienteRepositoryReadOnly>();
            enderecoClienteRepository.Setup(x => x.BuscarPorIdClienteAsync(It.IsAny<long>())).ReturnsAsync((long id) => enderecos.Where(w => w.ClienteId == id));
            var logger = new Mock<ILogger<EnderecoClienteServiceReadOnly>>();

            IEnderecoClienteServiceReadonly enderecoClienteService = new EnderecoClienteServiceReadOnly(logger.Object, enderecoClienteRepository.Object);

            // Act
            IEnumerable<EnderecoCliente> enderecoCliente = await enderecoClienteService.BuscaEnderecosPorIdCliente(idCliente);

            // Assert
            Assert.Equal(qtdEnderecosExistente, enderecoCliente.Count());
        }

        [Theory(DisplayName = "Falha ao conectar com o banco de dados. Não deve retornar endereços do cliente e deve logar erro")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void NaoDeveRetonarClienteEDeveLogarErro(long idCliente)
        {
            // Arrange
            var enderecoClienteRepository = new Mock<IEnderecoClienteRepositoryReadOnly>();
            enderecoClienteRepository.Setup(x => x.BuscarPorIdClienteAsync(It.IsAny<long>())).ThrowsAsync(new Exception("Exceção de banco de dados"));
            var logger = new Mock<ILogger<EnderecoClienteServiceReadOnly>>();

            IEnderecoClienteServiceReadonly enderecoClienteService = new EnderecoClienteServiceReadOnly(logger.Object, enderecoClienteRepository.Object);

            // Act
            IEnumerable<EnderecoCliente> enderecosCliente = await enderecoClienteService.BuscaEnderecosPorIdCliente(idCliente);

            // Assert
            Assert.Empty(enderecosCliente);
            logger._AssertLog(LogLevel.Information, $"Consultando endereços do cliente {idCliente}", 1);
            logger._AssertLog(LogLevel.Information, $"Consulta executada com sucesso", 0);
            logger._AssertLog(LogLevel.Error, $"Erro ao consultar endereços do cliente {idCliente}", 1);
            logger._AssertLog(LogLevel.Error, "Exceção de banco de dados", 1);
        }
    }
}
