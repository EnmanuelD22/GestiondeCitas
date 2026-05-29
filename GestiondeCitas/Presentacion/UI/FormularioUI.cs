using GestiondeCitas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Presentacion.UI
{
    public class FormularioUI
    {
        public string LimpiarDni(string entradaTeclado)
        {
            if (string.IsNullOrWhiteSpace(entradaTeclado))
            {
                return "";
            }

            return System.Text.RegularExpressions.Regex.Replace(entradaTeclado, @"[^\d]", "");
        }
       
    }
}
