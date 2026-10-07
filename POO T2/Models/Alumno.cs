using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POO_T2.Models
{
    public class Alumno
    {
        public string Dni { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Carrera { get; set; }
        public int Ciclo { get; set; }

        public Alumno()
        {
        }

        public Alumno(string dni, string nombres, string apellidos, string carrera, int ciclo)
        {
            Dni = dni;
            Nombres = nombres;
            Apellidos = apellidos;
            Carrera = carrera;
            Ciclo = ciclo;
        }
    }
}