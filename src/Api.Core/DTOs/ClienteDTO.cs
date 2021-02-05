using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Core.DTOs
{
    public class ClienteDTO
    {
    }

    //public class Rootobject
    //{
    //    public Class1[] Property1 { get; set; }
    //}

    public class EstadoDTO
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public RegiaoDTO regiao { get; set; }
    }

    public class RegiaoDTO
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
    }

}
