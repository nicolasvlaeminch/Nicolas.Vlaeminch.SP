using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entidades
{
    public static class PatenteStringExtension
    {
        public const string patente_vieja = "^[A-Z]{3}[0-9]{3}$";
        public const string patente_mercosur = "^[A-Z]{2}[0-9]{3}[A-Z]{2}$";

        /// <summary>
        /// Metodo de extension que valida una cadena como patente y la convierte en un objeto Patente.
        /// </summary>
        /// <param name="str">La patente a validar.</param>
        /// <returns>Objeto Patente valido.</returns>
        /// <exception cref="PatenteInvalidaException">Se lanza cuando la cadena no cumple con el formato esperado.</exception>
        public static Patente ValidarPatente(this string str)
        {
            Regex rgx_v = new Regex(patente_vieja);
            Regex rgx_n = new Regex(patente_mercosur);

            if (rgx_v.IsMatch(str))
            {
                return new Patente(str, "Vieja");
            }
            else if (rgx_n.IsMatch(str))
            {
                return new Patente(str, "Mercosur");
            }
            else
            {
                throw new PatenteInvalidaException($"{str} no cumple el formato");
            }
        }
    }
}
