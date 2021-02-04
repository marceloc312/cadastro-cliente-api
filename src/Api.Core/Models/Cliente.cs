using Api.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Core.Models
{
    public class Cliente
    {
        private ClienteDTO clienteDTO;

        public Cliente(ClienteDTO clienteDTO)
        {
            this.clienteDTO = clienteDTO;
        }
    }
}
