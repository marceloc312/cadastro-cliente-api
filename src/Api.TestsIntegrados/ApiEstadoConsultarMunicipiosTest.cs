using Api.Core.Models;
using Api.TestsIntegrados.Configurations;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Api.TestsIntegrados
{
    [Trait("[Testes Integrados] Api Localização", "Consulta Municipios")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class ApiEstadoConsultarMunicipiosTest
    {

        private readonly IntegrationTestFixture<Startup> _integrationTestFixture;
        public ApiEstadoConsultarMunicipiosTest(IntegrationTestFixture<Startup> integrationTestFixture)
        {
            _integrationTestFixture = integrationTestFixture;
        }

        [Theory(DisplayName = "Retorna o Município pelo Estados/UF se o Serviço de Terceiros estiver funcionando, senão deve retornar BadRequest")]
        [InlineData(35)]
        public async void ConsultarMunicipio(int idEstado)
        {
            // Arrange
            bool servicoTerceiroFuncionando = await HelperTest.PingDeVerificacaoServicoLocalizacaoEstadosMunicipios();

            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/servicos/consulta/estados/{idEstado}/municipios");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            if (servicoTerceiroFuncionando)
            {
                requisicao.StatusCode.Should().Be(HttpStatusCode.OK);
                Assert.True(requisicao.IsSuccessStatusCode);
                var municipio = JsonConvert.DeserializeObject<IEnumerable<Municipio>>(resposta);
                municipio.Should().NotBeEmpty();
                municipio.FirstOrDefault().Nome.Should().Be("Adamantina");
            }
            else
            {
                requisicao.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            }
        }

        [Theory(DisplayName = "Erro de comunicação com Api de terceiros, não deve retornar Municipios")]
        [InlineData(-1)]
        public async void ErroDeComunicacaoComApiDeTerceirosNaoDeveRetornarMunicipios(int idEstado)
        {
            // Arrange

            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/servicos/consulta/estados/{idEstado}/municipios");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            Assert.DoesNotContain("sigla", resposta);
        }
    }
}
