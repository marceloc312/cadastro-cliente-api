using Api.Core.DTOs;
using Api.Core.Models;
using Api.TestsIntegrados.Configurations;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace Api.TestsIntegrados
{
    [Trait("[Testes Integrados] Api Endereço", "Consulta Endereço")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class ApiEnderecoConsultaTest
    {
        private readonly IntegrationTestFixture<Startup> _integrationTestFixture;
        public ApiEnderecoConsultaTest(IntegrationTestFixture<Startup> integrationTestFixture)
        {
            _integrationTestFixture = integrationTestFixture;
        }

        [Theory(DisplayName = "Retorna todos os Endereços do Cliente")]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public async void RetornaTodosOsEnderecosDoCliente(int idCliente, int qtdEnderecosNoBanco)
        {
            // Arrange

            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/cliente/{idCliente}/enderecos");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.True(requisicao.IsSuccessStatusCode);
            var enderecos = JsonConvert.DeserializeObject<IEnumerable<EnderecoClienteDTO>>(resposta);
            Assert.Equal(qtdEnderecosNoBanco, enderecos.Count());
        }

        [Theory(DisplayName = "Retorna o Endereço do Cliente pelo id do Endereço na rota cliente/{idCliente}/enderecos/{idEndereco}")]
        [InlineData(4, 5, "Rua Antonio Caserta")]
        [InlineData(1, 2, "Rua Cerqueira Cesar")]
        [InlineData(2, 3, "Avenida Santo Inacio")]
        public async void RetornaEnderecoPorIdNaRotaClienteID(int idCliente, int idEndereco, string assertLogradouro)
        {
            // Arrange

            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/cliente/{idCliente}/enderecos/{idEndereco}");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.True(requisicao.IsSuccessStatusCode);
            var endereco = JsonConvert.DeserializeObject<EnderecoClienteDTO>(resposta);
            Assert.NotNull(endereco);
            endereco.Logradouro.Should().Be(assertLogradouro);
        }

        [Theory(DisplayName = "Não deve retornar endereço de outro Cliente")]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(1, 3)]
        public async void NaoDeveRetornarEnderecoDeOutroCliente(int idCliente, int idEndereco)
        {
            // Arrange

            // Act
            var requisicao = await _integrationTestFixture.Client.GetAsync($"/api/v1.0/cliente/{idCliente}/enderecos/{idEndereco}");
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var endereco = JsonConvert.DeserializeObject<EnderecoClienteDTO>(resposta);
            endereco.Logradouro.Should().BeNull();
            endereco.Pais.Should().BeNull();
        }
    }
}
