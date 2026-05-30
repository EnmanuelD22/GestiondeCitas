using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestiondeCitas.Interfaces;
using GestiondeCitas.Modelos;

namespace GestiondeCitas.Datos
{
      public class DatosCitas : IRepositorio<Cita>
    {
       private  List<Cita> Listacitas = new List<Cita>();

     
        public void Guardar(Cita cita)
        {
            Listacitas.Add(cita);
        }

        public List<Cita> mostrar()
        {
            return Listacitas;
        }

        public void Eliminar(Cita cita)
        {
            Listacitas.Remove(cita);
        }
    }
}
