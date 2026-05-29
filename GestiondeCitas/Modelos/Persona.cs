using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace GestiondeCitas.Modelos
{
     public abstract class Persona
    {
        private string dni;
        public string _dni { 
            get { return LimpiarCedula(_dni); }
            set { dni = ExtraerNumeros(value); } 
        }

        public string nombre { get; set; }
        private string _telefono;
        public string telefono
        {
            get { return FormatearTelefono(_telefono); }
            set { _telefono = ExtraerNumeros(value); }
        }

        public Persona(string nombre, string dni, string telefono)
        {
            this.nombre = nombre;
            this.dni = dni; 
            this.telefono = telefono; 
        }

        protected string ExtraerNumeros(string entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada)) return "";
            return Regex.Replace(entrada, @"[^\d", "");
        }

        protected string LimpiarCedula(string entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada)) return "";
            if (entrada.Length == 11)
            {
                return $"{entrada.Substring(0, 3)}-{entrada.Substring(3, 7)}-" +
                    $" {entrada.Substring(10, 1)}";
            } else
            {
                Console.WriteLine("Inserte un dato valido.");
            }
            return entrada;
        }

        protected string FormatearTelefono(string entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada)) return "";
            if (entrada.Length == 10)
            {
                return $"+1({entrada.Substring(0, 3)})-{entrada.Substring(3, 3)}-" +
                    $"{entrada.Substring(6, 4)}";
            } else
            {
                Console.WriteLine("Ingrese un dato valido.");
            }

            return entrada;
        }
        
    }
}
