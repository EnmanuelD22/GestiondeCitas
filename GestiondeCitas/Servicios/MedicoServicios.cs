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
            bool existe = medicos.Any(m => m.dni ==  medico.dni);

            if (existe)
            {
                Console.WriteLine("\nError. Este medico ya esta registrado.");
                return;
            }
            datos.Guardar(medico);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEl medico fue guardado de manera exitosa.");
            Console.ResetColor();
        }



    }
}
