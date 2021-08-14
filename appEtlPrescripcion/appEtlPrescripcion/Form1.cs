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

namespace appEtlPrescripcion
{
    public partial class Form1 : Form
    {
        Boolean refrescarEstadProcsamiento = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    EstadoForm.procesarDatos = true;

                    var datos = ProcesarDAtos.procesarFechas();

                    TextWriter tw = new StreamWriter("SavedList.txt");

                    foreach (var item in datos)
                    {
                        tw.WriteLine(item[0] + " " + item[1]);
                    }

                    tw.Close();

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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                    ProcesarDAtos.realizarConversion();
                        

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
    }
}
