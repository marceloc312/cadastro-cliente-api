using Api.Core.Contracts.Repositorys;
using Api.Core.Contracts.Services;
using Api.Core.DTOs;
using Api.Core.Models;
using Api.Core.Services;
using Api.Core.TestsUnidade.Config;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Core.TestsUnidade
{
    [Trait(TRAIT_NAME, TRAIT_VALUE)]
    public class ConsultarClienteTest
    {
        private const string TRAIT_NAME = "Cliente";
        private const string TRAIT_VALUE = "Consulta CPF";

        public ConsultarClienteTest()
        {
        }

        [Theory(DisplayName = "Deve encontrar cliente pelo CPF")]
        [InlineData("09878923467")]
        [InlineData("89897776")]
        public async void Deve_Retornar_ClientePorCPF(string cpf)
        {
            // Arrange
            string[] cpfsExistentes = { "09878923467", "89897776" };
            var clienteRepository = new Mock<IClienteRepository>();
            clienteRepository.Setup(x => x.BuscarPorCpfAsync(It.Is<string>(w => cpfsExistentes.Contains(w)))).ReturnsAsync((string cp) => new ClienteDTO() { Cpf = cp });
            var logger = new Mock<ILogger<ClienteService>>();
            var clienteService = new ClienteService(logger.Object, clienteRepository.Object);

            // Act
            Cliente cliente = await clienteService.BuscaPorCPF(cpf);

            // Assert
            Assert.NotNull(cliente);
            cliente.NoCpfme.Should().Be(cpf);
        }

        [Theory(DisplayName = "Falha ao conectar com o banco de dados. Não deve retornar cliente pelo CPF e deve logar erro")]
        [InlineData("88899977744")]
        public async void NaoDeveRetonarClienteEDeveLogarErro(string cpf)
        {
            // Arrange
            var clienteRepository = new Mock<IClienteRepository>();
            clienteRepository.Setup(x => x.BuscarPorCpfAsync(It.IsAny<string>())).ThrowsAsync(new Exception("Exceção de banco de dados"));
            var logger = new Mock<ILogger<ClienteService>>();
            var clienteService = new ClienteService(logger.Object, clienteRepository.Object);

            // Act
            Cliente cliente = await clienteService.BuscaPorCPF(cpf);

            // Assert
            Assert.Null(cliente);
            logger._AssertLog(LogLevel.Information, "Consultando no banco o cpf", 1);
            logger._AssertLog(LogLevel.Information, "Consulta executada com sucesso", 0);
            logger._AssertLog(LogLevel.Error, "Exceção de banco de dados", 1);

        }
    }
}
