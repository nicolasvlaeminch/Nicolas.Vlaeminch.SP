using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivo
{
    public interface IArchivo
    {
        /// <summary>
        /// Metodo que guardar una lista de patentes.
        /// </summary>
        /// <param name="datos">Lista de patentes a guardar.</param>
        /// <returns>True si se guardaron los datos correctamente o False en caso contrario.</returns>
        bool Guardar(List<Patente> datos);

        /// <summary>
        /// Metodo para leer y obtener una lista de patentes.
        /// </summary>
        /// <returns>Lista de patentes obtenidas.</returns>
        List<Patente> Leer();

    }
}
