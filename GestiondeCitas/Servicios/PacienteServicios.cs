using GestiondeCitas.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestiondeCitas.Modelos;
using System.Threading.Tasks;

namespace GestiondeCitas.Servicios
{
    public class PacienteServicios
    {
        private readonly DatosPacientes pacientes;

        public PacienteServicios(DatosPacientes pacientes)
        {
            this.pacientes = pacientes;
        }

        public void registrar(Paciente paciente)
        {
            pacientes.Guardar(paciente);
        }
    }
}
