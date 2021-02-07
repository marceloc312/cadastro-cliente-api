using Api.Core.Contracts.Facades;
using Api.Core.DTOs;
using Api.Core.DTOs.ACL;
using Api.Core.Facades;
using Api.Core.ModelConfigs;
using Api.Core.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Api.Core.TestsUnidade
{
        [Trait("[Testes Unitários] Estados Facade", "Consulta Lista de Estados")]
    public partial class ConsultarEstadosTest
    {
           readonly IOptions<OrdenacaoEstados> optionsOrdenacao = Options.Create<OrdenacaoEstados>(new OrdenacaoEstados() { Rank = new[] { "SP", "RJ" }, Sentido = "Asc" });
        public ConsultarEstadosTest()
        {
        }

        [Fact(DisplayName = "Deve listar todos os estados")]
        public async void DeveRetornarTodosOsEstados()
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestEstadosServiceMock(HttpStatusCode.OK, MockJsonValues.Ufs());
            var logger = new Mock<ILogger<EstadoFacade>>();
            IEstadoFacade estadoFacade = new EstadoFacade(logger.Object, optionsOrdenacao,restMock.Object);

            // Act
            IEnumerable<EstadoUF> estados= await estadoFacade.ListarUFs();

            // Assert
            Assert.Equal(27,estados.Count());
            estados.ElementAt(0).Sigla.Should().Be("SP");
            estados.ElementAt(1).Sigla.Should().Be("RJ");
        }

        [Fact(DisplayName = "Resposta incorreta da Api de estados de terceiros. Não deve retornar lista de estados e deve logar")]
        public async void NaoDeveRetornarTodosOsEstados()
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestEstadosServiceMock(HttpStatusCode.BadRequest, "Erro ao retornar estados");
            var logger = new Mock<ILogger<EstadoFacade>>();
            IEstadoFacade estadoFacade = new EstadoFacade(logger.Object, optionsOrdenacao, restMock.Object);

            // Act
            IEnumerable<EstadoUF> estados = await estadoFacade.ListarUFs();

            // Assert
            Assert.Empty(estados);
            logger._AssertLog(LogLevel.Information, "Enviando requisição GET", 1);
            logger._AssertLog(LogLevel.Information, "Consulta executada com StatusCode", 1);
        }

        [Fact(DisplayName = "Falha na comunicação com a Api de terceiros")]
        public async void NaoDeveRetornarTodosOsEstadosEDeveLogar_HouveFalhaNaComunicacao()
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestEstadosServiceMock(HttpStatusCode.BadRequest, "Erro ao retornar estados",new Exception("Time out"));
            var logger = new Mock<ILogger<EstadoFacade>>();
            IEstadoFacade estadoFacade = new EstadoFacade(logger.Object, optionsOrdenacao, restMock.Object);

            // Act
            IEnumerable<EstadoUF> estados = await estadoFacade.ListarUFs();

            // Assert
            Assert.Empty(estados);
            logger._AssertLog(LogLevel.Information, "Enviando requisição GET", 1);
            logger._AssertLog(LogLevel.Information, "Consulta executada com StatusCode", 0);
            logger._AssertLog(LogLevel.Error, "Time out", 1);
        }
    }
}
