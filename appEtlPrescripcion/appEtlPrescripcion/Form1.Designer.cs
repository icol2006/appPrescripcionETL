
namespace appEtlPrescripcion
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmbMesAnterior = new System.Windows.Forms.ComboBox();
            this.gbxControlesProcesamiento = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtMesActual = new System.Windows.Forms.MaskedTextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMesActual = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtSaldoContravencional = new System.Windows.Forms.TextBox();
            this.txtSaldoCartera = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.txtCantRegMesActual = new System.Windows.Forms.TextBox();
            this.txtCantRegMesAnterior = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSaldoTotal = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtProcesados = new System.Windows.Forms.TextBox();
            this.gbProcesoPrescritos = new System.Windows.Forms.GroupBox();
            this.ptbLoading = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtTiempoEspera = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gbxControlesProcesamiento.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.gbProcesoPrescritos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(717, 375);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label20);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.cmbMesAnterior);
            this.tabPage2.Controls.Add(this.gbxControlesProcesamiento);
            this.tabPage2.Controls.Add(this.txtTiempoEspera);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.cmbMesActual);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(709, 349);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Procesamiento";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmbMesAnterior
            // 
            this.cmbMesAnterior.FormattingEnabled = true;
            this.cmbMesAnterior.Location = new System.Drawing.Point(112, 24);
            this.cmbMesAnterior.Name = "cmbMesAnterior";
            this.cmbMesAnterior.Size = new System.Drawing.Size(140, 21);
            this.cmbMesAnterior.TabIndex = 15;
            this.cmbMesAnterior.SelectedIndexChanged += new System.EventHandler(this.cmbMesAnterior_SelectedIndexChanged);
            // 
            // gbxControlesProcesamiento
            // 
            this.gbxControlesProcesamiento.Controls.Add(this.button8);
            this.gbxControlesProcesamiento.Controls.Add(this.label15);
            this.gbxControlesProcesamiento.Controls.Add(this.label14);
            this.gbxControlesProcesamiento.Controls.Add(this.txtMesActual);
            this.gbxControlesProcesamiento.Controls.Add(this.button7);
            this.gbxControlesProcesamiento.Controls.Add(this.button6);
            this.gbxControlesProcesamiento.Controls.Add(this.label9);
            this.gbxControlesProcesamiento.Controls.Add(this.button3);
            this.gbxControlesProcesamiento.Controls.Add(this.label8);
            this.gbxControlesProcesamiento.Controls.Add(this.label4);
            this.gbxControlesProcesamiento.Controls.Add(this.button5);
            this.gbxControlesProcesamiento.Controls.Add(this.button4);
            this.gbxControlesProcesamiento.Controls.Add(this.label7);
            this.gbxControlesProcesamiento.Controls.Add(this.button2);
            this.gbxControlesProcesamiento.Controls.Add(this.label6);
            this.gbxControlesProcesamiento.Controls.Add(this.button1);
            this.gbxControlesProcesamiento.Controls.Add(this.label1);
            this.gbxControlesProcesamiento.Enabled = false;
            this.gbxControlesProcesamiento.Location = new System.Drawing.Point(28, 62);
            this.gbxControlesProcesamiento.Name = "gbxControlesProcesamiento";
            this.gbxControlesProcesamiento.Size = new System.Drawing.Size(664, 234);
            this.gbxControlesProcesamiento.TabIndex = 17;
            this.gbxControlesProcesamiento.TabStop = false;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(270, 28);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 17;
            this.button8.Text = "Sintaxis SQL";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(394, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 13);
            this.label15.TabIndex = 16;
            this.label15.Text = "Mes actual";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(503, 75);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "dd/mm/yyyy";
            // 
            // txtMesActual
            // 
            this.txtMesActual.Location = new System.Drawing.Point(397, 68);
            this.txtMesActual.Mask = "00/00/0000";
            this.txtMesActual.Name = "txtMesActual";
            this.txtMesActual.Size = new System.Drawing.Size(100, 20);
            this.txtMesActual.TabIndex = 14;
            this.txtMesActual.ValidatingType = typeof(System.DateTime);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(527, 108);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 13;
            this.button7.Text = "Procesar";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(175, 108);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 12;
            this.button6.Text = "Procesar";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Exportar mes actual";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(175, 28);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Procesar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(365, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Exportar mes anterior";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Conversion de datos";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(527, 147);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "Procesar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(175, 147);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Procesar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(365, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Exportar registros repetidos";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(270, 68);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Detener";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Exportar registros nuevos";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(175, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Iniciar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Procemiento registro prescriptos";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Mes anterior";
            // 
            // cmbMesActual
            // 
            this.cmbMesActual.FormattingEnabled = true;
            this.cmbMesActual.Location = new System.Drawing.Point(351, 24);
            this.cmbMesActual.Name = "cmbMesActual";
            this.cmbMesActual.Size = new System.Drawing.Size(140, 21);
            this.cmbMesActual.TabIndex = 16;
            this.cmbMesActual.SelectedIndexChanged += new System.EventHandler(this.cmbMesActual_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(266, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Mes actual";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtSaldoContravencional);
            this.tabPage1.Controls.Add(this.txtSaldoCartera);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.button9);
            this.tabPage1.Controls.Add(this.txtCantRegMesActual);
            this.tabPage1.Controls.Add(this.txtCantRegMesAnterior);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.txtSaldoTotal);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.chart1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(709, 349);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Graficos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtSaldoContravencional
            // 
            this.txtSaldoContravencional.Location = new System.Drawing.Point(390, 312);
            this.txtSaldoContravencional.Name = "txtSaldoContravencional";
            this.txtSaldoContravencional.Size = new System.Drawing.Size(100, 20);
            this.txtSaldoContravencional.TabIndex = 12;
            // 
            // txtSaldoCartera
            // 
            this.txtSaldoCartera.Location = new System.Drawing.Point(151, 312);
            this.txtSaldoCartera.Name = "txtSaldoCartera";
            this.txtSaldoCartera.Size = new System.Drawing.Size(100, 20);
            this.txtSaldoCartera.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(291, 319);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 13);
            this.label17.TabIndex = 10;
            this.label17.Text = "Contravencional";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(73, 319);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Saldo Cartera";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(287, 33);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(108, 23);
            this.button9.TabIndex = 8;
            this.button9.Text = "Obtener datos";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // txtCantRegMesActual
            // 
            this.txtCantRegMesActual.Enabled = false;
            this.txtCantRegMesActual.Location = new System.Drawing.Point(436, 9);
            this.txtCantRegMesActual.Name = "txtCantRegMesActual";
            this.txtCantRegMesActual.Size = new System.Drawing.Size(100, 20);
            this.txtCantRegMesActual.TabIndex = 7;
            // 
            // txtCantRegMesAnterior
            // 
            this.txtCantRegMesAnterior.Enabled = false;
            this.txtCantRegMesAnterior.Location = new System.Drawing.Point(175, 9);
            this.txtCantRegMesAnterior.Name = "txtCantRegMesAnterior";
            this.txtCantRegMesAnterior.Size = new System.Drawing.Size(100, 20);
            this.txtCantRegMesAnterior.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(146, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Cantidad registro mes anterior";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(284, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(140, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Cantidad registro mes actual";
            // 
            // txtSaldoTotal
            // 
            this.txtSaldoTotal.Enabled = false;
            this.txtSaldoTotal.Location = new System.Drawing.Point(174, 36);
            this.txtSaldoTotal.Name = "txtSaldoTotal";
            this.txtSaldoTotal.Size = new System.Drawing.Size(101, 20);
            this.txtSaldoTotal.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(111, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Saldo total";
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(16, 65);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(687, 236);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Total";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Procesados";
            // 
            // txtTotal
            // 
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(50, 19);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(94, 20);
            this.txtTotal.TabIndex = 4;
            // 
            // txtProcesados
            // 
            this.txtProcesados.Enabled = false;
            this.txtProcesados.Location = new System.Drawing.Point(221, 19);
            this.txtProcesados.Name = "txtProcesados";
            this.txtProcesados.Size = new System.Drawing.Size(94, 20);
            this.txtProcesados.TabIndex = 5;
            // 
            // gbProcesoPrescritos
            // 
            this.gbProcesoPrescritos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProcesoPrescritos.Controls.Add(this.txtProcesados);
            this.gbProcesoPrescritos.Controls.Add(this.label2);
            this.gbProcesoPrescritos.Controls.Add(this.label3);
            this.gbProcesoPrescritos.Controls.Add(this.txtTotal);
            this.gbProcesoPrescritos.Location = new System.Drawing.Point(348, 412);
            this.gbProcesoPrescritos.Name = "gbProcesoPrescritos";
            this.gbProcesoPrescritos.Size = new System.Drawing.Size(321, 48);
            this.gbProcesoPrescritos.TabIndex = 7;
            this.gbProcesoPrescritos.TabStop = false;
            this.gbProcesoPrescritos.Visible = false;
            // 
            // ptbLoading
            // 
            this.ptbLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ptbLoading.Image = global::AppPrescripcion.Properties.Resources.loading_buffering;
            this.ptbLoading.Location = new System.Drawing.Point(675, 412);
            this.ptbLoading.Name = "ptbLoading";
            this.ptbLoading.Size = new System.Drawing.Size(50, 48);
            this.ptbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbLoading.TabIndex = 6;
            this.ptbLoading.TabStop = false;
            this.ptbLoading.Visible = false;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(674, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "Version 4";
            // 
            // txtTiempoEspera
            // 
            this.txtTiempoEspera.Location = new System.Drawing.Point(566, 25);
            this.txtTiempoEspera.Name = "txtTiempoEspera";
            this.txtTiempoEspera.Size = new System.Drawing.Size(64, 20);
            this.txtTiempoEspera.TabIndex = 18;
            this.txtTiempoEspera.Text = "30";
            this.txtTiempoEspera.TextChanged += new System.EventHandler(this.txtTiempoEspera_TextChanged);
            this.txtTiempoEspera.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTiempoEspera_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(561, 11);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(125, 13);
            this.label19.TabIndex = 19;
            this.label19.Text = "Tiempo espera consultas";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(635, 31);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 13);
            this.label20.TabIndex = 20;
            this.label20.Text = "segundos";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 472);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.gbProcesoPrescritos);
            this.Controls.Add(this.ptbLoading);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "App Prescripcion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.gbxControlesProcesamiento.ResumeLayout(false);
            this.gbxControlesProcesamiento.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.gbProcesoPrescritos.ResumeLayout(false);
            this.gbProcesoPrescritos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtProcesados;
        private System.Windows.Forms.PictureBox ptbLoading;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbProcesoPrescritos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox cmbMesActual;
        private System.Windows.Forms.ComboBox cmbMesAnterior;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox gbxControlesProcesamiento;
        private System.Windows.Forms.TextBox txtCantRegMesActual;
        private System.Windows.Forms.TextBox txtCantRegMesAnterior;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSaldoTotal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.MaskedTextBox txtMesActual;
        private System.Windows.Forms.TextBox txtSaldoContravencional;
        private System.Windows.Forms.TextBox txtSaldoCartera;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtTiempoEspera;
        private System.Windows.Forms.Label label20;
    }
}

