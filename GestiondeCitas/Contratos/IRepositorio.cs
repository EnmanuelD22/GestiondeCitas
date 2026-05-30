using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Interfaces
{
     public interface IRepositorio<T>
    {
        void Guardar(T entidad);
        List<T> mostrar();
        
    }
}
