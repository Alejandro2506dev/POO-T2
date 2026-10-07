using Newtonsoft.Json;
using POO_T2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POO_T2.Controllers
{
    public class AlumnoController : Controller
    {
        private static List<Alumno> alumnos = new List<Alumno>();
        private string json = "[]";

        private string Ruta()
        {
            return Server.MapPath("~/App_Data/alumnos.json");
        }

        private void Cargar()
        {
            string ruta = Ruta();
            if (!System.IO.File.Exists(ruta))
            {
                System.IO.File.WriteAllText(ruta, json);
            }
            json = System.IO.File.ReadAllText(ruta);
            alumnos = JsonConvert.DeserializeObject<List<Alumno>>(json) ?? new List<Alumno>();
        }

        private void Guardar()
        {
            json = JsonConvert.SerializeObject(alumnos, Formatting.Indented);
            System.IO.File.WriteAllText(Ruta(), json);
        }

        public ActionResult Index()
        {
            Cargar();
            return View(alumnos);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(Alumno alumno)
        {
            Cargar();
            Alumno existente = alumnos.FirstOrDefault(a => a.Dni == alumno.Dni);
            if (existente == null)
            {
                alumnos.Add(alumno);
                Guardar();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "El DNI " + alumno.Dni + " ya esta registrado";
            return View(alumno);
        }

        public ActionResult Eliminar(string dni)
        {
            Cargar();
            Alumno alumno = alumnos.FirstOrDefault(a => a.Dni == dni);
            if (alumno != null)
            {
                alumnos.Remove(alumno);
                Guardar();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Detalles(string dni)
        {
            Cargar();
            Alumno alumno = alumnos.FirstOrDefault(a => a.Dni == dni);
            if (alumno == null)
            {
                return RedirectToAction("Index");
            }
            return View(alumno);
        }

        [HttpGet]
        public ActionResult Actualizar(string dni)
        {
            Cargar();
            Alumno alumno = alumnos.FirstOrDefault(a => a.Dni == dni);
            if (alumno == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.DniOriginal = alumno.Dni;
            return View(alumno);
        }

        [HttpPost]
        public ActionResult Actualizar(Alumno alumno, string dniOriginal)
        {
            Cargar();
            Alumno actual = alumnos.FirstOrDefault(a => a.Dni == dniOriginal);
            Alumno repetido = alumnos.FirstOrDefault(a => a.Dni == alumno.Dni && a.Dni != dniOriginal);
            if (actual != null && repetido == null)
            {
                actual.Dni = alumno.Dni;
                actual.Nombres = alumno.Nombres;
                actual.Apellidos = alumno.Apellidos;
                actual.Carrera = alumno.Carrera;
                actual.Ciclo = alumno.Ciclo;
                Guardar();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "El DNI " + alumno.Dni + " ya esta registrado en otro alumno";
            ViewBag.DniOriginal = dniOriginal;
            return View(alumno);
        }

    }
}