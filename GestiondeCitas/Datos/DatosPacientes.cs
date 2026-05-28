using GestiondeCitas.Interfaces;
using GestiondeCitas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Datos
{
    public class DatosPacientes : IRepositorio<Paciente>
    {
        private List<Paciente> pacientes = new List<Paciente>();
        public void Guardar(Paciente paciente)
        {
            pacientes.Add(paciente);
        }

        public List<Paciente> mostrar()
        {
            return pacientes;
        }
    }
}
