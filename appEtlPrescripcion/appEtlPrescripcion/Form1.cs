using AppBot.Modelos;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace appEtlPrescripcion
{
    public partial class Form1 : Form
    {
        Boolean refrescarEstadProcsamiento = true;
        String mesActual = "";
        String mesAnterior = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gbProcesoPrescritos.Visible = true;
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    EstadoForm.procesarDatos = true;
                    EstadoForm.cantidadRegistrosProcesado = 0;
              
                    ProcesarDAtos.procesarFechas(this.mesActual);

                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Finalizado");
                    }));        
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    EstadoForm.procesarDatos = false;
                    try
                    {
                        gbProcesoPrescritos.Visible = false;
                    }
                    catch (Exception)
                    {

                    }
                  
                }
            });
            thread2.Start();




        }

        private void actualizarEstadoForm()
        {
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    while (this.refrescarEstadProcsamiento == true)
                    {
                        try
                        {
                            Thread.Sleep(50);

                            this.Invoke(new Action(() =>
                            {
                                txtProcesados.Text = EstadoForm.cantidadRegistrosProcesado+"";
                                txtTotal.Text = EstadoForm.totalRegistros+"";
                                if (EstadoForm.procesarDatos)
                                {
                                    ptbLoading.Visible = true;
                                }else
                                {
                                    ptbLoading.Visible = false;
                                }
                            }));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                  
                }
                catch (Exception ex)
                {

                }
                finally
                {


                }
            });
            thread2.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.refrescarEstadProcsamiento = false;
            EstadoForm.procesarDatos = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarMeses();
            actualizarEstadoForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EstadoForm.procesarDatos = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    EstadoForm.procesarDatos = true;
                    ProcesarDAtos.realizarConversion(this.mesActual);                     

                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Finalizado");
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    EstadoForm.procesarDatos = false;
                }
            });
            thread2.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    EstadoForm.procesarDatos = true;
                    DbMesNuevo.ExportarRegistroNuevos(cmbMesActual.Text);
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Finalizado");
                    }));
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    EstadoForm.procesarDatos = false;
                }
            });
            thread2.Start();
        
   
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    EstadoForm.procesarDatos = true;
                    DbMesNuevo.ExportarRegistroRepetidos(this.mesActual);

                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Finalizado");
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    EstadoForm.procesarDatos = false;
                }
            });
            thread2.Start();

         
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    EstadoForm.procesarDatos = true;
                    DbMesNuevo.ExportarMesNuevo(cmbMesActual.Text);

                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Finalizado");
                    }));
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    EstadoForm.procesarDatos = false;
                }
            });
            thread2.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    EstadoForm.procesarDatos = true;
                    DbMesNuevo.ExportarMesViejo(this.mesAnterior);

                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Finalizado");
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    EstadoForm.procesarDatos = false;
                }
            });
            thread2.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Fetch the Statistical data from database.


            //Get the names of Cities.
            string[] x =new string[] { "uno", "dos", "tres", "cuatro", "cinco"};

            //Get the Total of Orders for each City.
            int[] y =new int[] { 20, 3, 24, 56, 32 };

            chart1.Series[0].LegendText = "Brazil Order Statistics";
            chart1.Series[0].ChartType = SeriesChartType.Bar;
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].Points.DataBindXY(x, y);
        }

        private void cargarMeses()
        {
            var meses = DbGeneral.ObtenerMeses();

 
            foreach (var item in meses)
            {
                cmbMesAnterior.Items.Add(item);
                cmbMesActual.Items.Add(item);
            }
        }

        private void cmbMesAnterior_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mesAnterior = cmbMesAnterior.SelectedItem.ToString();
        }

        private void cmbMesActual_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mesActual = cmbMesActual.SelectedItem.ToString();
        }
    }
}
