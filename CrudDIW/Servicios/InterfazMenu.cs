using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Servicios
{
    /// <summary>
    /// Interfaz que define los métodos que darán servicio a Menu
    /// </summary>
    interface InterfazMenu
    {
        /// <summary>
        /// Método que muestra el menu por consola y devuelve la opción elegida
        /// </summary>
        /// <returns>La opción elegida</returns>
        int Menu();
    }
}
