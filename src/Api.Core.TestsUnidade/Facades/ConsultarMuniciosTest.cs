using Api.Core.Contracts.Facades;
using Api.Core.DTOs;
using Api.Core.DTOs.ACL;
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
        [Trait("[Testes Unitários] Estados Facade", "Consulta Municipios por Estado")]
    public partial class ConsultarMuniciosTest
    {
        public ConsultarMuniciosTest()
        {
        }

        [Theory(DisplayName = "Deve listar todos os municípios do Estado")]
        [InlineData(35)]
        public async void DeveRetornarTodosOsMunicipiosPorEstados(int idEstado)
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestEstadosServiceMock(HttpStatusCode.OK, MockJsonValues.MunicipiosSP());
            var logger = new Mock<ILogger<EstadosFacade>>();
            IEstadosFacade estadoFacade = new EstadosFacade(logger.Object, restMock.Object);

            // Act
            IEnumerable<MunicipioDTO> municipios = await estadoFacade.ListarMunicipiosPorEstado(idEstado);

            // Assert
            Assert.True(municipios.Any());
            municipios.FirstOrDefault().nome.Should().Be("Adamantina");
            municipios.FirstOrDefault().microrregiao.id.Should().Be(35035);
            municipios.FirstOrDefault().microrregiao.mesorregiao.nome.Should().Be("Presidente Prudente");
            municipios.FirstOrDefault().microrregiao.mesorregiao.UF.sigla.Should().Be("SP");
            municipios.FirstOrDefault().microrregiao.mesorregiao.UF.regiao.sigla.Should().Be("SE");
        }

        [Theory(DisplayName = "Resposta incorreta da Api de municipios de terceiros. Não deve retornar lista de municipios e deve logar")]
        [InlineData(89098888)]
        [InlineData(356)]
        public async void NaoDeveRetornarOsMunicipiosEDeveLogar(int idEstado)
        {
            // Arrange
            var restMock = TestHelperFactory.CreateRestEstadosServiceMock(HttpStatusCode.BadRequest, "Erro ao retornar municipios");
            var logger = new Mock<ILogger<EstadosFacade>>();
            IEstadosFacade estadoFacade = new EstadosFacade(logger.Object, restMock.Object);

            // Act
            IEnumerable<MunicipioDTO> estados = await estadoFacade.ListarMunicipiosPorEstado(idEstado);

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
            var logger = new Mock<ILogger<EstadosFacade>>();
            IEstadosFacade estadoFacade = new EstadosFacade(logger.Object, restMock.Object);

            // Act
            IEnumerable<MunicipioDTO> estados = await estadoFacade.ListarMunicipiosPorEstado(35);

            // Assert
            Assert.Empty(estados);
            logger._AssertLog(LogLevel.Information, "Enviando requisição GET", 1);
            logger._AssertLog(LogLevel.Information, "Consulta executada com StatusCode", 0);
            logger._AssertLog(LogLevel.Error, "Time out", 1);
        }
    }
}
