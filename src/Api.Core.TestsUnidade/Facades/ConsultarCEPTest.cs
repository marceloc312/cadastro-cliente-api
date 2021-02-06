using Api.Core.Contracts.Facades;
using Api.Core.Contracts.Repositorys;
using Api.Core.Contracts.Services;
using Api.Core.DTOs;
using Api.Core.Facades;
using Api.Core.ModelConfigs;
using Api.Core.Models;
using Api.Core.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Api.Core.TestsUnidade
{
    public partial class ConsultarCEPTest
    {
        public ConsultarCEPTest()
        {
        }

        [Trait("CEP Facade", "Consulta CEP")]
        [Theory(DisplayName = "Deve retornar o CEP")]
        [InlineData("01001000")]
        [InlineData("01001-000")]
        public async void DeveRetornarEnderecoPorCEP(string cep)
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestClientCEPServiceMock(HttpStatusCode.OK, MockJsonValues.Cep());
            var logger = new Mock<ILogger<CepFacade>>();
            CepFacade cepFacade = new CepFacade(restMock.Object, logger.Object);

            // Act
            CepDTO cepDTO = await cepFacade.Buscar(cep);

            // Assert
            Assert.NotNull(cepDTO);
            cepDTO.logradouro.Should().Be("Praça da Sé");
        }

        [Trait("CEP Facade", "Consulta CEP")]
        [Theory(DisplayName = "CEP inválido. Não deve retornar o CEP")]
        [InlineData("88899977744")]
        [InlineData("89jjhhhh")]
        public async void NaoDeveRetonarEnderecoPorCep(string cep)
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestClientCEPServiceMock(HttpStatusCode.OK, null);
            var logger = new Mock<ILogger<CepFacade>>();
            CepFacade cepFacade = new CepFacade(restMock.Object, logger.Object);

            // Act
            CepDTO cepDTO = await cepFacade.Buscar(cep);

            // Assert
            Assert.Null(cepDTO);
            logger._AssertLog(LogLevel.Information, "Enviando requisição GET", 0);
            logger._AssertLog(LogLevel.Error, "inválido", 1);
        }

        [Trait("CEP Facade", "Consulta CEP")]
        [Theory(DisplayName = "Falha ao consultar o CEP na Api de terceiros StatusCode = InternalServerError. Não deve retornar o CEP e deve logar os erros")]
        [InlineData("01001000")]
        [InlineData("01001-000")]
        public async void NaoDeveRetonarEnderecoPorCepEDeveLogar(string cep)
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestClientCEPServiceMock(HttpStatusCode.InternalServerError, "Erro interno de servidor");
            var logger = new Mock<ILogger<CepFacade>>();
            CepFacade cepFacade = new CepFacade(restMock.Object, logger.Object);

            // Act
            CepDTO cepDTO = await cepFacade.Buscar(cep);

            // Assert
            Assert.Null(cepDTO);
            logger._AssertLog(LogLevel.Information, "Enviando requisição GET", 1);
            logger._AssertLog(LogLevel.Information, "Consulta executada com StatusCode", 1);
        }

        [Trait("CEP Facade", "Consulta CEP")]
        [Theory(DisplayName = "Falha ao consultar o CEP na Api de terceiros Exception lançada/ falha de comunicação. Não deve retornar o CEP e deve logar os erros")]
        [InlineData("01001000")]
        [InlineData("01001-000")]
        public async void NaoDeveRetonarEnderecoPorCepEDeveLogar_HouveFalhaNaComunicacao(string cep)
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestClientCEPServiceMock(HttpStatusCode.InternalServerError, null, new Exception("Time out"));
            var logger = new Mock<ILogger<CepFacade>>();
            CepFacade cepFacade = new CepFacade(restMock.Object, logger.Object);

            // Act
            CepDTO cepDTO = await cepFacade.Buscar(cep);

            // Assert
            Assert.Null(cepDTO);
            logger._AssertLog(LogLevel.Information, "Enviando requisição GET", 1);
            logger._AssertLog(LogLevel.Information, "Consulta executada com StatusCode", 0);
            logger._AssertLog(LogLevel.Error, "Erro ao consultar o CEP", 1);
            logger._AssertLog(LogLevel.Error, "Time out", 1);
        }

    }
}
