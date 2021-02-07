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
    [Trait("[Testes Unitários] Estados Facade", "Consulta Municipios por Estado")]
    public partial class ConsultarMunicipiosTest
    {
        readonly IOptions<OrdenacaoEstados> optionsOrdenacao = Options.Create<OrdenacaoEstados>(new OrdenacaoEstados() { Rank = new[] { "SP", "RJ" }, Sentido = "Asc" });
        public ConsultarMunicipiosTest()
        {
        }

        [Theory(DisplayName = "Deve listar todos os municípios do Estado")]
        [InlineData(35)]
        public async void DeveRetornarTodosOsMunicipiosPorEstados(int idEstado)
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestEstadosServiceMock(HttpStatusCode.OK, MockJsonValues.MunicipiosSP());
            var logger = new Mock<ILogger<EstadoFacade>>();
            IEstadoFacade estadoFacade = new EstadoFacade(logger.Object, optionsOrdenacao, restMock.Object);

            // Act
            IEnumerable<Municipio> municipios = await estadoFacade.ListarMunicipiosPorEstadoAsync(idEstado);

            // Assert
            Assert.True(municipios.Any());
            municipios.FirstOrDefault().Nome.Should().Be("Adamantina");
            municipios.FirstOrDefault().Id.Should().Be(3500105);
        }

        [Theory(DisplayName = "Resposta incorreta da Api de municipios de terceiros. Não deve retornar lista de municipios e deve logar")]
        [InlineData(89098888)]
        [InlineData(356)]
        public async void NaoDeveRetornarOsMunicipiosEDeveLogar(int idEstado)
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestEstadosServiceMock(HttpStatusCode.BadRequest, "Erro ao retornar municipios");
            var logger = new Mock<ILogger<EstadoFacade>>();
            IEstadoFacade estadoFacade = new EstadoFacade(logger.Object, optionsOrdenacao, restMock.Object);

            // Act
            IEnumerable<Municipio> estados = await estadoFacade.ListarMunicipiosPorEstadoAsync(idEstado);

            // Assert
            Assert.Empty(estados);
            logger._AssertLog(LogLevel.Information, "Enviando requisição GET", 1);
            logger._AssertLog(LogLevel.Information, "Consulta executada com StatusCode", 1);
        }

        [Fact(DisplayName = "Falha na comunicação com a Api de terceiros")]
        public async void NaoDeveRetornarTodosOsEstadosEDeveLogar_HouveFalhaNaComunicacao()
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestEstadosServiceMock(HttpStatusCode.BadRequest, "Erro ao retornar estados", new Exception("Time out"));
            var logger = new Mock<ILogger<EstadoFacade>>();
            IEstadoFacade estadoFacade = new EstadoFacade(logger.Object, optionsOrdenacao, restMock.Object);

            // Act
            IEnumerable<Municipio> estados = await estadoFacade.ListarMunicipiosPorEstadoAsync(35);

            // Assert
            Assert.Empty(estados);
            logger._AssertLog(LogLevel.Information, "Enviando requisição GET", 1);
            logger._AssertLog(LogLevel.Information, "Consulta executada com StatusCode", 0);
            logger._AssertLog(LogLevel.Error, "Time out", 1);
        }
    }
}
