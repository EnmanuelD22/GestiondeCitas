using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestiondeCitas.Datos;
using GestiondeCitas.Modelos;

namespace GestiondeCitas.Servicios
{
    internal class EspecialidadService
    {
        private readonly DatosEspecialidades datos;

         public EspecialidadService(DatosEspecialidades datos)
        {
            this.datos = datos;
        }

        public List<Especialidad> listarEspecialidades()
        {
            return datos.mostrar();
        }

        public void guardar(Especialidad especialidad)
        {
            var especialidades = datos.mostrar();
            bool existe = especialidades.Any(e => e.nombre.Equals(especialidad.nombre,
                StringComparison.OrdinalIgnoreCase));

            if (existe)
            {
                Console.WriteLine("Error. Ya existe una especialidad con este nombre.");
                return ;
            }
            datos.Guardar(especialidad);

            Console.WriteLine("La especialidad fue guardada correctamente.");
        }
    }
}
