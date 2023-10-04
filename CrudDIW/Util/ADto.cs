using CrudDIW.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Util
{
    /// <summary>
    /// Métodos que pasan a objeto de tipo DTO
    /// </summary>
    class ADto
    {
        public List<LibroDto> readerALibroDto(NpgsqlDataReader resultadoConsulta)
        {
            // Lista donde guardaremos los libros
            List<LibroDto> listaLibros = new List<LibroDto>();

            // Recorremos el ResultSet y vamos añadiendo los libros a la lista
            while(resultadoConsulta.Read())
            {
                listaLibros.Add(new LibroDto(long.Parse(resultadoConsulta[0].ToString()),
                                resultadoConsulta[1].ToString(),
                                resultadoConsulta[2].ToString(),
                                resultadoConsulta[3].ToString(),
                                Convert.ToInt32(resultadoConsulta[4].ToString())));
            }

            // Devolvemos la lista con los libros
            return listaLibros;
        }
    }
}
