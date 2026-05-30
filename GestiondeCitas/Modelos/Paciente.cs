using GestiondeCitas.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Modelos
{
    public class Paciente : Persona
    {
        public string email;
        public Paciente(string nombre, string dni, string telefono, string email)
            : base(nombre, dni, telefono) {
            this.email = email;
        }
    }
}
