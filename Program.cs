using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int cantidadEstudiantes, cantidadMaterias;
            string[] nombresEstudiantes; // Vector para nombres
            decimal[,] notas; // Matriz para notas [estudiantes][materias]
            decimal[] promedios; // Vector para promedios

            // Entrada de datos con validación mejorada
            Console.WriteLine("=== SISTEMA DE GESTIÓN DE NOTAS ACADÉMICAS ===\n");

            do
            {
                Console.WriteLine("Ingrese la cantidad de estudiantes (1-10):");
                string input = Console.ReadLine() ?? "0";
                if (int.TryParse(input, out cantidadEstudiantes) && cantidadEstudiantes >= 1 && cantidadEstudiantes <= 10)
                    break;
                Console.WriteLine("Error: Ingrese un número válido entre 1 y 10.");
            } while (true);

            do
            {
                Console.WriteLine("Ingrese la cantidad de materias (1-5):");
                string input = Console.ReadLine() ?? "0";
                if (int.TryParse(input, out cantidadMaterias) && cantidadMaterias >= 1 && cantidadMaterias <= 5)
                    break;
                Console.WriteLine("Error: Ingrese un número válido entre 1 y 5.");
            } while (true);

            // Inicialización de vectores y matriz
            nombresEstudiantes = new string[cantidadEstudiantes];
            notas = new decimal[cantidadEstudiantes, cantidadMaterias];
            promedios = new decimal[cantidadEstudiantes];

            // Carga de datos de estudiantes con validación
            for (int i = 0; i < cantidadEstudiantes; i++)
            {
                Console.WriteLine($"\n--- Estudiante {i + 1} ---");
                
                do
                {
                    Console.WriteLine($"Ingrese el nombre del estudiante {i + 1}:");
                    nombresEstudiantes[i] = Console.ReadLine()?.Trim() ?? "";
                    if (!string.IsNullOrEmpty(nombresEstudiantes[i]))
                        break;
                    Console.WriteLine("Error: El nombre no puede estar vacío.");
                } while (true);

                // Carga de notas para cada materia con validación
                for (int j = 0; j < cantidadMaterias; j++)
                {
                    do
                    {
                        Console.WriteLine($"Ingrese nota {j + 1} de {nombresEstudiantes[i]} (0-10):");
                        string notaInput = Console.ReadLine() ?? "0";
                        if (decimal.TryParse(notaInput, out decimal nota) && nota >= 0 && nota <= 10)
                        {
                            notas[i, j] = nota;
                            break;
                        }
                        Console.WriteLine("Error: Ingrese una nota válida entre 0 y 10.");
                    } while (true);
                }
            }

            // Cálculo de promedios
            for (int i = 0; i < cantidadEstudiantes; i++)
            {
                decimal suma = 0;
                for (int j = 0; j < cantidadMaterias; j++)
                {
                    suma += notas[i, j];
                }
                promedios[i] = suma / cantidadMaterias;
            }

            // Variables para estadísticas
            int contadorAprobados = 0;
            int contadorRecuperan = 0;
            int contadorReprobados = 0;
            decimal mejorPromedio = promedios[0];
            decimal peorPromedio = promedios[0];
            string estudianteMejorPromedio = nombresEstudiantes[0];
            string estudiantePeorPromedio = nombresEstudiantes[0];

            // Mostrar resultados con formato mejorado
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("RESULTADOS ACADÉMICOS");
            Console.WriteLine(new string('=', 60));

            for (int i = 0; i < cantidadEstudiantes; i++)
            {
                string situacion = "";

                // Determinar situación académica
                if (promedios[i] >= 6.0m)
                {
                    // Verificar si tiene alguna nota menor a 4
                    bool tieneNotaBaja = false;
                    for (int j = 0; j < cantidadMaterias; j++)
                    {
                        if (notas[i, j] < 4)
                        {
                            tieneNotaBaja = true;
                            break;
                        }
                    }

                    if (!tieneNotaBaja)
                    {
                        situacion = "Aprobado";
                        contadorAprobados++;
                    }
                    else
                    {
                        situacion = "Recupera";
                        contadorRecuperan++;
                    }
                }
                else if (promedios[i] >= 4.0m)
                {
                    situacion = "Recupera";
                    contadorRecuperan++;
                }
                else
                {
                    situacion = "Reprobado";
                    contadorReprobados++;
                }

                // Buscar mejor y peor promedio
                if (promedios[i] > mejorPromedio)
                {
                    mejorPromedio = promedios[i];
                    estudianteMejorPromedio = nombresEstudiantes[i];
                }
                
                if (promedios[i] < peorPromedio)
                {
                    peorPromedio = promedios[i];
                    estudiantePeorPromedio = nombresEstudiantes[i];
                }

                // Mostrar notas individuales
                Console.Write($"{nombresEstudiantes[i],-20} | Notas: ");
                for (int j = 0; j < cantidadMaterias; j++)
                {
                    Console.Write($"{notas[i, j]:F1} ");
                }
                Console.WriteLine($"| Promedio: {promedios[i]:F2} | {situacion}");
            }

            // Cálculo de porcentajes
            decimal porcentajeAprobados = (decimal)contadorAprobados / cantidadEstudiantes * 100;
            decimal porcentajeRecuperan = (decimal)contadorRecuperan / cantidadEstudiantes * 100;
            decimal porcentajeReprobados = (decimal)contadorReprobados / cantidadEstudiantes * 100;

            // Calcular promedio general del curso
            decimal sumaPromedios = 0;
            for (int i = 0; i < cantidadEstudiantes; i++)
            {
                sumaPromedios += promedios[i];
            }
            decimal promedioGeneral = sumaPromedios / cantidadEstudiantes;

            // Mostrar estadísticas finales mejoradas
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ESTADÍSTICAS DEL CURSO");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"Promedio general del curso: {promedioGeneral:F2}");
            Console.WriteLine($"Estudiante con mejor promedio: {estudianteMejorPromedio} ({mejorPromedio:F2})");
            Console.WriteLine($"Estudiante con menor promedio: {estudiantePeorPromedio} ({peorPromedio:F2})");
            Console.WriteLine($"\nDistribución de estudiantes:");
            Console.WriteLine($"  • Aprobados: {contadorAprobados} ({porcentajeAprobados:F1}%)");
            Console.WriteLine($"  • Recuperan: {contadorRecuperan} ({porcentajeRecuperan:F1}%)");
            Console.WriteLine($"  • Reprobados: {contadorReprobados} ({porcentajeReprobados:F1}%)");

            // Pausa antes de cerrar
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
