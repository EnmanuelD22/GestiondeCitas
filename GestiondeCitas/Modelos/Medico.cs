using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Modelos
{
    public class Medico : Persona
    {
        public Especialidad especialidad {  get; set; }
        public TimeOnly Horainicio {  get; set; }
        public TimeOnly Horafin { get; set; }

        public Medico ( Especialidad especialidad, string nombre, 
             string dni, string telefono, TimeOnly Horainicio, TimeOnly Horafin) : base (nombre, dni, telefono)
        {
            this.especialidad = especialidad;
            this.Horainicio = Horainicio;
            this.Horafin = Horafin; 
        }

       
    }
}
