using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MUSEO_DE_LOS_DESCALZOS.ViewModel;
using MuseoDescalzos.Models;
using MUSEODESCALZOS.Models;

namespace MUSEODESCALZOS.ViewModel
{
    public class ConfirmacionViewModel
    {
        public Cliente? Cliente { get; set; }
        public List<EventoViewModel>? Eventos { get; set; }
        public decimal Total { get; set; }
    }
}