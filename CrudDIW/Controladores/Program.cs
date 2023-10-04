using CrudDIW.Dtos;
using CrudDIW.Servicios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW
{
    /// <summary>
    /// Clase principal de la aplicacion
    /// </summary>
    class Program
    {
        /// <summary>
        /// Método main de la aplicacion, puerta de entrada
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Inicializamos la interfaz para conectar base de datos
            InterfazConexionSql conexionSql = new ImplConexionSql();

            // Inicializamos la interfaz para hacer consultas base de datos
            InterfazConsultasSql consultasSql = new ImplConsultasSql();

            // Inicializamos la interfaz menu
            InterfazMenu intM = new ImplMenu();

            List<LibroDto> listaLibros = new List<LibroDto>();

            NpgsqlConnection conexion = conexionSql.ConectaBD();
            
            if(conexion != null)
            {
                int opcion;
                do
                {
                    opcion = -1;
                    try
                    {
                        opcion = intM.Menu();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[ERROR-Program-Main] No se ha introducido una opción correcta " + e);
                    }

                    switch (opcion) 
                    {
                        case 1:
                            // Select
                            try
                            {
                                listaLibros = consultasSql.selectLibro(conexion);

                                foreach (LibroDto aux in listaLibros)
                                {
                                    Console.WriteLine("Id = {0}, Titulo = {1}, Autor = {2}, Isbn = {3}, Edicion = {4}", aux.IdLibro
                                                                                                                      , aux.Titulo
                                                                                                                      , aux.Autor
                                                                                                                      , aux.Isbn
                                                                                                                      , aux.Edicion);
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("[ERROR-Program-Main] Error no se ha podido hacer el select");
                            }
                            break;
                        case 2:
                            // Insert
                            break;
                        case 3:
                            // Update
                            break;
                        case 4:
                            // Delete
                            break;
                    }

                    // Para volver al menu
                    if(opcion != 0)
                    {
                        Console.WriteLine("\n\tPulse una tecla para volver al menu");
                        Console.ReadKey(true);
                    }

                    Console.Clear();
                } while (opcion != 0);
            }
            else
                Console.WriteLine("[ERROR-Program-Main] Error la conexion no esta abierta");

            // Para salir
            Console.WriteLine("\n\n\tPulse una tecla para salir");
            Console.ReadKey();
        }
    }
}
