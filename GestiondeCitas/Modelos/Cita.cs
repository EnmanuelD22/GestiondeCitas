using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Modelos
{
      public class Cita
    {
        private static int contador = 1;
        public  int id { get; private set; }
        public Medico medico { get; set; } 
        public Paciente paciente { get; set; } 
        public DateOnly fecha {  get; set; }
        public TimeOnly hora { get; set;  }
        public string estado { get; set; }

        public Cita(Medico medico, Paciente paciente, DateOnly fecha, TimeOnly hora)
        {
            id = contador;
            this.medico = medico;
            this.paciente = paciente;
            this.fecha = fecha;
            estado = "Pendiente";
            this.hora = hora;
            contador++;
        }

        public void cancelarCita()
        {
            estado = "Cancelada";
        }

        public string ToString()
        {
            return

            "+--------------------------------------------------+\n" +
            "|                   CITA MEDICA                     |\n" +
            "+--------------------------------------------------+\n" +
            $"| Id Cita        : {id,-30}|\n" +
            $"| Fecha          : {fecha:hh:mm tt,-30}|\n" +
            $"| Estado         : {estado}|\n" +
            "+--------------------------------------------------+\n" +

            "|                  DATOS PACIENTE                  |\n" +
            "+--------------------------------------------------+\n" +
            $"| Nombre         : {paciente.nombre,-30}|\n" +
            $"| DNI            : {paciente.dni,-30}|\n" +
            $"| Telefono       : {paciente.telefono,-30}|\n" +
            "+--------------------------------------------------+\n" +

            "|                   DATOS MEDICO                   |\n" +
            "+--------------------------------------------------+\n" +
            $"| DR            : {medico.nombre,-30}|\n" +
            $"| Especialidad   : {medico.especialidad.nombre,-30}|\n" +
            "+--------------------------------------------------+\n";
        }
    }
}
