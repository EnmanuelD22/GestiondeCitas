using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Modelos
{
    public class Medico : Persona
    {
        int id;
        public Especialidad especialidad {  get; set; }
        public TimeOnly Horainicio {  get; set; }
        public TimeOnly Horafin { get; set; }

        public Medico (int id, Especialidad especialidad, string nombre, 
             string dni, string telefono, TimeOnly Horainicio, TimeOnly Horafin) : base (nombre, dni, telefono)
        {
            this.id = id;
            this.especialidad = especialidad;
            this.Horainicio = Horainicio;
            this.Horafin = Horafin; 
        }

       
    }
}
