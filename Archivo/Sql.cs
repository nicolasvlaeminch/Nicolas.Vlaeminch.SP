using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Archivo
{
    public class Sql : IArchivo
    {
        private SqlCommand _comando;
        private SqlConnection _conexion;

        public Sql()
        {
            _conexion = new SqlConnection("Data Source=DESKTOP-TKOUI69\\SQLEXPRESS;Initial Catalog=LAB_SP;Integrated Security=True");
            _comando = new SqlCommand();
            _comando.Connection = _conexion;
            _comando.CommandType = CommandType.Text;
        }

        public bool Guardar(List<Patente> datos)
        {
            try
            {
                _conexion.Open();

                foreach (var patente in datos)
                {
                    _comando.CommandText = $"INSERT INTO patentes (patente, tipo) VALUES ('{patente.CodigoPatente}', '{patente.TipoCodigo}')";
                    _comando.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Error al guardar en la base de datos: {ex.Message}");
                return false;
            }
            finally
            {
                if (_conexion.State == ConnectionState.Open)
                {
                    _conexion.Close();
                }
            }
        }

        public List<Patente> Leer()
        {
            List<Patente> patentes = new List<Patente>();

            try
            {
                _conexion.Open();

                _comando.CommandText = "SELECT patente, tipo FROM patentes";
                SqlDataReader reader = _comando.ExecuteReader();

                while (reader.Read())
                {
                    string codigo = reader["patente"].ToString();
                    string tipo = reader["tipo"].ToString();
                    patentes.Add(new Patente(codigo, tipo));
                }

                return patentes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer datos de la base de datos: {ex.Message}");
                return null;
            }
            finally
            {
                if (_conexion.State == ConnectionState.Open)
                {
                    _conexion.Close();
                }
            }
        }
    }
}
