using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDIW.Servicios
{
    /// <summary>
    /// Implementación de la interfaz Menu
    /// </summary>
    class ImplMenu : InterfazMenu
    {
        public int Menu()
        {
            int opcion = -1;
            do
            {
                Console.WriteLine("\n\n\t\t\t╔════════════════════════════╗");
                Console.WriteLine("\t\t\t║   1) Select                ║");
                Console.WriteLine("\t\t\t║   2) Insert                ║");
                Console.WriteLine("\t\t\t║   3) Update                ║");
                Console.WriteLine("\t\t\t║   4) Delete                ║");
                Console.WriteLine("\t\t\t║                            ║");
                Console.WriteLine("\t\t\t║   0) Salir                 ║");
                Console.WriteLine("\t\t\t║                            ║");
                Console.WriteLine("\t\t\t╚════════════════════════════╝");
                Console.Write("\t\t\tIntroduzca una opción: ");
                opcion = Console.ReadKey().KeyChar - '0';

                if (opcion < 0 || opcion > 4)
                {
                    Console.Clear();
                    Console.WriteLine("\n\t\t\t** Error: El valor no está dentro del rango **");
                }
            } while (opcion < 0 || opcion > 4);

            return opcion;
        }
    }
}
