﻿using System;
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
        public ActionResult Create([Bind(Include = 
            "ClienteId," +
            "Nombre," +
            "RFC," +
            "TipoPersonaSat, " +
            "TipoClienteId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
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
        public ActionResult Edit([Bind(Include = "ClienteId,Nombre,RFC,TipoPersonaSat,TipoClienteId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
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
    }
}
