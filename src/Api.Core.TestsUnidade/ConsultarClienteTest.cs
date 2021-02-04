using Api.Core.Contracts.Repositorys;
using Api.Core.Contracts.Services;
using Api.Core.DTOs;
using Api.Core.Models;
using Api.Core.Services;
using Api.Core.TestsUnidade.Config;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Core.TestsUnidade
{
    public class ConsultarClienteTest 
    {
        readonly IClienteService _clienteService;

        public ConsultarClienteTest()
        {
            string[] cpfsExistentes = { "09878923467", "89897776" };
            var clienteRepository = new Mock<IClienteRepository>();
            clienteRepository.Setup(x => x.FindAsync(It.Is<string>(w => cpfsExistentes.Contains(w)))).ReturnsAsync(new ClienteDTO());
            _clienteService = new ClienteService(clienteRepository.Object);
        }

        [Trait("Cliente", "Consulta CPF")]
        [Theory(DisplayName = "Deve encontrar cliente pelo CPF")]
        [InlineData("09878923467")]
        [InlineData("89897776")]
        public async void Deve_Retornar_ClientePorCPF(string cpf)
        {
            // Arrange

            // Act
            Cliente cliente = await _clienteService.BuscaPorCPF(cpf);

            // Assert
            Assert.NotNull(cliente);
        }


        [Trait("Cliente", "Consulta CPF")]
        [Theory(DisplayName = "Não deve retornar cliente pelo CPF")]
        [InlineData("88899977744")]
        public async void NaoDeveRetonarCliente(string cpf)
        {
            // Arrange

            // Act
            Cliente cliente = await _clienteService.BuscaPorCPF(cpf);

            // Assert
            Assert.Null(cliente);
        }


    }
}
