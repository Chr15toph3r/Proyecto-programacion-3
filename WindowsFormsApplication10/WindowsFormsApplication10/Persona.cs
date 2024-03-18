using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaLecancer1
{
    public class Persona
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Condicion { get; set; }
        public bool EstaEnfermo { get; set; }
        public decimal Deuda { get; set; }
    }
}
