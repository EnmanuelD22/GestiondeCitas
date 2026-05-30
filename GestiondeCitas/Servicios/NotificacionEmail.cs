using GestiondeCitas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Servicios
{
    internal class NotificacionEmail : INotificacion
    {
        public void enviar(string email, string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nRecordatorio para: {email}");
            Console.WriteLine($"   {mensaje}");
            Console.ResetColor();
        }
    }
}
