using CrudDIW.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Servicios
{
    /// <summary>
    /// Interfaz que define los métodos que harán las consultas a la base de datos
    /// </summary>
    interface InterfazConsultasSql
    {
        /// <summary>
        /// Método que hace el select dependiendo si se quiere todos los libros o uno concreto
        /// Preguntara si se quiere hacer el select de todos los libros o de uno concreto
        /// </summary>
        /// <param name="conexion"></param>
        /// <returns></returns>
        List<LibroDto> selectLibro(NpgsqlConnection conexion);

        /// <summary>
        /// Método que hace el insert de uno o varios libros a la base de datos
        /// Pedirá los datos del libro y una vez que se termine se preguntara si se quiere añadir más libros
        /// </summary>
        /// <param name="conexion"></param>
        void insertLibro(NpgsqlConnection conexion);

        /// <summary>
        /// Método que hace el update de un libro a la base de datos.
        /// Preguntará el isbn del libro a modificar y después preguntará los nuevos datos del libro.
        /// </summary>
        /// <param name="conexion"></param>
        void updateLibro(NpgsqlConnection conexion);

        /// <summary>
        /// Método que hace el delete de un libro a la base de datos.
        /// Preguntará el isbn del libro a eliminar y después preguntará si se está seguro de eliminar
        /// </summary>
        /// <param name="conexion"></param>
        void deleteLibro(NpgsqlConnection conexion);
    }
}
