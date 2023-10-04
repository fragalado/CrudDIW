using CrudDIW.Dtos;
using CrudDIW.Util;
using Npgsql;
using System;
using System.Collections.Generic;
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
                    Console.Write("Introduzca el isbn del libro: ");

                    consulta = new NpgsqlCommand("SELECT * FROM \"gbp_almacen\".\"gbp_alm_cat_libros\" WHERE isbn=@isbn", conexion);
                    consulta.Parameters.AddWithValue("@isbn", Console.ReadLine());
                }

                NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();

                // Paso de DataReader a lista de LibroDto
                listaLibros = aDto.readerALibroDto(resultadoConsulta);

                // Mostramos por consola el numero de libros
                Console.WriteLine("[INFO-ImplConsultasSql-selectLibro] Número de libros: " + listaLibros.Count);

                // Cerramos la conexion y el resultadoConsulta
                conexion.Close();
                resultadoConsulta.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR-ImplConsultasSql-SelectLibro] Error " + e);
            }

            return listaLibros;
        }

        public void deleteLibro(NpgsqlConnection conexion)
        {
            throw new NotImplementedException();
        }

        public void insertLibro(NpgsqlConnection conexion)
        {
            throw new NotImplementedException();
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
                Console.Write("¿{0}? [s=Si/n=No]: ",txt);
                opcion = Console.ReadKey().KeyChar.ToString();

                if (opcion.Equals("s") || opcion.Equals("S"))
                    return true;
                else if (opcion.Equals("n") || opcion.Equals("N"))
                    return false;
            } while (true);
        }
    }
}
