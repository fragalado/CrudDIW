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

            NpgsqlConnection conexion = null;
            
            int opcion;
            bool estado = false;
            do
            {
                // Conectamos a la base de datos
                try
                {
                    conexion = conexionSql.ConectaBD();
                    estado = conexion.State.ToString().Equals("Open");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t[ERROR-Program-Main] Error conexión no abierta: " + e.Message);
                }

                // Si la conexion esta abierta mostraremos el menu
                // Si esta cerrada no mostraremos el menu
                opcion = -1;
                if (estado)
                {
                    try
                    {
                        opcion = intM.Menu();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\n\t[ERROR-Program-Main] No se ha introducido una opción correcta " + e.Message);
                    }

                    Console.Clear(); // Limpiamos la consola
                    switch (opcion)
                    {
                        case 1:
                            // Select
                            try
                            {
                                Console.Clear();
                                listaLibros = consultasSql.selectLibro(conexion);

                                foreach (LibroDto aux in listaLibros)
                                {
                                    Console.WriteLine("\tId = {0}, Titulo = {1}, Autor = {2}, Isbn = {3}, Edicion = {4}", aux.IdLibro
                                                                                                                        , aux.Titulo
                                                                                                                        , aux.Autor
                                                                                                                        , aux.Isbn
                                                                                                                        , aux.Edicion);
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\n\t[ERROR-Program-Main] Error no se ha podido hacer el select");
                            }
                            break;
                        case 2:
                            // Insert
                            try
                            {
                                consultasSql.insertLibro(conexion);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\n\t[ERROR-Program-Main] Error no se ha podido hacer el insert");
                            }
                            break;
                        case 3:
                            // Update
                            try
                            {
                                consultasSql.updateLibro(conexion);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\n\t[ERROR-Program-Main] Error no se ha podido hacer el update");
                            }
                            break;
                        case 4:
                            // Delete
                            try
                            {
                                consultasSql.deleteLibro(conexion);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\n\t[ERROR-Program-Main] Error no se ha podido hacer el delete");
                            }
                            break;
                    }
                }

                // Para volver al menu
                if(opcion != 0 && estado)
                {
                    Console.WriteLine("\n\tPulse una tecla para volver al menu");
                    Console.ReadKey(true);
                }

                Console.Clear();
            } while (opcion != 0 && estado);

            // Para salir
            Console.WriteLine("\n\n\tPulse una tecla para salir");
            Console.ReadKey();
        }
    }
}
