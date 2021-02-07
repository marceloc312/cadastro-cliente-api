using Api.Core.Models;
using Api.TestsIntegrados.Configurations;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text.RegularExpressions;
using Xunit;

namespace Api.TestsIntegrados
{
    [Trait("[Testes Integrados] Api Localização", "Consulta CEP")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class ApiCepTest
    {
        private const string CEP_1 = "01001-000";
        private const string CEP_2 = "04138001";
        private const string CEP_3 = "04470-060";

        private readonly IntegrationTestFixture<Startup> _integrationTestFixture;
        public ApiCepTest(IntegrationTestFixture<Startup> integrationTestFixture)
        {
            _integrationTestFixture = integrationTestFixture;
        }

        [Theory(DisplayName = "Retorna o endereço do CEP informado")]
        [InlineData(CEP_1)]
        [InlineData(CEP_2)]
        [InlineData(CEP_3)]
        public async void ConsultaClienteComSucesso(string cep)
        {
            // Arrange
            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/servicos/consulta/cep/{cep}");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.True(requisicao.IsSuccessStatusCode);
            var enderecoCep = JsonConvert.DeserializeObject<EnderecoCep>(resposta);
            string cepPesquisa = Regex.Replace(cep, @"[^\d]", "");
            string cepEncontrado = Regex.Replace(enderecoCep.Cep, @"[^\d]", "");
            Assert.Equal(cepPesquisa, cepEncontrado);
            enderecoCep.Logradouro.Should().NotBeNullOrEmpty();
        }

        [Theory(DisplayName = "Parâmetros inválido, deve retornar NotFound")]
        [InlineData("00000000")]
        [InlineData("AJHOIUYT")]
        [InlineData("@$#%^&*|")]
        public async void CepNaoEncontrado(string cep)
        {
            // Arrange
            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/servicos/consulta/cep/{cep}");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory(DisplayName = "Erro ao processar a solicitação, deve retornar InternalServerError")]
        [InlineData(null)]
        [InlineData("")]
        public async void ErroAoPorcessarSolicitacao(string cep)
        {
            // Arrange
            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/servicos/consulta/cep/{cep}");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}
