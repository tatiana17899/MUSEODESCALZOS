using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MuseoDescalzos.Models;
namespace MUSEO_DE_LOS_DESCALZOS.ViewModel
{
    public class GuiaViewModel
    {
        public Guía? FormGuia { get; set; }
        public List<Guía>? ListGuía { get; set; }
        public List<Tarea> ListaTareas { get; set; } = new List<Tarea>();
    }
}