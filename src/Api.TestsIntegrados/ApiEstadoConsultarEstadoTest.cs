using Api.Core.Models;
using Api.TestsIntegrados.Configurations;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Xunit;

namespace Api.TestsIntegrados
{
    [Trait("[Testes Integrados] Api Localização", "Consulta Estados")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class ApiEstadoConsultarEstadoTest
    {

        private readonly IntegrationTestFixture<Startup> _integrationTestFixture;
        public ApiEstadoConsultarEstadoTest(IntegrationTestFixture<Startup> integrationTestFixture)
        {
            _integrationTestFixture = integrationTestFixture;
        }

        [Fact(DisplayName = "Retorna todos os Estados/UF")]
        public async void ConsultarUfs()
        {
            // Arrange
            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/servicos/consulta/estado");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.BadGateway);
            Assert.True(requisicao.IsSuccessStatusCode);
            var ufs = JsonConvert.DeserializeObject<IEnumerable<EstadoUF>>(resposta);
            ufs.Should().NotBeEmpty();
            Assert.Equal(27, ufs.Count());
        }

        [Fact(DisplayName = "Erro de comunicação com Api de terceiros, não deve retornar Estados/UF")]
        public async void ErroDeComunicacaoComApiDeTerceirosNaoDeveRetornarDadosUFs()
        {
            // Arrange
            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/servicos/consulta/estado");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            Assert.DoesNotContain("sigla", resposta);
        }
    }
}
