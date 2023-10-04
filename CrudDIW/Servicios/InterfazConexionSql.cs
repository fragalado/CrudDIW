using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Servicios
{
    /// <summary>
    /// Interfaz que define los métodos para generar conexiones a base de datos
    /// </summary>
    interface InterfazConexionSql
    {
        /// <summary>
        /// Método que genera la conexion a postgresql
        /// </summary>
        /// <returns>Devuelve la conexion a la base de datos</returns>
        NpgsqlConnection ConectaBD();

        /// <summary>
        /// Método que cierra la conexion a la base de datos
        /// </summary>
        /// <param name="conexion"></param>
        void DesconectaBD(NpgsqlConnection conexion);
    }
}
