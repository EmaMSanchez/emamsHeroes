using HeroeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeroeApp.Controllers
{
    public class HeroeController : Controller
    {
        private HeroeBD BD;

        public HeroeController()
        {
            BD = new HeroeBD();
        }
            
        // GET: Heroe
        public ActionResult Index()
        {
            var listado = BD.Heroe.ToList();
            return View(listado);
        }

        public ActionResult Detalle(int id)
        {
            var heroe = BD.Heroe.FirstOrDefault(x => x.IdHeroe == id);
            return View(heroe);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(Heroe model)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var imagen = System.Web.HttpContext.Current.Request.Files["Imagen"];
                string logoPath = Server.MapPath("~/ Content / Images /");
                string filename = imagen.FileName;

                imagen.SaveAs(logoPath + filename);

                model.Imagen = "~/ Content / Images /" + imagen.FileName;
                BD.Heroe.Add(model);
                BD.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Actualizar(int id)
        {
            var edit = BD.Heroe.FirstOrDefault(x => x.IdHeroe == id);
            return View(edit);
        }

        [HttpPost]
        public ActionResult Actualizar(Heroe heroe)
        {
            var heroeactualizado = BD.Heroe.FirstOrDefault(x => x.IdHeroe == heroe.IdHeroe);
            heroeactualizado.Nombre = heroe.Nombre;
            heroeactualizado.Historia = heroe.Historia;
            heroeactualizado.Poder = heroe.Poder;
            heroeactualizado.Debilidad = heroe.Debilidad;
            BD.SaveChanges();

            return RedirectToAction("Index");
        }
    }

}
