using System;

namespace Entidades
{
    public class PatenteInvalidaException : Exception
    {
        /// <summary>
        /// Constructor que inicializa una instancia de la excepcion PatenteInvalidaException con un mensaje.
        /// </summary>
        /// <param name="message">Mensaje que describe la excepción.</param>
        public PatenteInvalidaException(string message) : base(message) { }
    }
}
