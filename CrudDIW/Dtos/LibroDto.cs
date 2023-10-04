using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Dtos
{
    class LibroDto
    {
        // Atributos

        long idLibro;
        String titulo, autor, isbn;
        int edicion;

        // Constructores -> Constructor con todos los parámetros, el constructor vacío no existe

        public LibroDto(long idLibro, string titulo, string autor, string isbn, int edicion)
        {
            this.idLibro = idLibro;
            this.titulo = titulo;
            this.autor = autor;
            this.isbn = isbn;
            this.edicion = edicion;
        }

        // Getters

        public long IdLibro { get => idLibro;}
        public string Titulo { get => titulo;}
        public string Autor { get => autor;}
        public string Isbn { get => isbn;}
        public int Edicion { get => edicion;}
    }
}
