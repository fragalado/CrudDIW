using CrudDIW.Dtos;
using CrudDIW.Util;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Servicios
{
    /// <summary>
    /// Implementación de la interfaz consultas base de datos
    /// </summary>
    class ImplConsultasSql : InterfazConsultasSql
    {
        public List<LibroDto> selectLibro(NpgsqlConnection conexion)
        {
            ADto aDto = new ADto(); // Inicializamos ADto para poder transformar de resulset a lista
            List<LibroDto> listaLibros = new List<LibroDto>(); // Lista donde guardaremos los libros

            NpgsqlCommand consulta = null;
            try
            {
                // Preguntamos si se quiere sacar todos los libros o uno
                if (PreguntaSiNo("Quieres sacar todos los libros"))
                    consulta = new NpgsqlCommand("SELECT * FROM \"gbp_almacen\".\"gbp_alm_cat_libros\"", conexion); // Sacamos todos los libros
                else
                {
                    // Preguntamos por el isbn del libro
                    Console.Write("\n\tIntroduzca el isbn del libro: ");

                    consulta = new NpgsqlCommand("SELECT * FROM \"gbp_almacen\".\"gbp_alm_cat_libros\" WHERE isbn=@isbn", conexion);
                    consulta.Parameters.AddWithValue("@isbn", Console.ReadLine());
                }

                NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();

                // Paso de DataReader a lista de LibroDto
                listaLibros = aDto.readerALibroDto(resultadoConsulta);

                // Mostramos por consola el numero de libros
                Console.WriteLine("\n\t[INFO-ImplConsultasSql-selectLibro] Número de libros: " + listaLibros.Count);

                // Cerramos la conexion y el resultadoConsulta
                conexion.Close();
                resultadoConsulta.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t[ERROR-ImplConsultasSql-SelectLibro] Error " + e.Message);
            }

            return listaLibros;
        }

        public void insertLibro(NpgsqlConnection conexion)
        { // TODO
            // Lista para guardar los libros
            List<LibroDto> listaLibros = new List<LibroDto>();

            NpgsqlCommand declaracion = null;
            try
            {
                // Pediremos datos hasta que se cancele
                // Variables donde guardaremos los datos
                string titulo, autor, isbn;
                int edicion;

                do
                {
                    // Limpiamos la consola
                    Console.Clear();

                    // Pedimos el titulo
                    Console.Write("\n\tIntroduzca el titulo del libro: ");
                    titulo = Console.ReadLine();

                    // Pedimos el autor
                    Console.Write("\n\tIntroduzca el autor del libro: ");
                    autor = Console.ReadLine();

                    // Pedimos el isbn
                    Console.Write("\n\tIntroduzca el isbn del libro: ");
                    isbn = Console.ReadLine();

                    // Pedimos la edicion
                    Console.Write("\n\tIntroduzca la edicion del libro: ");
                    edicion = Convert.ToInt32(Console.ReadLine());

                    // Lo añadimos a la lista
                    listaLibros.Add(new LibroDto(0, titulo, autor, isbn, edicion));

                } while (PreguntaSiNo("Quieres seguir"));

                // Ahora tendremos que hacer el insert de los libros de la lista
                // Recorremos la lista
                foreach (LibroDto aux in listaLibros)
                {
                    declaracion = new NpgsqlCommand("INSERT INTO gbp_almacen.gbp_alm_cat_libros (titulo, autor, isbn, edicion) VALUES (@titulo, @autor, @isbn, @edicion);", conexion);
                    declaracion.Parameters.AddWithValue("@titulo", aux.Titulo);
                    declaracion.Parameters.AddWithValue("@autor", aux.Autor);
                    declaracion.Parameters.AddWithValue("@isbn", aux.Isbn);
                    declaracion.Parameters.AddWithValue("@edicion", aux.Edicion);
                }

                // Hacemos el commit
                int filasAfectadas = declaracion.ExecuteNonQuery();
                if(filasAfectadas > -1)
                    Console.WriteLine("\n\t[INFO-ImplConsultasSql-insertLibro] Insert ha funcionado");

                // Cerramos la conexion
                conexion.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine("\n\t[ERROR-ImplConsultasSql-insertLibro] Error " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t[ERROR-ImplConsultasSql-insertLibro] Error " + e.Message);
            }
        }

        public void deleteLibro(NpgsqlConnection conexion)
        {
            NpgsqlCommand declaracion = null;
            try
            {
                string isbn;

                // Limpiamos la consola
                Console.Clear();

                // Pedimos el isbn
                Console.Write("\n\tIntroduzca el isbn del libro a eliminar: ");
                isbn = Console.ReadLine();

                // Preguntaremos ahora si se esta seguro de eliminar
                if(PreguntaSiNo("Seguro que quieres eliminar"))
                {
                    // Si se quiere eliminar haremos la query y el execute
                    declaracion = new NpgsqlCommand("DELETE FROM gbp_almacen.gbp_alm_cat_libros WHERE ISBN=@isbn", conexion);
                    declaracion.Parameters.AddWithValue("@isbn", isbn);

                    int filasAfectadas = declaracion.ExecuteNonQuery();
                    if (filasAfectadas > -1)
                        Console.WriteLine("\n\t[INFO-ImplConsultasSql-deleteLibro] Delete ha funcionado");
                }                

                // Cerramos la conexion
                conexion.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine("\n\t[ERROR-ImplConsultasSql-deleteLibro] Error " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t[ERROR-ImplConsultasSql-deleteLibro] Error " + e.Message);
            }
        }

        

        public void updateLibro(NpgsqlConnection conexion)
        {
            throw new NotImplementedException();
        }

        private Boolean PreguntaSiNo(string txt)
        {
            String opcion;

            do
            {
                Console.Write("\n\t¿{0}? [s=Si/n=No]: ", txt);
                opcion = Console.ReadKey().KeyChar.ToString();

                if (opcion.Equals("s") || opcion.Equals("S"))
                    return true;
                else if (opcion.Equals("n") || opcion.Equals("N"))
                    return false;
            } while (true);
        }
    }
}
