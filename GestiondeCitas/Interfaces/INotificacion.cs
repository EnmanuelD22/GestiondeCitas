using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Interfaces
{
     public interface INotificacion
    {
        public void enviar(string email, string mensaje);
    }
}
