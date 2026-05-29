using GestiondeCitas.Datos;
using GestiondeCitas.Interfaces;
using GestiondeCitas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Servicios
{
    public class MedicoServicios 
    {
        private readonly DatosMedicos datos;

        public MedicoServicios(DatosMedicos datos)
        {
            this.datos = datos;
        }

        public void Registrar(Medico medico)
        {
            var medicos = datos.mostrar();
            bool existe = medicos.Any(m => m._dni ==  medico._dni);

            if (existe)
            {
                Console.WriteLine("Error. Este medico ya esta registrado.");
                return;
            }
            datos.Guardar(medico);
        }



    }
}
