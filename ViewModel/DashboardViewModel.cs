using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEO_DE_LOS_DESCALZOS.ViewModel
{
    public class DashboardViewModel
    {
        public int CantidadAlquileres { get; set; }
        public int CantidadVisitas { get; set; }
        public int CantidadEventos { get; set; }
         public FormAdminViewModel FormAdmin { get; set; } = new FormAdminViewModel();
        public List<VentaBase> ListVenta { get; set; } = new List<VentaBase>();
        public int Mes { get; set; } 
        public int Ano { get; set; }
        
    }
}