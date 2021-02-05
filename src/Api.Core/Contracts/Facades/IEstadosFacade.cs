using Api.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Facades
{
    public interface IEstadosFacade
    {
        Task<IEnumerable<EstadoDTO>> Buscar();
    }
}
