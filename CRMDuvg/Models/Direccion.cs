using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMDuvg.Models
{
    public class Direccion
    {
        public int DireccionId { get; set; }

        public string Calle { get; set; }

        public string NumExterior { get; set; }

        public string NumInterior { get; set; }

        public string Departamento { get; set; }

        public string Municipio { get; set; }

        public bool Principal { get; set; } 
    }
}