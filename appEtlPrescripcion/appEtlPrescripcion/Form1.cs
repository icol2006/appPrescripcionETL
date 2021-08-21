using AppBot.Modelos;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            var fechaMesActual = txtMesActual.Text;

            DateTime fechaCorte = DateTime.ParseExact(fechaMesActual, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //var ultimoDiaMesActual= DateTime.DaysInMonth(fechaCorte.Year, fechaCorte.Month);
            //var fechaUltimoDiaMesActual = new DateTime(fechaCorte.Year, fechaCorte.Month, ultimoDiaMesActual);


            Thread thread2 = new Thread(() =>
            {
                try
                {
                    EstadoForm.procesarDatos = true;
                    EstadoForm.cantidadRegistrosProcesado = 0;

                    try
                    {
                        ProcesarDAtos.procesarFechas("repetidos", fechaCorte.ToString("yyyy/MM/dd"));
                    }
                    catch (Exception)
                    {

                    }

                    EstadoForm.cantidadRegistrosProcesado = 0;
                    EstadoForm.totalRegistros = 0;
                    resultadoProcesamiento =ProcesarDAtos.procesarFechas(this.mesActual, fechaCorte.ToString("yyyy/MM/dd"));
              

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
            this.txtMesActual.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EstadoForm.procesarDatos = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var verificaMesAnterior = ProcesarDAtos.verificarColumnas(this.mesAnterior);
            var verificaMesActual = ProcesarDAtos.verificarColumnas(this.mesActual);

            if(verificaMesAnterior.Item2==false && verificaMesActual.Item2==false)
            {
                realizarConversion();
            }
            else
            {
                String resultado = "";

                if(verificaMesAnterior.Item2==true)
                {
                    resultado = verificaMesAnterior.Item1 + "Tabla " + mesAnterior + Environment.NewLine;
                }
                if (verificaMesActual.Item2 == true)
                {
                    resultado += verificaMesActual.Item1 + "Tabla " + mesActual + Environment.NewLine;
                }
                MessageBox.Show(resultado);
            }           
        }

        private void realizarConversion()
        {
            Thread thread2 = new Thread(() =>
            {
                try
                {
                   EstadoForm.procesarDatos = true;
                   resultadoProcesamiento= ProcesarDAtos.realizarConversion(this.mesActual,this.mesAnterior);

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
                    try
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (Exception)
                    {

                    }                
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
                        var res = DbGeneral.ExportarRegistroNuevos(this.mesActual);
                        var nombresColumnas = DbGeneral.obtenerNombresColumnas(this.mesActual);

                        ProcesarDAtos.GenerarCsv(res.Item1,nombreArchivo,nombresColumnas.Item1);

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
                        var res = DbGeneral.ExportarRegistroRepetidos(this.mesActual);
                        var nombresColumnas = DbGeneral.obtenerNombresColumnas(this.mesActual);

                        ProcesarDAtos.GenerarCsv(res.Item1,nombreArchivo,nombresColumnas.Item1);

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
            exportarMesActual();
        }

        private String abrirFileDialog()
        {
            String resultado = "";

            SaveFileDialog oSaveFileDialog = new SaveFileDialog();
            oSaveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (oSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = oSaveFileDialog.FileName;
                string extesion = Path.GetExtension(fileName);
                resultado = fileName;
            }

            return resultado;
        }

        private void exportarMesActual()
        {
           var nombreArchivo= abrirFileDialog();
             
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    if(nombreArchivo.Trim().Length>0)
                    {
                        EstadoForm.procesarDatos = true;
                        var res = DbGeneral.ExportarMesNuevo(this.mesActual);
                        var nombresColumnas = DbGeneral.obtenerNombresColumnas(this.mesActual);

                        ProcesarDAtos.GenerarCsv(res.Item1,nombreArchivo,nombresColumnas.Item1);

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
                        var res = DbGeneral.ExportarMesViejo(this.mesAnterior);
                        var nombresColumnas = DbGeneral.obtenerNombresColumnas(this.mesActual);

                        ProcesarDAtos.GenerarCsv(res.Item1,nombreArchivo,nombresColumnas.Item1);

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
       
        }


        private void cargarMeses()
        {
           Tuple<List<String>, string, Boolean> datos;

           Thread thread2 = new Thread(() =>
            {
                try
                {
                    datos = DbGeneral.ObtenerMeses();

                    this.Invoke(new Action(() =>
                    {
                        if (datos.Item3 == false)
                        {
                            MessageBox.Show(datos.Item2);
                        }

                        foreach (var item in datos.Item1)
                        {
                            cmbMesAnterior.Items.Add(item);
                            cmbMesActual.Items.Add(item);
                        }
                    }));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                }
            });
            thread2.Start();
                  
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

        private void button9_Click(object sender, EventArgs e)
        {
            obtenerCantidadMeses();
            obtenerSaldo();
            obtenerSaldoEtapaCartera();
            obtenerSaldoEtapaContravencional();
            generarGraficoEtapa();
        }

        private void obtenerCantidadMeses()
        {

            Thread thread2 = new Thread(() =>
            {
                try
                {
                    var res1 =  ProcesarDAtos.obtnerTotalRegistro(this.mesAnterior);
                    var res2 = ProcesarDAtos.obtnerTotalRegistro(this.mesActual);


                    this.Invoke(new Action(() =>
                    {

                        if(res1.Item3==false)
                        {
                            MessageBox.Show(res1.Item2);
                        }
                        else
                        {                            
                            this.txtCantRegMesAnterior.Text = res1.Item1+"";
                        }

                        if (res2.Item3 == false)
                        {
                            MessageBox.Show(res2.Item2);
                        }
                        else
                        {
                            this.txtCantRegMesActual.Text = res2.Item1 + ""; ;
                        }                      
                    
                      
                    }));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                }
            });
            thread2.Start();
        }


        private void obtenerSaldo()
        {

            Thread thread2 = new Thread(() =>
            {
                try
                {
                    var res1 = ProcesarDAtos.obtnerTotalSaldo(this.mesActual);


                    this.Invoke(new Action(() =>
                    {

                        if (res1.Item3 == false)
                        {
                            MessageBox.Show(res1.Item2);
                        }
                        else
                        {
                            this.txtSaldoTotal.Text = String.Format("{0:n0}", res1.Item1) + "";
                        }

                    }));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                }
            });
            thread2.Start();
        }

        private void obtenerSaldoEtapaCartera()
        {

            Thread thread2 = new Thread(() =>
            {
                try
                {
                    var res1 = ProcesarDAtos.obtnerTotalSaldoEtapaCartera(this.mesActual);


                    this.Invoke(new Action(() =>
                    {

                        this.txtSaldoCartera.Text = String.Format("{0:n0}", res1.Item1) + "";
                    }));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                }
            });
            thread2.Start();
        }

        private void obtenerSaldoEtapaContravencional()
        {

            Thread thread2 = new Thread(() =>
            {
                try
                {
                    var res1 = ProcesarDAtos.obtnerTotalSaldoEtapaContravencional(this.mesActual);


                    this.Invoke(new Action(() =>
                    {

                        this.txtSaldoContravencional.Text = String.Format("{0:n0}", res1.Item1) + "";
                    }));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                }
            });
            thread2.Start();
        }

        private void generarGraficoEtapa()
        {

            Thread thread2 = new Thread(() =>
            {
                try
                {
                    var res = ProcesarDAtos.generarGrafico("etapa", this.mesActual);
                    //DESCRIPCION#INFRACCION            

                    this.Invoke(new Action(() =>
                    {
                        chart1.Series[0].LegendText = "Graficos";
                        chart1.Series[0].ChartType = SeriesChartType.Bar;
                        chart1.Series[0].IsValueShownAsLabel = true;
                        chart1.Series[0].Points.DataBindXY(res.Item1, res.Item2);
                    }));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                }
            });
            thread2.Start();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ProcesarDAtos.renombrarColumnas(this.mesActual);
        }
    }
}
