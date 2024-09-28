using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuseoDescalzos.Models;

namespace MUSEO_DE_LOS_DESCALZOS.ViewModel
{
    public class ContactoViewModel
    {
        public Contacto? FormContacto  {get; set;}
        public IEnumerable<Contacto>? ListContacto  {get; set;}
    }
}