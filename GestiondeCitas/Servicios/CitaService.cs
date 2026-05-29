using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestiondeCitas.Modelos;
using GestiondeCitas.Datos;
using System.Runtime.InteropServices;
namespace GestiondeCitas.Servicios
{
     public class CitaService
    {
        private readonly DatosCitas citas;

        public CitaService(DatosCitas citas)
        {
            this.citas = citas;
        }
        
        public void Registrarcita(Cita cita)
        {
            var ci = citas.mostrar();
            bool existe = ci.Any(c => c.medico.dni.Equals(cita.medico.dni, StringComparison.OrdinalIgnoreCase) 
           &&
           c.fecha == cita.fecha
           );

            if (ci.Any(c => c.medico == null) || ci.Any(c => c.paciente == null)){
                Console.WriteLine("Error. La cita debe tener un paciente y un doctor asignado.");
                return;
            }

            bool estaDispinible = validarDisponibilidad(cita.medico, cita.fecha, cita.hora);

            if (!estaDispinible)
            {
                return;
            }

            if (existe)
            {
                Console.WriteLine($"Ya el Dr.{cita.medico.nombre} tiene una cita para esa fecha.");
                return;
            }
            citas.Guardar(cita);
        }

        public void Cancelar(int id)
        {
            var ci = citas.mostrar();
            Cita? cita = ci.FirstOrDefault(c => c.id == id);
            citas.Eliminar(cita);
            Console.WriteLine("Se ha eliminado la cita de manera correcta.");

        }

        public void Reprogramar(int id, DateOnly nfecha, TimeOnly nhora)
        {
            var ci = citas.mostrar();
            Cita? cita = ci.FirstOrDefault(c => c.id == id);
            
            if (cita == null)
            {
                Console.WriteLine("No existe una cita este id.");
                return;
            }

            bool existe = ci.Any(c => c.medico.dni == cita.medico.dni
            && c.fecha == nfecha && c.hora == nhora);

            if (existe)
            {
                Console.WriteLine($"El Dr.{cita.medico.nombre} ya tiene una cita agendada en este horario");
                return;
            }

            citas.Eliminar(cita);
            Console.WriteLine("Cita reprogramada correctamente");
        }
        
        public void ConsultarPorPaciente(string dni)
        {
            var ci = citas.mostrar();

            var resultado = ci.Where(c => c.paciente.dni.Equals(dni, 
                StringComparison.OrdinalIgnoreCase)).ToList();

            if (!resultado.Any())
            {
                Console.WriteLine("No hay citas asignadas para este paciente.");
            }

            foreach (var c in resultado){
                Console.WriteLine(c);
            }
        }
        
        public void ConsultarPorMedico(string dni)
        {
            var ci = citas.mostrar();

            var resultado = ci.Where(c => c.medico.dni.Equals(dni,
                StringComparison.OrdinalIgnoreCase)).ToList();

            if (!resultado.Any())
            {
                Console.WriteLine("No hay citas asignadas para este doctor.");
            }

            foreach (var c in resultado)
            {
                Console.WriteLine(c);
            }
        }

        public bool validarDisponibilidad(Medico medico, DateOnly fecha, TimeOnly hora)
        {
            if (hora < medico.Horainicio || hora > medico.Horafin)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error. La hora solicitada esta fuera del horario del medico.");
                Console.ResetColor();
                return false;
            }

            var ci = citas.mostrar();

            bool ocupado = ci.Any(c =>
            c.medico.dni.Equals(medico.dni, StringComparison.OrdinalIgnoreCase) &&
            c.fecha == fecha && c.hora == hora && c.estado == "Pendiente");

            if (ocupado)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error. El Dr{medico.nombre} ya tiene una cita en este horario");
                return false;
            }

            return true;
        }
    }
}
