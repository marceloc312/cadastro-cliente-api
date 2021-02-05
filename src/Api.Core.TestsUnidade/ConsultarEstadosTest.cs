using Api.Core.Contracts.Facades;
using Api.Core.DTOs;
using Api.Core.Facades;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Api.Core.TestsUnidade
{
    public partial class ConsultarEstadosTest
    {
        public ConsultarEstadosTest()
        {
        }

        [Trait("Estados Facade", "Consulta Lista de Estados")]
        [Fact(DisplayName = "Deve listar todos os estados")]
        public async void DeveRetornarTodosOsEstados()
        {
            // Arrange
            var restMock = TestHelper.CreateRestEstadosServiceMock(HttpStatusCode.OK, ConstantsJSON.JSON_ESTADOS);
            var logger = new Mock<ILogger<EstadosFacade>>();
            IEstadosFacade estadoFacade = new EstadosFacade(logger.Object,restMock.Object);

            // Act
            IEnumerable<EstadoDTO> estados= await estadoFacade.Buscar();

            // Assert
            Assert.Equal(27,estados.Count());
        }

        [Trait("Estados Facade", "Consulta Lista de Estados")]
        [Fact(DisplayName = "Resposta incorreta da Api de estados de terceiros. Não deve retornar lista de estados e deve logar")]
        public async void NaoDeveRetornarTodosOsEstados()
        {
            // Arrange
            var restMock = TestHelper.CreateRestEstadosServiceMock(HttpStatusCode.BadRequest, "Erro ao retornar estados");
            var logger = new Mock<ILogger<EstadosFacade>>();
            IEstadosFacade estadoFacade = new EstadosFacade(logger.Object, restMock.Object);

            // Act
            IEnumerable<EstadoDTO> estados = await estadoFacade.Buscar();

            // Assert
            Assert.Empty(estados);
            logger._AssertLog(LogLevel.Information, "Enviando requisição GET", 1);
            logger._AssertLog(LogLevel.Information, "Consulta executada com StatusCode", 1);
        }

        [Trait("Estados Facade", "Consulta Lista de Estados")]
        [Fact(DisplayName = "Falha na comunicação com a Api de terceiros")]
        public async void NaoDeveRetornarTodosOsEstadosEDeveLogar_HouveFalhaNaComunicacao()
        {
            // Arrange
            var restMock = TestHelper.CreateRestEstadosServiceMock(HttpStatusCode.BadRequest, "Erro ao retornar estados",new Exception("Time out"));
            var logger = new Mock<ILogger<EstadosFacade>>();
            IEstadosFacade estadoFacade = new EstadosFacade(logger.Object, restMock.Object);

            // Act
            IEnumerable<EstadoDTO> estados = await estadoFacade.Buscar();

            // Assert
            Assert.Empty(estados);
            logger._AssertLog(LogLevel.Information, "Enviando requisição GET", 1);
            logger._AssertLog(LogLevel.Information, "Consulta executada com StatusCode", 0);
            logger._AssertLog(LogLevel.Error, "Time out", 1);
        }
    }
}
