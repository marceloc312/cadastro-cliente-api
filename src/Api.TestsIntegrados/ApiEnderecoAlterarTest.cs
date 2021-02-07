using Api.Core.DTOs;
using Api.TestsIntegrados.Configurations;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Api.TestsIntegrados
{
    [Trait("[Testes Integrados] Api Endereço", "Alterar Endereço")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class ApiEnderecoAlterarTest
    {
        private readonly IntegrationTestFixture<Startup> _integrationTestFixture;
        public ApiEnderecoAlterarTest(IntegrationTestFixture<Startup> integrationTestFixture)
        {
            _integrationTestFixture = integrationTestFixture;
        }

        [Theory(DisplayName = "Deve alterar o Endereço do Cliente com sucesso")]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public async void DeveAlterarOEndrecoComSucesso(int idCliente, int qtdEnderecosNoBanco)
        {
            // Arrange

            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/cliente/{idCliente}/endereco");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.True(requisicao.IsSuccessStatusCode);
            var enderecos = JsonConvert.DeserializeObject<IEnumerable<EnderecoClienteDTO>>(resposta);
            Assert.Equal(qtdEnderecosNoBanco, enderecos.Count());
        }
    }
}
