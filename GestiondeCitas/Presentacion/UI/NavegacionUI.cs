using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestiondeCitas.Presentacion.UI
{
    public class NavegacionUI
    {
        public int MostrarMenu(string titulo, string encabezado, string[] opciones)
        {
            int indiceSeleccionado = 0;

            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;

                Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                                               ║");
                Console.WriteLine($"║                        {  titulo       }                          ║");
                Console.WriteLine($"║                       { encabezado }                        ║");
                Console.WriteLine("║                                                               ║");
                Console.WriteLine("╠═══════════════════════════════════════════════════════════════╣");
                Console.WriteLine("║                                                               ║");
                Console.WriteLine("║      Navegación: [^] [v] Moverse    [ENTER] Seleccionar       ║");
                Console.WriteLine("║                                                               ║");
                Console.WriteLine("║                                                               ║");

                for (int i = 0; i < opciones.Length; i++)
                {
                    string opcion = opciones[i];

                    if (i == indiceSeleccionado)
                    {
                        string indicador = $"        >   {opcion}  <";
                        Console.Write("║");

                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;

                        Console.Write(indicador.PadRight(63));
                        Console.ResetColor();
                        Console.WriteLine("║");
                    } else
                    {
                        string indicadorNormal = $"        {opcion}   ";
                        Console.Write("║");
                        Console.Write(indicadorNormal.PadRight(63));
                        Console.WriteLine("║");
                    }
                }

                Console.WriteLine("║                                                               ║");
                Console.WriteLine("║                                                               ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");

                ConsoleKey tecla = Console.ReadKey(true).Key;

                if (tecla == ConsoleKey.DownArrow)
                {
                    if (indiceSeleccionado < opciones.Length-1 )
                    {
                        indiceSeleccionado++;
                    }
                } else if (tecla == ConsoleKey.UpArrow)
                {
                    if (indiceSeleccionado > 0)
                    {
                        indiceSeleccionado--;
                    }
                } else if (tecla == ConsoleKey.Enter)
                {
                    Console.CursorVisible = true;
                    return indiceSeleccionado;
                }
            }
        }
    }
}
