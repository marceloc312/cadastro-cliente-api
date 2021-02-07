using Api.Core.DTOs;
using Api.Core.Models.Specifications;
using Api.TestsIntegrados.Configurations;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        [InlineData(1, 1, "Rua 3", "456", "", "São Paulo", "SP", "Brasil", "00998988")]
        public async void DeveAlterarOEnderecoDoClienteComSucesso(int id, long idCliente, string logradouro, string numero, string complemento, string cidade, string estado, string pais, string cep)
        {
            // Arrange
            EnderecoClienteDTO enderecoClienteDTO = new EnderecoClienteDTO(id, logradouro, numero, complemento, cidade, estado, pais, cep);
            string payloadEndereco = JsonConvert.SerializeObject(enderecoClienteDTO);
            HttpContent content = new StringContent(payloadEndereco, System.Text.Encoding.UTF8, "application/json");
            // Act
            var requisicao = await _integrationTestFixture.Client.PutAsync($"/api/v1.0/cliente/{idCliente}/enderecos/{id}", content);
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory(DisplayName = "Dados do endereço informados inválidos para alteração, deve retonar BadRequest")]
        [InlineData(1, 1, "Rua 3", "456", "", null, "SP", "", "00998-988")]
        public async void DadosInformadosInvalidos(int id, long idCliente, string logradouro, string numero, string complemento, string cidade, string estado, string pais, string cep)
        {
            // Arrange
            EnderecoClienteDTO enderecoClienteDTO = new EnderecoClienteDTO(id, logradouro, numero, complemento, cidade, estado, pais, cep);
            string payloadEndereco = JsonConvert.SerializeObject(enderecoClienteDTO);
            HttpContent content = new StringContent(payloadEndereco, System.Text.Encoding.UTF8, "application/json");
            // Act
            var requisicao = await _integrationTestFixture.Client.PutAsync($"/api/v1.0/cliente/{idCliente}/enderecos/{id}", content);
            var resposta = await requisicao.Content.ReadAsStringAsync();

            // Assert
            requisicao.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var validacoes = JsonConvert.DeserializeObject<IEnumerable<ErrorMessage>>(resposta);
            if (id == 1)
            {
                validacoes.Any(x => x.message == EnderecoClienteValidationMessages.CAMPO_CIDADE_NAO_INFORMADO).Should().BeTrue();
                validacoes.Any(x => x.message == EnderecoClienteValidationMessages.CEP_INVALIDO).Should().BeTrue();
                validacoes.Any(x => x.message == EnderecoClienteValidationMessages.PAIS_NAO_INFORMADO).Should().BeTrue();
            }
        }
    }

    public class ErrorMessage
    {
        public string message { get; set; }
    }

}
