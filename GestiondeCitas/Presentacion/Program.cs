using GestiondeCitas.Servicios;
using GestiondeCitas.Modelos;
using GestiondeCitas.Datos;
using GestiondeCitas.Presentacion.UI;
using System.Security.Cryptography.X509Certificates;
using GestiondeCitas.Servicios;

class Program
{
    private static readonly DatosCitas citas = new DatosCitas();
    private static readonly DatosEspecialidades especialidades = new DatosEspecialidades();
    private static readonly DatosMedicos medicos = new DatosMedicos();
    private static readonly DatosPacientes pacientes = new DatosPacientes();

    public static void Main(string[] args)
    {
        NavegacionUI navegacion = new NavegacionUI();
        EspecialidadService especialidad = new EspecialidadService(especialidades);
        MedicoServicios medico = new MedicoServicios(medicos);
        CitaService cita = new CitaService(citas);
        PacienteServicios paciente = new PacienteServicios(pacientes);

    

    string[] opcionesMenu = {
            "Registrar Nuevo Paciente",
            "Registrar Nuevo Médico",
            "Gestión de Especialidades",
            "Agendar Nueva Cita",
            "Consultar Citas por Medico",
            "Consultar Citas por Paciente",
            "Cancelar una Cita",
            "Reprogramar Cita",
            "[Salir del Sistema]"
        };

        bool ejecutando = true;

        while (ejecutando)
        {
            int seleccion = navegacion.MostrarMenu("M E D I A P P", "GESTION DE CITAS", opcionesMenu);

            switch (seleccion)
            {
                case 0: RegistrarPaciente(); break;
                case 1: RegistrarMedico(); break;
                case 2: GestionarEspecialidad(especialidad); break;
                case 3: AgendarCita(); break;
                case 4: ConsultarporMedico(); break;
                case 5: ConsultarporPaciente(); break;
                case 6: CancelarCita(); break;
                case 7: ReprogramarCita(); break;
                case 8: Salir(); ejecutando = false; break;
            }
        }

    }

    public static void RegistrarPaciente()
    {
        Console.Clear();
        Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("                   Ingrese el nuevo paciente                    ");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();
        Console.Write("DNI: ");
        string dni = Console.ReadLine();
        Console.Write("Tel: ");
        string telefono = Console.ReadLine();

    }
    public static void RegistrarMedico()
    {

    }
    public static void GestionarEspecialidad(EspecialidadService especialidad)
    {
        Console.Clear();

        Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                  CATÁLOGO DE ESPECIALIDADES                   ║");

        Console.WriteLine("╠═══════════════════════════════════════════════════════════════╣");

        Console.WriteLine("║ NOMBRE DE LA ESPECIALIDAD                                     ║");

        Console.WriteLine("╠═══════════════════════════════════════════════════════════════╣");

        if (especialidad.listarEspecialidades() == null || especialidad.listarEspecialidades().Count == 0)
        {
            string mensaje = "No existen especialidades".PadRight(61);
            Console.WriteLine($"║ {mensaje} ║");
        }
        else
        {
            for (int i = 0; i < especialidad.listarEspecialidades().Count; i++)
            {
                Especialidad esp = especialidad.listarEspecialidades()[i];
                string nombreColum = esp.nombre.PadRight(61);
                Console.WriteLine($"║ {nombreColum} ║");
            }
        }
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("               Ingrese la nueva especialidad                     ");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        Console.Write("Especialidad: ");
        string nombreEsp = Console.ReadLine();
        Especialidad espe  = new Especialidad(nombreEsp);
        especialidad.guardar(espe);
        Console.ReadKey();
    }
    
    public static void CancelarCita() {

    }
    public static void ReprogramarCita() {

    }
    public static void AgendarCita() {

    }
    public static void ConsultarporMedico() {

    }
    public static void ConsultarporPaciente()
    {

    }
    public static void Salir() {

    }


}