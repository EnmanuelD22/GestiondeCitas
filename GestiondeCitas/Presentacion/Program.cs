using GestiondeCitas.Datos;
using GestiondeCitas.Modelos;
using GestiondeCitas.Presentacion.UI;
using GestiondeCitas.Servicios;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;


class Program
{
    

    public static void Main(string[] args)
    {
        SistemaUI aplicacion = new SistemaUI();
        aplicacion.Iniciar();
    }
}