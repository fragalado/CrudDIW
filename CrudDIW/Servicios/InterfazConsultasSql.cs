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

        void insertLibro(NpgsqlConnection conexion);

        void updateLibro(NpgsqlConnection conexion);

        void deleteLibro(NpgsqlConnection conexion);
    }
}
