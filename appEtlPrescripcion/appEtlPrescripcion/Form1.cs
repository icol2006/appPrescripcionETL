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
        Boolean refrescarEstado = true;

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
                    var datos = ProcesarPrescripcion.procesarDatos();

                    TextWriter tw = new StreamWriter("SavedList.txt");

                    foreach (var item in datos)
                    {
                        tw.WriteLine(item[0] + " " + item[1]);
                    }

                    tw.Close();

                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show("fdsalkj");
                    }));        
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

        private void actualizarEstadoForm()
        {
            Thread thread2 = new Thread(() =>
            {
                try
                {
                    while (this.refrescarEstado == true)
                    {
                        try
                        {
                            Thread.Sleep(50);

                            this.Invoke(new Action(() =>
                            {
                                txtProcesados.Text = EstadoForm.cantidadRegistrosProcesado+"";
                                txtTotal.Text = EstadoForm.totalRegistros+"";
                                var afds = 0;
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
            this.refrescarEstado = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            actualizarEstadoForm();
        }
    }
}
