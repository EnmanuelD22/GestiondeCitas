using GestiondeCitas.Datos;
using GestiondeCitas.Interfaces;
using GestiondeCitas.Modelos;
using GestiondeCitas.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace GestiondeCitas.Presentacion.UI
{
    public class SistemaUI
    {
        private readonly DatosCitas citas = new DatosCitas();
        private readonly DatosEspecialidades especialidades = new DatosEspecialidades();
        private readonly DatosMedicos medicos = new DatosMedicos();
        private readonly DatosPacientes pacientes = new DatosPacientes();

        private NavegacionUI navegacion = new NavegacionUI();
        private EspecialidadService especialidadS;
        private MedicoServicios medicoS;
        private CitaService citaS;
        private PacienteServicios pacienteS;
        private INotificacion notificacion;
        public SistemaUI()
        {
            notificacion = new NotificacionEmail();
            especialidadS = new EspecialidadService(especialidades);
            medicoS = new MedicoServicios(medicos);
            citaS = new CitaService(citas);
            pacienteS = new PacienteServicios(pacientes);
        }

        public void Iniciar()
        {
            string[] opcionesMenu = {
            "Registrar Nuevo Paciente",
            "Registrar Nuevo Médico",
            "Gestión de Especialidades",
            "Agendar Nueva Cita",
            "Consultar Citas por Medico",
            "Consultar Citas por Paciente",
            "Cancelar una Cita",
            "Reprogramar Cita",
            "Enviar Recordatorio",
            "[Salir del Sistema]" };

            bool ejecutando = true;

            while (ejecutando)
            {
                int seleccion = navegacion.MostrarMenu("M E D I A P P", "GESTION DE CITAS", opcionesMenu);

                switch (seleccion)
                {
                    case 0: RegistrarPaciente(); break;
                    case 1: RegistrarMedico(); break;
                    case 2: GestionarEspecialidad(especialidadS); break;
                    case 3: AgendarCita(); break;
                    case 4: ConsultarporMedico(); break;
                    case 5: ConsultarporPaciente(); break;
                    case 6: CancelarCita(); break;
                    case 7: ReprogramarCita(); break;
                    case 8: EnviarRecordatorio(); break;
                    case 9:
                        Console.Write("Seguro que quieres salir(s/n): ");
                        char entrada = Console.ReadLine().ToLower()[0];

                        if (entrada.Equals('s'))
                        {
                            Console.WriteLine("Cerrando sistema.......");
                            ejecutando = false;
                            Console.Clear();
                        }
                        else if (entrada.Equals('n'))
                        {

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nError seleccion fuera de rango.");
                            Console.ResetColor();
                        }
                        break;
                }
            }

        }

        private void EnviarRecordatorio()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                          Enviar Recordatorio                    ");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
            Console.Write("Ingrese un email: ");
            string email = CapturarEmail();

            var citaEncotrada = citas.mostrar().FirstOrDefault(c =>
            c.paciente.email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (citaEncotrada == null) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo existe paciente registrado con este email.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }   
            string mensaje = $"\nRecuerda tu cita con el Dr.{citaEncotrada.medico.nombre}" +
                                $"\nPara el {citaEncotrada.fecha} a las {citaEncotrada.hora}";
            notificacion.enviar(email, mensaje);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nRecordatorio enviado exitosamente.");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void RegistrarPaciente()
        {

            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                          Nuevo paciente                         ");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            string dni = CapturarCedula();
            string tel = CapturarTelefono();
            string email = CapturarEmail();
            Paciente paciente = new Paciente(nombre, dni, tel, email);
            pacienteS.registrar(paciente);
            Console.ReadKey();
        }
        private void RegistrarMedico()
        {
            EspecialidadService especialidad = new EspecialidadService(especialidades);
            MedicoServicios medi = new MedicoServicios(medicos);
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                           Nuevo Médico                          ");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            string dni = CapturarCedula();
            string tel = CapturarTelefono();
            Especialidad esp = validarEspecialidad(especialidad);
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

            Medico dr = new Medico(nombre, dni, tel,  esp, horaInicio, horaFin);
            medi.Registrar(dr);
            Console.ReadKey();
        }
        private void GestionarEspecialidad(EspecialidadService especialidad)
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
            Especialidad espe = new Especialidad(nombreEsp);
            especialidad.guardar(espe);
            Console.ReadKey();
        }

        private void CancelarCita()
        {
            CitaService cita = new CitaService(citas);
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                         Cancelar Cita                           ");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
            Console.Write("Ingrese el id de la cita: ");
            int id = int.Parse(Console.ReadLine());

            if (!int.TryParse(Console.ReadLine(), out int num))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError. Debe ingrese un numero valido.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nCita reprogramada de manera exitosa.");
            Console.ResetColor();
            Console.ReadKey();

            cita.Cancelar(id);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Se ha cancelado la cita correctamente.");
            Console.ResetColor();
            Console.ReadKey();
        }
        private void ReprogramarCita()
        {
            CitaService cita = new CitaService(citas);
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                        Reprogramar Cita                         ");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
            Console.Write("Ingrese el id de la cita: ");
            int id = int.Parse(Console.ReadLine());
            DateOnly fechaNueva = CapturarFecha();
            TimeOnly horaNueva = CapturarHora("Hora (Ej.10:00 AM): ");
            cita.Reprogramar(id, fechaNueva, horaNueva);
            Console.ReadKey();
        }
        private void AgendarCita()
        {
            CitaService cita = new CitaService(citas);

            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                    AGENDAR NUEVA CITA                           ");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- D A T O S   P A C I E N T E ---");
            Console.ResetColor();
            string dnipaciente = CapturarCedula();
            var paciente = pacientes.mostrar().FirstOrDefault(p => p.dni == dnipaciente);

            if (paciente != null)
            {
                Console.WriteLine($"Nombre: {paciente.nombre}");
                Console.WriteLine($"Tel: {paciente.telefono}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError. No existe ningun paciente con este DNI.");
                Console.ResetColor();

                Console.ReadKey();
                return;
            }


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- D A T O S   M E D I C O ---");
            Console.ResetColor();
            string dnimedico = CapturarCedula();
            var medi = medicos.mostrar().FirstOrDefault(m => m.dni == dnimedico);

            if (medi != null)
            {
                Console.WriteLine($"Nombre: {medi.nombre}");
                Console.WriteLine($"Tel: {medi.telefono}");
                Console.WriteLine($"Especialidad: {medi.especialidad}");
                Console.WriteLine($"Horario {medi.Horainicio}-{medi.Horafin}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError. No existe ningun medico con este DNI.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- F E C H A   C I T A  ---");
            Console.ResetColor();
            DateOnly fechaCita = CapturarFecha();
            TimeOnly horaCita = CapturarHora("Hora (Ej.10:00 AM): ");

            Cita nuevaCita = new Cita(medi, paciente, fechaCita, horaCita);
            cita.Registrarcita(nuevaCita);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nLa cita se ha creado de manera exitosa.");
            Console.ResetColor();

            Console.WriteLine("\nPresione cualquier tecla para volver al menu");
            Console.ReadKey();
        }
        private void ConsultarporMedico()
        {
            CitaService cita = new CitaService(citas);
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                     Consultar por Medico                        ");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
            Console.Write("Ingrese el DNI del medico: ");
            string dni = CapturarCedula();
            cita.ConsultarPorMedico(dni);
            Console.ReadKey();
        }
        private void ConsultarporPaciente()
        {
            CitaService cita = new CitaService(citas);
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                     Consultar por Paciente                      ");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
            Console.Write("Ingrese el DNI del paciente: ");
            string dni = CapturarCedula();
            cita.ConsultarPorPaciente(dni);
            Console.ReadKey();
        }

        private static string ExtraerNumeros(string entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada)) return "";
            return Regex.Replace(entrada, @"\D+", "");
        }

        private static string CapturarCedula()
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

                    Console.SetCursorPosition(0, lineaActual + 1);
                    return $"{numeros.Substring(0, 3)}-" +
                        $"{numeros.Substring(3, 7)}-{numeros.Substring(10, 1)}";
                }
                else
                {
                    Console.SetCursorPosition(0, lineaActual + 1);
                    Console.Write(new string(' ', Console.WindowWidth - 1));
                    Console.SetCursorPosition(0, lineaActual + 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError. Asegúrese de ingresar un dni válido ");
                    Console.ResetColor();
                }
            }
        }

        private static string CapturarTelefono()
        {
            int lineaActual = Console.CursorTop;

            while (true)
            {
                Console.SetCursorPosition(0, lineaActual + 1);
                Console.SetCursorPosition(0, lineaActual);
                Console.Write(new string(' ', Console.WindowWidth - 1));


                Console.SetCursorPosition(0, lineaActual);
                Console.Write("Tel: ");
                string entrada = Console.ReadLine();
                string numeros = ExtraerNumeros(entrada);

                if (numeros.Length == 10)
                {
                    Console.SetCursorPosition(0, lineaActual + 1);
                    Console.Write(new string(' ', Console.WindowWidth - 1));
                    Console.SetCursorPosition(0, lineaActual + 1);
                    return $"+1({numeros.Substring(0, 3)})-{numeros.Substring(3, 3)}-" +
                   $"{numeros.Substring(6, 4)}";
                }
                else
                {
                    Console.SetCursorPosition(0, lineaActual + 1);
                    Console.Write(new string(' ', Console.WindowWidth - 1));
                    Console.SetCursorPosition(0, lineaActual + 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError. Asegurese de insertar un telefono válido");
                    Console.ResetColor();
                }
            }
        }

        private static Especialidad validarEspecialidad(EspecialidadService especialidad)
        {
            var lista = especialidad.listarEspecialidades();
            int lineaActual = Console.CursorTop;

            while (true)
            {
                Console.SetCursorPosition(0, lineaActual);
                Console.Write(new string(' ', Console.WindowWidth - 1));

                Console.SetCursorPosition(0, lineaActual);
                Console.Write("Especialidad: ");
                string entrada = Console.ReadLine();


                for (int i = 0; i < lista.Count; i++)
                {
                    Especialidad esp = lista[i];
                    if (esp.nombre.Trim().Equals(entrada, StringComparison.OrdinalIgnoreCase))
                    {

                        Console.SetCursorPosition(0, lineaActual + 1);
                        Console.Write(new string(' ', Console.WindowWidth - 1));
                        return esp;
                    }

                    Console.SetCursorPosition(0, lineaActual + 1);
                    Console.Write(new string(' ', Console.WindowWidth - 1));
                }



                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError. Esta especialidad no existe.");
                Console.ResetColor();
            }
        }

        private static TimeOnly CapturarHora(string texto)
        {
            int lineaActual = Console.CursorTop;

            while (true)
            {
                Console.SetCursorPosition(0, lineaActual);
                Console.Write(new string(' ', Console.WindowWidth - 1));

                Console.SetCursorPosition(0, lineaActual);
                Console.Write(texto);
                string entrada = Console.ReadLine();

                if (TimeOnly.TryParse(entrada, out TimeOnly horaValidada))
                {
                    Console.SetCursorPosition(0, lineaActual + 1);
                    Console.Write(new string(' ', Console.WindowWidth - 1));

                    return horaValidada;
                }
                else
                {
                    Console.SetCursorPosition(0, lineaActual + 1);
                    Console.Write(new string(' ', Console.WindowWidth - 1));

                    Console.SetCursorPosition(0, lineaActual + 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nError. Ingresa una hora valida (Ej.8:00AM)");
                    Console.ResetColor();
                }
            }
        }

        public static DateOnly CapturarFecha()
        {
            int indiceActual = Console.CursorTop;

            while (true)
            {
                Console.SetCursorPosition(0, indiceActual);
                Console.Write(new string(' ', Console.WindowWidth - 1));

                Console.SetCursorPosition(0, indiceActual);
                Console.Write("Fecha (DD/MM/YYYY): ");
                string entrada = Console.ReadLine();


                if (DateOnly.TryParse(entrada, out DateOnly fechaValidada))
                {

                    DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Now);

                    if (fechaValidada > fechaActual)
                    {
                        Console.SetCursorPosition(0, indiceActual + 1);
                        Console.Write(new string(' ', Console.WindowWidth - 1));
                        return fechaValidada;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, indiceActual + 1);
                        Console.Write(new string(' ', Console.WindowWidth - 1));

                        Console.SetCursorPosition(0, indiceActual + 1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nError. Ingresa una hora valida (Ej.8:00AM)");
                        Console.ResetColor();
                    }

                }
                else
                {
                    Console.SetCursorPosition(0, indiceActual + 1);
                    Console.Write(new string(' ', Console.WindowWidth - 1));

                    Console.SetCursorPosition(0, indiceActual + 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\nError. Ingresa una fecha válida (Ej. 25/10/2026)");
                    Console.ResetColor();
                }
            }
        }

        private string CapturarEmail()
        {
            int indiceActual = Console.CursorTop;

            while (true)
            {
                Console.SetCursorPosition(0, indiceActual);
                Console.Write(new string(' ', Console.WindowWidth - 1));

                Console.SetCursorPosition(0, indiceActual);
                Console.Write("Email: ");
                string entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada) &&
                    entrada.Contains("@") && entrada.Contains("."))
                {
                    Console.SetCursorPosition(0, indiceActual + 1);
                    Console.Write(new string(' ', Console.WindowWidth - 1));
                    return entrada;
                }
                else
                {
                    Console.SetCursorPosition(0, indiceActual + 1);
                    Console.Write(new string(' ', Console.WindowWidth - 1));

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError. Debe agregar un email valido.");
                    Console.ResetColor();
                }
            }
        }
    }
    
}
