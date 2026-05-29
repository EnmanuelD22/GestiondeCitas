using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace GestiondeCitas.Modelos
{
     public abstract class Persona
    {
        public string dni {  get; set; }
        public string nombre { get; set; }
       
        public string telefono { get; set; }
        
          
        

        public Persona(string nombre, string dni, string telefono)
        {
            this.nombre = nombre;
            this.dni = dni; 
            this.telefono = telefono; 
        }
    }
}
