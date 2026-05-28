using GestiondeCitas.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Modelos
{
    public class Paciente : Persona
    {
        public Paciente(string nombre, string dni, string telefono)
            : base(nombre, dni, telefono) { }

        public override string mostrar()
        {
            return $"{nombre} (DNI: {dni}) | Tel: {telefono}";
        }
    }
}
