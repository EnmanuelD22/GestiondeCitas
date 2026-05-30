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

        public override string ToString()
        {
            return

            "\n+------------------------------------------------+\n" +
            "|                   CITA MEDICA                  |\n" +
            "+------------------------------------------------+\n" +
            $"| Id Cita        :{id,-31}|\n" +
            $"| Fecha          :{fecha,-31}|\n" +
            $"| Hora           :{hora,-31}|\n" +
            $"| Estado         :{estado,-31}|\n" +
            "+------------------------------------------------+\n" +

            "|                  DATOS PACIENTE                |\n" +
            "+------------------------------------------------+\n" +
            $"| Nombre         : {paciente.nombre,-29} |\n" +
            $"| DNI            : {paciente.dni,-29} |\n" +
            $"| Telefono       : {paciente.telefono,-29} |\n" +
            "+------------------------------------------------+\n" +

            "|                   DATOS MEDICO                 |\n" +
            "+------------------------------------------------+\n" +
            $"| DR             : {medico.nombre,-30}|\n" +
            $"| Especialidad   : {medico.especialidad.nombre,-30}|\n" +
            "+------------------------------------------------+\n";
        }
    }
}
