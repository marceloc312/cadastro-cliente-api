namespace Api.Core.ModelConfigs
{
    public class ParametroRestConsultaEstado : ParametroConsultaRestTerceirosTemplate
    {
    }

    public class OrdenacaoEstados
    {
        public string[] Rank { get; set; }
        public string Sentido { get; set; }
    }

}
