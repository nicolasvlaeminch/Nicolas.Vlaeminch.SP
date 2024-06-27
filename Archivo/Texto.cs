using Entidades;
using System;
using System.Collections.Generic;
using System.IO;

namespace Archivo
{
    public class Texto : IArchivo
    {
        private string filePath;


        /// <summary>
        /// Inicializa una instancia de la clase Texto y añade la ruta del archivo.
        /// </summary>
        public Texto()
        {
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "patentes.txt");
        }

        /// <summary>
        /// Guarda una lista de patentes en un archivo de texto en la ruta especificada.
        /// </summary>
        /// <param name="datos">La lista de patentes a guardar.</param>
        /// <returns>Dara True si se guardo con exito o False si hubo un error.</returns>
        public bool Guardar(List<Patente> datos)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    foreach (var patente in datos)
                    {
                        sw.WriteLine($"{patente.CodigoPatente},{patente.TipoCodigo}");
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Lee la lista de patentes desde un XML en la ruta especificada.
        /// </summary>
        /// <returns>Una lista de patentes leida desde el XML o una lista vacia si ocurre un error.</returns>
        public List<Patente> Leer()
        {
            List<Patente> patentes = new List<Patente>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] datos = line.Split(',');
                        string codigo = datos[0];
                        string tipo = datos[1];
                        patentes.Add(new Patente(codigo, tipo));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer desde archivo de texto: {ex.Message}");
            }
            return patentes;
        }
    }
}
