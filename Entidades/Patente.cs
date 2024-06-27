using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Enum de los tipos de patente.
    /// </summary>
    public enum Tipo
    {
        Vieja,
        Mercosur
    }

    public class Patente
    {

        private string _codigoPatente;
        private string _tipoCodigo;

        public Patente() { }

        public Patente(string codigoPatente, string tipoCodigo)
        {
            _codigoPatente = codigoPatente;
            _tipoCodigo = tipoCodigo;
        }

        /// <summary>
        /// Obtiene y establece el tipo de patente.
        /// </summary>
        public string CodigoPatente
        {
            get { return _codigoPatente; }
            set { _codigoPatente = value; }
        }

        /// <summary>
        /// Obtiene y establece el tipo de codigo.
        /// </summary>
        public string TipoCodigo
        {
            get { return _tipoCodigo; }
            set { _tipoCodigo = value; }
        }

        /// <summary>
        /// Sobrescritura del metodo ToString para obtener el codigo de la patente.
        /// </summary>
        /// <returns>Codigo de la patente.</returns>
        public override string ToString()
        {
            return _codigoPatente;
        }
    }
}
