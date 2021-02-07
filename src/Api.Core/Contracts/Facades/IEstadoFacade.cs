using Api.Core.DTOs;
using Api.Core.DTOs.ACL;
using Api.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Facades
{
    public interface IEstadoFacade
    {
        Task<IEnumerable<EstadoUF>> ListarEstadosAsync();
        Task<IEnumerable<Municipio>> ListarMunicipiosPorEstadoAsync(int idEstado);
    }
}
