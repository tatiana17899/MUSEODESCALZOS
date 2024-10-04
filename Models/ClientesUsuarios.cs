using System.Collections.Generic;

namespace MuseoDescalzos.Models
{
    public class ClientesUsuarios
    {
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
