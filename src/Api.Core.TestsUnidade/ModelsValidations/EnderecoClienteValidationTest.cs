using Api.Core.Models;
using FluentAssertions;
using Xunit;

namespace Api.Core.TestsUnidade
{
    [Trait(TRAIT_NAME, TRAIT_VALUE)]
    public class EnderecoClienteValidationTest
    {
        private const string TRAIT_NAME = "Endereço Cliente";
        private const string TRAIT_VALUE = "Validação de integridade";

        public EnderecoClienteValidationTest()
        {
        }

        [Theory(DisplayName = "Deve falhar, dados incompletos")]
        [InlineData(0, "", "", "", "", "", "")]
        [InlineData(0, "Rua 3", "", "", "", "", "")]
        [InlineData(0, "Rua 3", "456", "", "", "", "")]
        [InlineData(0, "Rua 3", "456", "", "São Paulo", "", "")]
        [InlineData(0, "Rua 3", "456", "", "São Paulo", "Brasil", "")]
        [InlineData(0, "Rua 3", "456", "", "São Paulo", "Brasil", "00998-988")]
        [InlineData(1, "Rua 3", "456", "", "São Paulo", "Brasil", "009980909988")]
        [InlineData(1, "Rua 3", "456", "", "São Paulo", "Brasil", "00")]
        public void DeveFalharDadosIncompletos(long idCliente, string logradouro, string numero, string complemento, string cidade, string pais, string cep)
        {
            // Arrange
            EnderecoCliente enderecoCliente = new EnderecoCliente(1, idCliente, logradouro, numero, complemento, cidade, pais, cep);

            // Act
            bool isValid = enderecoCliente.IsValid();

            // Assert
            isValid.Should().Be(false);
        }

        [Theory(DisplayName = "Endereço válido")]
        [InlineData(1, "Rua 3", "456", "", "São Paulo", "Brasil", "00998-988")]
        [InlineData(1, "Rua 3", "456", "", "São Paulo", "Brasil", "04138002")]
        public void EnderecoValido(long idCliente, string logradouro, string numero, string complemento, string cidade, string pais, string cep)
        {
            // Arrange
            EnderecoCliente enderecoCliente = new EnderecoCliente(1, idCliente, logradouro, numero, complemento, cidade, pais, cep);

            // Act
            bool isValid = enderecoCliente.IsValid();

            // Assert
            isValid.Should().Be(true);
        }
    }
}
