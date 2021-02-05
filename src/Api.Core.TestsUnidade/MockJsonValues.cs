using System;
using System.IO;

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

        private static string GetTextFromFileFile(string fileName)
        {
            string dirAtual = Environment.CurrentDirectory;
            string fileFullName = Path.Combine(dirAtual.Substring(0, dirAtual.IndexOf(@"src\")), "src", typeof(ConsultarMuniciosTest).Assembly.GetName().Name, "Arquivos", fileName);

            string result = File.ReadAllText(fileFullName);
            return result;
        }
    }
}
