using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMDuvg.Models
{
    public class Contacto
    {
        public int ContactoId { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public bool Principal { get; set; }

        public ICollection<Telefono> Telefonos { get; set; }

        public ICollection<Email> Correos { get; set; }

        public ICollection<Direccion> Direcciones { get; set; }
    }
}