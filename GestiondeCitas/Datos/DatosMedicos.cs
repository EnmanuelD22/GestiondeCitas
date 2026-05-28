using GestiondeCitas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestiondeCitas.Modelos;

namespace GestiondeCitas.Datos
{
    public class DatosMedicos : IRepositorio<Medico>
    {
       private List<Medico> medicos = new List<Medico>();


        public void Guardar(Medico medico)
        {
            medicos.Add(medico);
        }

        public List<Medico> mostrar()
        {
            return medicos;
        }
        
    }
}
