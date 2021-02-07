using Api.Core.Contracts.Repositorys;
using Api.Core.Contracts.Services;
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
    public class AlterarEnderecoClienteTest
    {
        private const string TRAIT_NAME = "[Testes Unitários] Endereço Cliente";
        private const string TRAIT_VALUE = "Alterar Endereço do cliente";

        public AlterarEnderecoClienteTest()
        {
        }

        [Theory(DisplayName = "Deve alterar o endereço do cliente com sucesso")]
        [InlineData(1, 1, "Rua 3", "456", "", "São Paulo", "SP", "Brasil", "00998-988")]
        public async void DeveAlterarOEnderecoDoClienteComSucesso(int id, long idCliente, string logradouro, string numero, string complemento, string cidade, string estado, string pais, string cep)
        {
            // Arrange
            var enderecoClienteRepository = new Mock<IEnderecoClienteRepository>();
            enderecoClienteRepository.Setup(x => x.AlterarAsync(It.IsAny<EnderecoCliente>()));
            var logger = new Mock<ILogger<EnderecoClienteService>>();
            IEnderecoClienteService enderecoClienteService = new EnderecoClienteService(logger.Object, enderecoClienteRepository.Object);

            EnderecoCliente enderecoCliente = new EnderecoCliente(id, idCliente, logradouro, numero, complemento, cidade, estado, pais, cep);

            // Act
            var validationResult = await enderecoClienteService.AlterarEndereco(enderecoCliente);

            // Assert
            Assert.Empty(validationResult.Errors);
            validationResult.IsValid.Should().Be(true);
            logger._AssertLog(LogLevel.Information, $"Alterando endereço {enderecoCliente.Id} do cliente {enderecoCliente.ClienteId}", 1);
            logger._AssertLog(LogLevel.Information, $"Endereço {enderecoCliente.Id} do cliente {enderecoCliente.ClienteId} alterado com sucesso", 1);
        }

        [Theory(DisplayName = "Não deve alterar, dados informados inválidos")]
        [InlineData(1, 1, "Rua 3", "456", "", null, "SP", "Brasil", "00998-988")]
        [InlineData(0, 1, "Rua 3", "456", "", null, "", "Brasil", "00998-988")]
        public async void NaoDeveAlterarDadosInformadosInvalidos(int id, long idCliente, string logradouro, string numero, string complemento, string cidade, string estado, string pais, string cep)
        {
            // Arrange
            var enderecoClienteRepository = new Mock<IEnderecoClienteRepository>();
            enderecoClienteRepository.Setup(x => x.AlterarAsync(It.IsAny<EnderecoCliente>()));
            var logger = new Mock<ILogger<EnderecoClienteService>>();

            IEnderecoClienteService enderecoClienteService = new EnderecoClienteService(logger.Object, enderecoClienteRepository.Object);
            EnderecoCliente enderecoCliente = new EnderecoCliente(id, idCliente, logradouro, numero, complemento, cidade, estado, pais, cep);

            // Act
            var validateResult = await enderecoClienteService.AlterarEndereco(enderecoCliente);

            // Assert
            Assert.NotEmpty(validateResult.Errors);
            logger._AssertLog(LogLevel.Information, $"Alterando endereço {enderecoCliente.Id} do cliente {enderecoCliente.ClienteId}", 0);
            logger._AssertLog(LogLevel.Information, $"Endereço {enderecoCliente.Id} do cliente {enderecoCliente.ClienteId} alterado com sucesso", 0);
        }

        [Theory(DisplayName = "Não deve alterar, falha na transação com o banco de dados")]
        [InlineData(1, 1, "Rua 3", "456", "", "São Paulo", "SP", "Brasil", "00998-988")]
        public async void FalhaNaTransacaoComOBanco(int id, long idCliente, string logradouro, string numero, string complemento, string cidade, string estado, string pais, string cep)
        {
            // Arrange
            var enderecoClienteRepository = new Mock<IEnderecoClienteRepository>();
            enderecoClienteRepository.Setup(x => x.AlterarAsync(It.IsAny<EnderecoCliente>())).ThrowsAsync(new Exception("Exceção de banco de dados"));
            var logger = new Mock<ILogger<EnderecoClienteService>>();

            IEnderecoClienteService enderecoClienteService = new EnderecoClienteService(logger.Object, enderecoClienteRepository.Object);
            EnderecoCliente enderecoCliente = new EnderecoCliente(id, idCliente, logradouro, numero, complemento, cidade, estado, pais, cep);

            // Act
            var validateResult = await enderecoClienteService.AlterarEndereco(enderecoCliente);

            // Assert
            Assert.NotEmpty(validateResult.Errors);
            logger._AssertLog(LogLevel.Information, $"Alterando endereço {enderecoCliente.Id} do cliente {enderecoCliente.ClienteId}", 1);
            logger._AssertLog(LogLevel.Information, $"Endereço {enderecoCliente.Id} do cliente {enderecoCliente.ClienteId} alterado com sucesso", 0);
            logger._AssertLog(LogLevel.Error, $"Erro ao alterar o endereço {enderecoCliente.Id} do cliente {enderecoCliente.ClienteId}", 1);
            logger._AssertLog(LogLevel.Error, "Exceção de banco de dados", 1);
        }
    }
}
