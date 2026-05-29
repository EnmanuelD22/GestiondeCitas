using GestiondeCitas.Datos;
using GestiondeCitas.Modelos;
using GestiondeCitas.Presentacion.UI;
using GestiondeCitas.Servicios;
using GestiondeCitas.Servicios;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

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
        string dni = CapturarCedula();
        string tel = CapturarTelefono();
        Paciente paciente = new Paciente(nombre, dni, tel);
        pacientes.Guardar(paciente);
        Console.WriteLine("Paciente agregado de manera exitosa.");
        Console.ReadKey();
    }
    public static void RegistrarMedico()
    {
        EspecialidadService especialidad = new EspecialidadService(especialidades);
        Console.Clear();
        Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("                    Ingrese la nueva Médico                      ");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();
        string dni = CapturarCedula();
        string tel = CapturarTelefono();
        string esp = validarEspecialidad(especialidad);
        Console.WriteLine("");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
        Console.ResetColor();
        Console.WriteLine("                        H O R A R I O                            ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        Console.ResetColor();


        TimeOnly horaInicio = CapturarHora("Hora de Entrada (Ej. 08:00 AM):");
        TimeOnly horaFin = CapturarHora("Hora de Salida (Ej. 05:00 PM):");
        Console.ReadKey();

        Medico medico = new Medico(nombre, dni, tel, esp, horaInicio, horaFin);




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
    public static void AgendarCita(Cita cita, Paciente paciente, Medico medico) {

    }
    public static void ConsultarporMedico() {

    }
    public static void ConsultarporPaciente()
    {

    }
    public static void Salir() {

    }

    public static string ExtraerNumeros(string entrada)
    {
        if (string.IsNullOrWhiteSpace(entrada)) return "";
        return Regex.Replace(entrada, @"\D+", "");
    }

    public static string CapturarCedula()
    {
        int lineaActual = Console.CursorTop;
        while (true)
        {
            Console.SetCursorPosition(0, lineaActual);
            Console.Write(new string(' ', Console.WindowWidth - 1));

            Console.SetCursorPosition(0, lineaActual);
            Console.Write("Dni: ");
            string entrada = Console.ReadLine();
            string numeros = ExtraerNumeros(entrada);

            if (numeros.Length == 11)
            {
                Console.SetCursorPosition(0, lineaActual + 1);
                Console.Write(new string(' ', Console.WindowWidth - 1));

                Console.SetCursorPosition(0, lineaActual + 1);
                return $"{numeros.Substring(0, 3)}-" +
                    $"{numeros.Substring(3, 7)}-{numeros.Substring(10, 1)}";
            }
            else
            {
                Console.Write(new string(' ', Console.WindowWidth - 1));
                Console.SetCursorPosition(0, lineaActual + 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error. Asegúrese de ingresar un dni válido ");
                Console.ResetColor();
            }
        }
    }

    public static string CapturarTelefono()
    {
        int lineaActual = Console.CursorTop;

       while (true)
        {
            Console.SetCursorPosition(0, lineaActual);
            Console.Write(new string(' ', Console.WindowWidth - 1));

            Console.SetCursorPosition(0, lineaActual);
            Console.Write("Tel: ");
            string entrada = Console.ReadLine();
            string numeros = ExtraerNumeros(entrada);

            if (numeros.Length == 10)
            {
                
                Console.Write(new string(' ', Console.WindowWidth - 1));
                Console.SetCursorPosition(0, lineaActual + 1);
                return $"+1({numeros.Substring(0, 3)})-{numeros.Substring(3, 3)}-" +
               $"{numeros.Substring(6, 4)}";
            } else
            {
               
                Console.Write(new string(' ', Console.WindowWidth - 1));
                Console.SetCursorPosition(0, lineaActual + 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error. Asegurese de insertar un telefono válido");
                Console.ResetColor();
            }
        }
    }

    public static string validarEspecialidad(EspecialidadService especialidad)
    {
        var lista = especialidad.listarEspecialidades();

        while (true)
        {
            Console.Write("Especialidad: ");
            string entrada = Console.ReadLine();


            for (int i = 0; i < lista.Count; i++)
            {
                Especialidad esp = lista[i];
                if (esp.nombre.Trim().Equals(entrada, StringComparison.OrdinalIgnoreCase)) {
                    return esp.nombre;
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error. Esta especialidad no existe.");
            Console.ResetColor();
        }
    }

    public static TimeOnly CapturarHora(string texto)
    {
        int lineaActual = Console.CursorTop;

        while (true)
        {
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, lineaActual);
            Console.Write(texto);
            string entrada = Console.ReadLine();

            if (TimeOnly.TryParse(entrada, out TimeOnly horaValidada))
            {
                Console.Write(new string(' ', Console.WindowWidth - 1));
                Console.SetCursorPosition(0, lineaActual + 1);
                return horaValidada;
            } else
            {
                Console.Write(new string(' ', Console.WindowWidth - 1));
                Console.SetCursorPosition(0, lineaActual + 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error. Ingresa una hora valida (Ej.8:00AM)");
                Console.ResetColor();
            }
        }
    }


}