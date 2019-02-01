using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMDuvg.Models
{
    public class ClientesViewModel
    {
        private ApplicationDbContext contexto;
        public List<ClientesBusqueda> Clientes { get; set; }

        public ClientesViewModel()
        {
            contexto = new ApplicationDbContext();
            Clientes = new List<ClientesBusqueda>();
        }

        // Búscar cliente por nombre
        public void BuscarPorNombre(string busqueda)
        {
            var consulta = from c in contexto.Clientes
                           where c.Nombre.Contains(busqueda)
                           select new
                           {
                               c.ClienteId,
                               c.RFC,
                               c.TipoPersonaSat,
                               Tipo = c.Tipo.NombreTipo ?? "",
                               NombreCliente = c.Nombre,
                               NombreContacto = (c.ContactoCliente.Nombre + " " + c.ContactoCliente.Apellidos),
                               Correo = (from e in contexto.Emails where e.Principal  select e.Direccion).FirstOrDefault() ?? "",
                               Telefono = (from t in c.Telefonos where t.Principal select t.Numero).FirstOrDefault() ?? "",
                               Direccion = (from d in c.Direcciones
                                                    where d.Principal
                                                    select d.Calle + " " + d.NumExterior + " " + d.NumInterior + " " +d.Departamento + " " + d.Municipio )
                                                              .FirstOrDefault() ?? ""
                           };

            Clientes.Clear();
            if (consulta != null)
            {
                var lclientes = consulta.ToList();
                foreach (var item in lclientes)
                {
                    Clientes.Add(new ClientesBusqueda
                    {
                        ClienteId = item.ClienteId,
                        Nombre = item.NombreCliente,
                        NombreContacto = item.NombreCliente,
                        RFC = item.RFC,
                        Tipo = item.Tipo,
                        TipoPesonaSat = item.TipoPersonaSat,
                        Correo = item.Correo,
                        Telefono = item.Telefono,
                        Direccion = item.Direccion
                    });
                }

            }
        }

        // Búscar cliente por email
        public void BuscarPorEmail (string email)
        {
            var consulta = from cl in contexto.Clientes
                           where cl.ClienteId ==
                           (from e in cl.Correos
                            where e.Direccion == email
                            select cl.ClienteId).FirstOrDefault()
                           select new
                           {
                               cl.ClienteId,
                               cl.RFC,
                               cl.TipoPersonaSat,
                               Tipo = cl.Tipo.NombreTipo ?? "",
                               NombreCliente = cl.Nombre,
                               NombreContacto = cl.ContactoCliente.Nombre + " " + cl.ContactoCliente.Apellidos ?? cl.Nombre,
                               Correo = (from e in cl.Correos
                                         where e.Principal
                                         select e.Direccion).FirstOrDefault() ?? "",
                               Telefono = (from t in cl.Telefonos
                                           where t.Principal
                                           select t.Numero).FirstOrDefault() ?? "",
                               Direccion = (from d in cl.Direcciones
                                            where d.Principal
                                            select d.Calle + " " + d.NumExterior + " " + d.Departamento + " " + d.Municipio)
                                            .FirstOrDefault() ?? ""

                           };
            Clientes.Clear();

            if (consulta != null)
            {
                var lclientes = consulta.ToList();
                foreach (var item in lclientes)
                {
                    Clientes.Add(new ClientesBusqueda
                    {
                        ClienteId = item.ClienteId,
                        Nombre = item.NombreCliente,
                        NombreContacto = item.NombreContacto,
                        RFC = item.RFC,
                        Tipo = item.Tipo,
                        TipoPesonaSat = item.TipoPersonaSat,
                        Correo = item.Correo,
                        Telefono = item.Telefono,
                        Direccion = item.Direccion
                    }); 
                }
            }
            
        }

        // Búscar por telefono
        public void BuscarPorTelefono(string telefono)
        {
            var consulta = from cl in contexto.Clientes
                           where cl.ClienteId ==
                           (from t in cl.Telefonos
                            where t.Numero == telefono
                            select cl.ClienteId).FirstOrDefault()
                           select new
                           {
                               cl.ClienteId,
                               cl.RFC,
                               cl.TipoPersonaSat,
                               Tipo = cl.Tipo.NombreTipo ?? "",
                               NombreCliente = cl.Nombre,
                               NombreContacto = cl.ContactoCliente.Nombre + " " + cl.ContactoCliente.Apellidos ?? cl.Nombre,
                               Correo = (from co in cl.Correos
                                         where co.Principal
                                         select co.Direccion).FirstOrDefault() ?? "",
                               Telefono = (from t in cl.Telefonos
                                           where t.Principal
                                           select t.Numero).FirstOrDefault() ?? "",
                               Direccion = (from d in cl.Direcciones
                                            where d.Principal
                                            select d.Calle + " " + d.NumExterior + " " + d.Departamento + " " + d.Municipio)
                                            .FirstOrDefault() ?? ""

                           };

            Clientes.Clear();
            if (consulta != null)
            {
                var lclientes = consulta.ToList();
                foreach (var item in lclientes)
                {
                    Clientes.Add(new ClientesBusqueda
                    {
                        ClienteId = item.ClienteId,
                        Nombre = item.NombreCliente,
                        NombreContacto = item.NombreContacto,
                        RFC = item.RFC,
                        Tipo = item.Tipo,
                        TipoPesonaSat = item.TipoPersonaSat,
                        Telefono = item.Telefono,
                        Correo = item.Correo,
                        Direccion = item.Direccion

                    });
                }

            }
        }

        
    }
}