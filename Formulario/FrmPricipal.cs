using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Entidades;
using Archivo;
using System.Threading;

namespace Formulario
{
    /// <summary>
    /// Formulario principal para la gestión de patentes.
    /// </summary>
    public partial class FrmPricipal : Form
    {
        List<Patente> patentes;
        List<Thread> hilos; // Lista de hilos para la simulación
        int bandera = 0;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FrmPricipal"/>.
        /// </summary>
        public FrmPricipal()
        {
            InitializeComponent();
            patentes = new List<Patente>();
            hilos = new List<Thread>(); // Inicialización de la lista de hilos
        }

        /// <summary>
        /// Manejador del evento Load del formulario.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void FrmPricipal_Load(object sender, EventArgs e)
        {
            // Asociar evento finExposicion con ProximaPatente para vistaPatente
            vistaPatente.finExposicion += ProximaPatente;
        }

        /// <summary>
        /// Manejador del evento FormClosing del formulario.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void FrmPricipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Asegurarse de que todos los hilos estén terminados al cerrar el formulario
            FinalizarSimulacion();
        }

        /// <summary>
        /// Manejador del evento Click del botón para agregar más patentes.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnMas_Click(object sender, EventArgs e)
        {
            try
            {
                List<Patente> listPatente = new List<Patente>
                {
                    new Patente("CP709WA", "Mercosur"),
                    new Patente("DIB009", "Vieja"),
                    new Patente("FD010GC", "Mercosur")
                };

                if (bandera == 1)
                {
                    // Guardar en la base de datos SQL
                    IArchivo archivoSQL = new Sql();
                    bool guardadoSQL = archivoSQL.Guardar(listPatente);

                    if (guardadoSQL)
                    {
                        MessageBox.Show("¡Patentes guardadas en la base de datos!");
                    }
                    else
                    {
                        MessageBox.Show("¡Error al guardar en la base de datos!");
                    }
                }
                else if (bandera == 2)
                {
                    // Guardar en archivo XML
                    IArchivo archivoXml = new Xml();
                    bool guardadoXml = archivoXml.Guardar(listPatente);

                    if (guardadoXml)
                    {
                        MessageBox.Show("¡Patentes guardadas en el archivo XML!");
                    }
                    else
                    {
                        MessageBox.Show("¡Error al guardar en el archivo XML!");
                    }
                }
                else if (bandera == 3)
                {
                    // Guardar en archivo de texto
                    IArchivo archivoTexto = new Texto();
                    bool guardadoTexto = archivoTexto.Guardar(listPatente);

                    if (guardadoTexto)
                    {
                        MessageBox.Show("¡Patentes guardadas en el archivo!");
                    }
                    else
                    {
                        MessageBox.Show("¡Error al guardar en el archivo!");
                    }
                }
                else
                {
                    MessageBox.Show("¡Debe seleccionar un tipo antes de guardar!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Manejador del evento Click del botón para leer patentes desde la base de datos SQL.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnSql_Click(object sender, EventArgs e)
        {
            try
            {
                bandera = 1;
                IArchivo archivoSQL = new Sql();
                patentes = archivoSQL.Leer();

                if (patentes.Count > 0)
                {
                    IniciarSimulacion();
                }
                else
                {
                    MessageBox.Show("No se encontraron patentes en la base de datos SQL.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer patentes desde SQL: {ex.Message}");
            }
        }

        /// <summary>
        /// Manejador del evento Click del botón para leer patentes desde un archivo XML.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnXml_Click(object sender, EventArgs e)
        {
            try
            {
                bandera = 2;
                IArchivo archivoXml = new Xml();
                patentes = archivoXml.Leer();

                if (patentes.Count > 0)
                {
                    IniciarSimulacion();
                }
                else
                {
                    MessageBox.Show("No se encontraron patentes en el archivo XML.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer patentes desde XML: {ex.Message}");
            }
        }

        /// <summary>
        /// Manejador del evento Click del botón para leer patentes desde un archivo de texto.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnTxt_Click(object sender, EventArgs e)
        {
            try
            {
                bandera = 3;
                IArchivo archivoTexto = new Texto();
                patentes = archivoTexto.Leer();

                if (patentes.Count > 0)
                {
                    IniciarSimulacion();
                }
                else
                {
                    MessageBox.Show("No se encontraron patentes en el archivo de texto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer patentes desde archivo de texto: {ex.Message}");
            }
        }

        /// <summary>
        /// Inicia la simulación de visualización de patentes.
        /// </summary>
        private void IniciarSimulacion()
        {
            FinalizarSimulacion();

            foreach (Patentes.VistaPatente vista in Controls.OfType<Patentes.VistaPatente>())
            {
                ProximaPatente(vista);
            }
        }

        /// <summary>
        /// Finaliza todos los hilos activos.
        /// </summary>
        private void FinalizarSimulacion()
        {
            foreach (Thread hilo in hilos)
            {
                if (hilo.IsAlive)
                {
                    hilo.Abort();
                }
            }
            hilos.Clear();
        }

        /// <summary>
        /// Muestra la próxima patente en la vista.
        /// </summary>
        /// <param name="vistaPatente">La vista de la patente.</param>
        private void ProximaPatente(Patentes.VistaPatente vistaPatente)
        {
            if (patentes.Count > 0)
            {
                Patente proximaPatente = patentes[0];
                patentes.RemoveAt(0);

                // Iniciar un hilo para mostrar la patente en la vista
                Thread hilo = new Thread(() =>
                {
                    vistaPatente.Invoke((MethodInvoker)delegate
                    {
                        vistaPatente.MostrarPatente(proximaPatente);
                    });
                });

                // Agregar hilo a la lista de hilos
                hilos.Add(hilo);

                // Iniciar el hilo
                hilo.Start();
            }
        }
    }
}
