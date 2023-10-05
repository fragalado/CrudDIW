using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Servicios
{
    class ImplConexionSql : InterfazConexionSql
    {
        public NpgsqlConnection ConectaBD()
        {
            // Se lee la cadena de conexion a Postgresql del archivo de configuracion
            string stringConexionPostgresql = ConfigurationManager.ConnectionStrings["stringConexion"].ConnectionString;
            Console.WriteLine("\n\t[INFO-ImplConexionSql-ConectaBD] Cadena conexion: " + stringConexionPostgresql);

            NpgsqlConnection conexion = null;
            string estado = "";

            if (!string.IsNullOrWhiteSpace(stringConexionPostgresql))
            {
                try
                {
                    conexion = new NpgsqlConnection(stringConexionPostgresql);
                    conexion.Open();
                    // Se obtiene el estado de conexion para informarlo por consola
                    estado = conexion.State.ToString();

                    if (estado.Equals("Open"))
                        Console.WriteLine("\n\t[INFO-ImplConexionSql-ConectaBD] Estado conexion: " + estado);
                    else
                        conexion = null;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t[ERROR-ImplConexionSql-ConectaBD] Error al generar la conexion " + e.Message);
                    return conexion=null;
                }
            }

            return conexion;
        }

        public void DesconectaBD(NpgsqlConnection conexion)
        {
            try
            {
                conexion.Close();

                if(!conexion.State.ToString().Equals("Open"))
                    Console.WriteLine("\n\t[INFO-ImplConexionSql-DesconectaBD] Estado conexion: " + conexion.State.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t[ERROR-ImplConexionSql-DesconectaBD] Error al desconectar la conexion " + e.Message);
            }
        }
    }
}
