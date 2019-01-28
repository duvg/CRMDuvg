using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMDuvg.Models
{
    public class Telefono
    {
        public int TelefonoId { get; set; }

        public string Numero { get; set; }

        public string Tipo { get; set; }

        public bool Principal { get; set; }
    }
}