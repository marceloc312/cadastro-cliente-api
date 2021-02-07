using Api.Core.Models;
using Api.TestsIntegrados.Configurations;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using Xunit;

namespace Api.TestsIntegrados
{
    [Trait("[Testes Integrados] Api Cliente", "Consulta cliente")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class ApiClienteConsultarTest
    {
        private const string CPF_MARCELO = "93939714046";
        private const string CPF_TEREZA = "58939041097";
        private const string CPF_RENATA = "89781767049";

        private readonly IntegrationTestFixture<Startup> _integrationTestFixture;
        public ApiClienteConsultarTest(IntegrationTestFixture<Startup> integrationTestFixture)
        {
            _integrationTestFixture = integrationTestFixture;
        }

        [Theory(DisplayName = "Retorna o Cliente pelo CPF com sucesso")]
        [InlineData(CPF_MARCELO)]
        [InlineData(CPF_TEREZA )]
        [InlineData(CPF_RENATA)]
        public async void ConsultaClienteComSucesso(string cpf)
        {
            // Arrange

            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/cliente/{cpf}");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.True(requisicao.IsSuccessStatusCode);
           var cliente= JsonConvert.DeserializeObject<Cliente>(resposta);
            cliente.CPF.Should().Be(cpf);
            cliente.Nome.Should().NotBeNullOrEmpty();
        }

        [Theory(DisplayName = "Retorna NotFound")]
        [InlineData("50494135085")]
        [InlineData("44799157027")]
        public async void RetornaNotFound(string cpf)
        {
            // Arrange

            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/cliente/{cpf}");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.NotFound);
            resposta.Should().NotContain(cpf);
        }

        [Theory(DisplayName = "Retorna InternalServerError")]
        [InlineData(null)]
        [InlineData("")]
        public async void RetornaInternalServerError(string cpf)
        {
            // Arrange

            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/cliente/{cpf}");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Theory(DisplayName = "Retorna BadRequest")]
        [InlineData("cpf incorreto")]
        public async void BadRequest(string cpf)
        {
            // Arrange

            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/cliente/{cpf}");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
    }
