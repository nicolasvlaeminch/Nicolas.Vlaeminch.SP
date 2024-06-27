using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Archivo
{
    public class Xml : IArchivo
    {
        private string filePath;

        /// <summary>
        /// Inicializa una instancia de la clase Xml y añade la ruta del archivo.
        /// </summary>
        public Xml()
        {
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "patentes.xml");
        }

        /// <summary>
        /// Guarda una lista de patentes en un XML en la ruta especificada.
        /// </summary>
        /// <param name="datos">Lista de patentes a guardar.</param>
        /// <returns>Dara True si se guardo con exito o False si hubo un error.</returns>
        public bool Guardar(List<Patente> nuevasPatentes)
        {
            try
            {
                // Leer patentes existentes
                List<Patente> patentesActuales = Leer();

                // Agregar nuevas patentes a la lista existente
                patentesActuales.AddRange(nuevasPatentes);

                // Serializar y guardar la lista actualizada
                XmlSerializer serializer = new XmlSerializer(typeof(List<Patente>));
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    serializer.Serialize(sw, patentesActuales);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar patentes en XML: {ex.Message}");
                return false;
            }
        }


        /// <summary>
        /// Lee la lista de patentes desde un XML en la ruta especificada.
        /// </summary>
        /// <returns>La lista de patentes leída desde el XML o una lista vacía si ocurre algun error.</returns>
        public List<Patente> Leer()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Patente>));
                using (StreamReader sr = new StreamReader(filePath))
                {
                    return (List<Patente>)serializer.Deserialize(sr);
                }
            }
            catch (Exception)
            {
                return new List<Patente>();
            }
        }
    }
}
