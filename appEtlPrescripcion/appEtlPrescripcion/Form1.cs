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
        String resultadoProcesamiento = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iniciarProcedimientoPrescripcion();
        }

        private void iniciarProcedimientoPrescripcion()
        {
            gbProcesoPrescritos.Visible = true;
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    EstadoForm.procesarDatos = true;
                    EstadoForm.cantidadRegistrosProcesado = 0;

                    resultadoProcesamiento=ProcesarDAtos.procesarFechas(this.mesActual);

                    this.Invoke(new Action(() =>
                    {
                        if(resultadoProcesamiento.Trim().Length>0)
                        {
                            MessageBox.Show(resultadoProcesamiento);
                        }
                        else
                        {
                            MessageBox.Show("Finalizado");
                        }
                       
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

                            if(EstadoForm.formEstaAbierto == true)
                            {                                
                                this.Invoke(new Action(() =>
                                {
                                    txtProcesados.Text = EstadoForm.cantidadRegistrosProcesado + "";
                                    txtTotal.Text = EstadoForm.totalRegistros + "";
                                    if (EstadoForm.procesarDatos)
                                    {
                                        ptbLoading.Visible = true;
                                    }
                                    else
                                    {
                                        ptbLoading.Visible = false;
                                    }
                                }));
                            }
                      
                        }
                        catch (Exception ex)
                        {
                           

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
            realizarConversion();
        }

        private void realizarConversion()
        {
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    EstadoForm.procesarDatos = true;
                   resultadoProcesamiento= ProcesarDAtos.realizarConversion(this.mesActual);

                    this.Invoke(new Action(() =>
                    {
                        if(resultadoProcesamiento.Trim().Length>0)
                        {
                            MessageBox.Show(resultadoProcesamiento);
                        }
                        else
                        {
                            MessageBox.Show("Finalizado");
                        }
                        
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
            exportarRegistrosNuevos();
        }

        private void exportarRegistrosNuevos()
        {
            var nombreArchivo = abrirFileDialog();

            Thread thread2 = new Thread(() =>
            {
                try
                {
                    if (nombreArchivo.Trim().Length > 0)
                    {
                        EstadoForm.procesarDatos = true;
                        var res = DbMesNuevo.ExportarRegistroNuevos(this.mesActual);
                        ProcesarDAtos.GenerarCsv(res.Item1,nombreArchivo);

                        this.Invoke(new Action(() =>
                        {
                            if (res.Item3 == true)
                            {
                                MessageBox.Show(res.Item2);
                            }
                            else
                            {
                                MessageBox.Show("Finalizado");
                            }
                        }));
                    }
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

        private void button5_Click(object sender, EventArgs e)
        {
            exportarRegistrosRepetidos();
        }

        private void exportarRegistrosRepetidos()
        {
            var nombreArchivo = abrirFileDialog();

            Thread thread2 = new Thread(() =>
            {
                try
                {
                    if (nombreArchivo.Trim().Length > 0)
                    {
                        EstadoForm.procesarDatos = true;
                        var res = DbMesNuevo.ExportarRegistroRepetidos(this.mesActual);
                        ProcesarDAtos.GenerarCsv(res.Item1,nombreArchivo);

                        this.Invoke(new Action(() =>
                        {
                            if (res.Item3 == true)
                            {
                                MessageBox.Show(res.Item2);
                            }
                            else
                            {
                                MessageBox.Show("Finalizado");
                            }
                        }));
                    }
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
            exportarMesNuevos();
        }

        private String abrirFileDialog()
        {
            String resultado = "";

            SaveFileDialog oSaveFileDialog = new SaveFileDialog();
            oSaveFileDialog.Filter = "All files (*.*) | *.*";
            if (oSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = oSaveFileDialog.FileName;
                string extesion = Path.GetExtension(fileName);
                resultado = fileName;
            }

            return resultado;
        }

        private void exportarMesNuevos()
        {
           var nombreArchivo= abrirFileDialog();
             
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    if(nombreArchivo.Trim().Length>0)
                    {
                        EstadoForm.procesarDatos = true;
                        var res = DbMesNuevo.ExportarMesNuevo(this.mesActual);
                        ProcesarDAtos.GenerarCsv(res.Item1,nombreArchivo);

                        this.Invoke(new Action(() =>
                        {
                            if (res.Item3 == true)
                            {
                                MessageBox.Show(res.Item2);
                            }
                            else
                            {
                                MessageBox.Show("Finalizado");
                            }
                        }));
                    }

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

        private void button7_Click(object sender, EventArgs e)
        {
            exportarMesViejo();
        }

        private void exportarMesViejo()
        {
            var nombreArchivo = abrirFileDialog();

            Thread thread2 = new Thread(() =>
            {
                try
                {
                    if (nombreArchivo.Trim().Length > 0)
                    {
                        EstadoForm.procesarDatos = true;
                        var res = DbMesNuevo.ExportarMesViejo(this.mesAnterior);
                        ProcesarDAtos.GenerarCsv(res.Item1,nombreArchivo);

                        this.Invoke(new Action(() =>
                        {
                            if (res.Item3 == true)
                            {
                                MessageBox.Show(res.Item2);
                            }
                            else
                            {
                                MessageBox.Show("Finalizado");
                            }
                        }));
                    }
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
            generarGrafico("DESCRIPCION#INFRACCION");
        }

        private void generarGrafico(string columna)
        {
            var dt = DbGeneral.obtenerDatosDatables("select "+ columna +", COUNT(*) [Total] from " + mesActual + " group by  " + columna );

            //Get the names of Cities.
            string[] x = (from p in dt.AsEnumerable()
                          orderby p.Field<string>(columna) ascending
                          select p.Field<string>(columna)).ToArray();

            //Get the Total of Orders for each City.
            int[] y = (from p in dt.AsEnumerable()
                       orderby p.Field<string>(columna) ascending
                       select p.Field<int>("Total")).ToArray();

            chart1.Series[0].LegendText = "Graficos";
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
            activarControlesProcesamiento();

        }

        private void cmbMesActual_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mesActual = cmbMesActual.SelectedItem.ToString();
            activarControlesProcesamiento();
        }

        private void activarControlesProcesamiento()
        {
            if (this.mesAnterior.Trim().Length > 0 && this.mesActual.Trim().Length > 0)
            {
                gbxControlesProcesamiento.Enabled = true;
            }
            else
            {
                gbxControlesProcesamiento.Enabled = false;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            EstadoForm.formEstaAbierto = false;
        }
    }
}
