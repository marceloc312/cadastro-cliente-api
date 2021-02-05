using Api.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Facades
{
    public interface ICepFacade
    {
        Task<CepDTO> Buscar(string cep);
    }
}
