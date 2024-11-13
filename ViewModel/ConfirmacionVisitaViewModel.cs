using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEODESCALZOS.ViewModel
{
    public class ConfirmacionVisitaViewModel
    {
        public Cliente? Cliente { get; set; }
        public List<ReservarVisitaViewModel> Visitas { get; set; }
        public decimal Total { get; set; }
    }
}