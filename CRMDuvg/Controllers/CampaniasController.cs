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
    public class CampaniasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Campanias
        public ActionResult Index()
        {
            return View(db.Campanias.ToList());
        }

        // GET: Campanias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campania campania = db.Campanias.Find(id);
            if (campania == null)
            {
                return HttpNotFound();
            }
            return View(campania);
        }

        // GET: Campanias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campanias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CampaniaId,Nombre,FechaPlan,Fecha")] Campania campania)
        {
            if (ModelState.IsValid)
            {
                db.Campanias.Add(campania);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(campania);
        }

        // GET: Campanias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campania campania = db.Campanias.Find(id);
            if (campania == null)
            {
                return HttpNotFound();
            }
            return View(campania);
        }

        // POST: Campanias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampaniaId,Nombre,FechaPlan,Fecha")] Campania campania)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campania).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(campania);
        }

        // GET: Campanias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campania campania = db.Campanias.Find(id);
            if (campania == null)
            {
                return HttpNotFound();
            }
            return View(campania);
        }

        // POST: Campanias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campania campania = db.Campanias.Find(id);
            db.Campanias.Remove(campania);
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
