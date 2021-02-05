using Api.Core.DTOs;
using Api.Core.DTOs.ACL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Facades
{
    public interface IEstadosFacade
    {
        Task<IEnumerable<UFDTO>> ListarUFs();
        Task<IEnumerable<MunicipioDTO>> ListarMunicipiosPorEstado(int idEstado);
    }
}
