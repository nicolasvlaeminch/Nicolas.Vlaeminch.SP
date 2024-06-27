using System;
using System.Windows.Forms;
using Entidades;

namespace Patentes
{
    public delegate void FinExposicionPatente(VistaPatente vistaPatente);
    public delegate void MostrarPatente(object patente);

    public partial class VistaPatente : UserControl
    {
        public event FinExposicionPatente finExposicion;

        private Timer timerMostrar;

        public VistaPatente()
        {
            InitializeComponent();
            picPatente.Image = fondosPatente.Images[(int)Tipo.Mercosur];

            // Inicializar el temporizador para controlar la exposicion de la patente
            timerMostrar = new Timer();
            timerMostrar.Interval = 1500;
            timerMostrar.Tick += TimerMostrar_Tick;
        }

        public void MostrarPatente(object patente)
        {
            try
            {
                string tipoCodigo = ((Patente)patente).TipoCodigo;

                // Seleccionar la imagen basada en el tipo
                if (tipoCodigo == "Mercosur")
                {
                    picPatente.Image = fondosPatente.Images[1];
                }
                else if (tipoCodigo == "Vieja")
                {
                    picPatente.Image = fondosPatente.Images[0];
                }
                else
                {
                    // Caso no esperado
                }

                lblPatenteNro.Text = patente.ToString();

                // Iniciar el temporizador para controlar el tiempo de exposicion
                timerMostrar.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar patente: {ex.Message}");
            }
        }

        private void TimerMostrar_Tick(object sender, EventArgs e)
        {
            // Detener el temporizador
            timerMostrar.Stop();

            // Disparar el evento de que finalizo la exposicion de la patente.
            finExposicion?.Invoke(this);
        }
    }
}
