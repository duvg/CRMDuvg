using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRMDuvg.Models;

namespace CRMDuvg.Controllers
{
    public class ClientesController : Controller
    {   
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clientes
        public ActionResult Index()
        {
            BusquedaClienteModelView cliente = new BusquedaClienteModelView();
            return View(cliente);
        }

        // Búscar por nombre
        [HttpPost]
        public ActionResult BuscarNombre(BusquedaClienteModelView model)
        {
            if (ModelState.IsValid)
            {
                ClientesViewModel clientes = new ClientesViewModel();
                clientes.BuscarPorNombre(model.NombreBuscar);
                return PartialView("_ListadoClientes", clientes.Clientes);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View("index", model);
            }
        }

        // Búscar por correo electronico
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarEmail(BusquedaClienteModelView model)
        {
            if (ModelState.IsValid)
            {
                ClientesViewModel clientes = new ClientesViewModel();
                clientes.BuscarPorEmail(model.NombreBuscar);
                return PartialView("_ListadoClientes", clientes.Clientes);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View("index", model);
            }
        }

        // Búscar por telefono
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarTelefono(BusquedaClienteModelView model)
        {
            if (ModelState.IsValid)
            {
                ClientesViewModel clientes = new ClientesViewModel();
                clientes.BuscarPorTelefono(model.NombreBuscar);
                return PartialView("_ListadoClientes", clientes.Clientes);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View("index", model);
            }
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            var list = new SelectList(
                new[] {
                    new { ID="", Name="--SELECCIONE EL TIPO DE PERSONA"},
                    new { ID="PERSONA FISICA", Name="PERSONA FISICA"},
                    new { ID="PERSONA MORAL", Name="PERSONA MORAL" },
                },
                "ID", "Name", 1);
            var tipos = new SelectList(db.TipoClientes.ToList(), "TipoClienteId", "NombreTipo");
            ViewData["list"] = list;
            ViewData["tipos"] = tipos;
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return Json(true);
            }

            return Json(false);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cliente cliente = db.Clientes.Find(id);

            if (cliente == null)
            {
                return HttpNotFound();
            }

            db.Entry(cliente).Collection(d => d.Telefonos).Load();
            db.Entry(cliente).Collection(d => d.Correos).Load();
            db.Entry(cliente).Collection(d => d.Direcciones).Load();

            var list = new SelectList(
                new[] {
                    new { ID="", Name="--SELECCIONE EL TIPO DE PERSONA"},
                    new { ID="PERSONA FISICA", Name="PERSONA FISICA"},
                    new { ID="PERSONA MORAL", Name="PERSONA MORAL" },
                },
                "ID", "Name", 1);
            var tipos = new SelectList(db.TipoClientes.ToList(), "TipoClienteId", "NombreTipo");
            ViewData["list"] = list;
            ViewData["tipos"] = tipos;

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit( Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                // Cargamosel cliente para modificar
                Cliente clienteModificar = db.Clientes.Find(cliente.ClienteId);
                db.Entry(clienteModificar).Collection(d => d.Telefonos).Load();
                db.Entry(clienteModificar).Collection(d => d.Correos).Load();
                db.Entry(clienteModificar).Collection(d => d.Direcciones).Load();

                this.ProcesarTelefonos(cliente, clienteModificar);
                this.ProcesarEmails(cliente, clienteModificar);
                this.ProcesarDirecciones(cliente, clienteModificar);

                clienteModificar.TipoClienteId = cliente.TipoClienteId;
                clienteModificar.Nombre = cliente.Nombre;
                clienteModificar.Tipo = cliente.Tipo;
                clienteModificar.TipoPersonaSat = cliente.TipoPersonaSat;

                db.Entry(clienteModificar).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true);
            }
            return Json(false);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Comparar las listas de telefonos, emails y direcciones de la base de datos
        // con las que proviene del formulario para asi detectar los cambios y proceder
        // a actualiar las listas
        private void ProcesarTelefonos(Cliente cliente, Cliente clienteModificar)
        {
            var telefonosExistentes = new List<Telefono>();
            if (clienteModificar.Telefonos != null)
            {
                telefonosExistentes = clienteModificar.Telefonos.ToList<Telefono>();
            }

            var telefonosModificados = new List<Telefono>();
            if (cliente.Telefonos != null)
            {
                telefonosModificados = cliente.Telefonos.ToList<Telefono>();
            }
            var telefonosAgregar = telefonosModificados.Except(telefonosExistentes);
            var telefonosEliminar = telefonosExistentes.Except(telefonosModificados);

            telefonosEliminar.ToList<Telefono>().ForEach(t => db.Entry(t).State = System.Data.Entity.EntityState.Deleted);

            foreach (Telefono telefono in telefonosAgregar)
            {
                clienteModificar.Telefonos.Add(telefono);
            }
        }

        private void ProcesarEmails(Cliente cliente, Cliente clienteModificar)
        {
            var emailsExistentes = new List<Email>();
            if (clienteModificar.Correos != null)
            {
                emailsExistentes = clienteModificar.Correos.ToList<Email>();
            }

            var emailsModificados = new List<Email>();
            if (cliente.Telefonos != null)
            {
                emailsModificados = cliente.Correos.ToList<Email>();
            }
            var emailsAgregar = emailsModificados.Except(emailsExistentes);
            var emailsEliminar = emailsExistentes.Except(emailsModificados);

            emailsEliminar.ToList<Email>().ForEach(t => db.Entry(t).State = System.Data.Entity.EntityState.Deleted);

            foreach (Email email in emailsAgregar)
            {
                clienteModificar.Correos.Add(email);
            }
        }

        private void ProcesarDirecciones(Cliente cliente, Cliente clienteModificar)
        {
            var direccionesExistentes = new List<Direccion>();
            if (clienteModificar.Direcciones != null)
            {
                direccionesExistentes = clienteModificar.Direcciones.ToList<Direccion>();
            }

            var direccionesModificados = new List<Direccion>();
            if (cliente.Direcciones != null)
            {
                direccionesModificados = cliente.Direcciones.ToList<Direccion>();
            }
            var direccionesAgregar = direccionesModificados.Except(direccionesExistentes);
            var direccionesEliminar = direccionesExistentes.Except(direccionesModificados);

            direccionesEliminar.ToList<Direccion>().ForEach(t => db.Entry(t).State = System.Data.Entity.EntityState.Deleted);

            foreach (Direccion direccion in direccionesAgregar)
            {
                clienteModificar.Direcciones.Add(direccion);
            }
        }
    }
}
