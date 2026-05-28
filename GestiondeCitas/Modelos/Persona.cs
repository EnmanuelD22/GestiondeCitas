using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Modelos
{
     public abstract class Persona
    {
        public string dni { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }

        public Persona(string nombre, string dni, string telefono)
        {
            this.nombre = nombre;
            this.dni = dni; 
            this.telefono = telefono; 
        }
        public abstract string mostrar();
    }
}
