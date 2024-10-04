using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEO_DE_LOS_DESCALZOS.ViewModel
{
    public class GuiaDashboardViewModel
    {
        public IEnumerable<Tarea> Tareas { get; set; }
        public int TareasCompletadas { get; set; }
        public int TareasNoCompletadas { get; set; }
        public IEnumerable<PedidoVisita> Visitas { get; set; }
    }
}