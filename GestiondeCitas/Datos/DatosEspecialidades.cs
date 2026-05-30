using GestiondeCitas.Interfaces;
using GestiondeCitas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Datos
{
     public class DatosEspecialidades : IRepositorio<Especialidad>
    {
        private static List<Especialidad> especialidades = new List<Especialidad>() {
            new Especialidad("Cardiologia"),
            new Especialidad("Neurologia"),
            new Especialidad("Neumologia"),
            new Especialidad("Medicina General")
        };
     
        public void Guardar(Especialidad especialidad)
        {
            especialidades.Add(especialidad);
        }

        public List<Especialidad> mostrar()
        {
            return especialidades;
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
