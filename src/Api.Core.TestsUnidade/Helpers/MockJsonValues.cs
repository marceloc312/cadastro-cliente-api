using Api.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Api.Core.TestsUnidade
{
    public static class MockJsonValues
    {
        public static string Ufs()
        {
            return GetTextFromFileFile("ufs.json");
        }

        public static string MunicipiosSP()
        {
            return GetTextFromFileFile("municipios-sp.json");
        }

        public static string Cep()
        {
            return GetTextFromFileFile("cep-001001-000.json");
        }

        public static IEnumerable<EnderecoClienteDTO> EnderecosDTO()
        {
            var jsonText = GetTextFromFileFile("endereco-cliente.json");
            var listaEnderecos = JsonConvert.DeserializeObject<IEnumerable<EnderecoClienteDTO>>(jsonText);

            return listaEnderecos;
        }

        private static string GetTextFromFileFile(string fileName)
        {
            string dirAtual = Environment.CurrentDirectory;
            string fileFullName = Path.Combine(dirAtual.Substring(0, dirAtual.IndexOf(@"src\")), "src", typeof(ConsultarMuniciosTest).Assembly.GetName().Name, "Arquivos", fileName);
            Encoding isoLatin1 = Encoding.GetEncoding(28591);

            string result = File.ReadAllText(fileFullName, isoLatin1);
            return result;
        }
    }
}
