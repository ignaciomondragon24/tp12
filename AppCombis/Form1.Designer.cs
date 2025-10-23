namespace AppCombis
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            groupBoxTerminal = new GroupBox();
            lblTiempoRestante = new Label();
            lblTiempo = new Label();
            cmbTipoPasajero = new ComboBox();
            lblTipoPasajero = new Label();
            lstEnEspera = new ListBox();
            btnSubir = new Button();
            btnAnotar = new Button();
            txtPasajero = new TextBox();
            lblEnEspera = new Label();
            lblPasajero = new Label();
            lblTitulo = new Label();
            groupBoxEstadisticas = new GroupBox();
            lblRecaudacion = new Label();
            lblPasajerosHoy = new Label();
            lblViajesHoy = new Label();
            btnReporte = new Button();
            btnCerrar = new Button();
            timerCombi = new System.Windows.Forms.Timer(components);
            groupBoxTerminal.SuspendLayout();
            groupBoxEstadisticas.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxTerminal
            // 
            groupBoxTerminal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxTerminal.BackColor = Color.FromArgb(240, 248, 255);
            groupBoxTerminal.Controls.Add(lblTiempoRestante);
            groupBoxTerminal.Controls.Add(lblTiempo);
            groupBoxTerminal.Controls.Add(cmbTipoPasajero);
            groupBoxTerminal.Controls.Add(lblTipoPasajero);
            groupBoxTerminal.Controls.Add(lstEnEspera);
            groupBoxTerminal.Controls.Add(btnSubir);
            groupBoxTerminal.Controls.Add(btnAnotar);
            groupBoxTerminal.Controls.Add(txtPasajero);
            groupBoxTerminal.Controls.Add(lblEnEspera);
            groupBoxTerminal.Controls.Add(lblPasajero);
            groupBoxTerminal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBoxTerminal.ForeColor = Color.FromArgb(0, 102, 204);
            groupBoxTerminal.Location = new Point(20, 70);
            groupBoxTerminal.Name = "groupBoxTerminal";
            groupBoxTerminal.Size = new Size(520, 430);
            groupBoxTerminal.TabIndex = 0;
            groupBoxTerminal.TabStop = false;
            groupBoxTerminal.Text = "Terminal Obelisco";
            // 
            // lblTiempoRestante
            // 
            lblTiempoRestante.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTiempoRestante.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTiempoRestante.ForeColor = Color.FromArgb(220, 20, 60);
            lblTiempoRestante.Location = new Point(360, 25);
            lblTiempoRestante.Name = "lblTiempoRestante";
            lblTiempoRestante.Size = new Size(140, 30);
            lblTiempoRestante.TabIndex = 9;
            lblTiempoRestante.Text = "20:00";
            lblTiempoRestante.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTiempo
            // 
            lblTiempo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTiempo.AutoSize = true;
            lblTiempo.Font = new Font("Segoe UI", 9F);
            lblTiempo.ForeColor = Color.FromArgb(0, 102, 204);
            lblTiempo.Location = new Point(340, 30);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(50, 15);
            lblTiempo.TabIndex = 8;
            lblTiempo.Text = "Tiempo:";
            // 
            // cmbTipoPasajero
            // 
            cmbTipoPasajero.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbTipoPasajero.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoPasajero.Font = new Font("Segoe UI", 10F);
            cmbTipoPasajero.FormattingEnabled = true;
            cmbTipoPasajero.Location = new Point(115, 100);
            cmbTipoPasajero.Name = "cmbTipoPasajero";
            cmbTipoPasajero.Size = new Size(200, 25);
            cmbTipoPasajero.TabIndex = 7;
            cmbTipoPasajero.SelectedIndexChanged += cmbTipoPasajero_SelectedIndexChanged;
            // 
            // lblTipoPasajero
            // 
            lblTipoPasajero.AutoSize = true;
            lblTipoPasajero.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTipoPasajero.ForeColor = Color.FromArgb(0, 102, 204);
            lblTipoPasajero.Location = new Point(30, 103);
            lblTipoPasajero.Name = "lblTipoPasajero";
            lblTipoPasajero.Size = new Size(43, 19);
            lblTipoPasajero.TabIndex = 6;
            lblTipoPasajero.Text = "Tipo:";
            // 
            // lstEnEspera
            // 
            lstEnEspera.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstEnEspera.Font = new Font("Segoe UI", 9F);
            lstEnEspera.ForeColor = Color.Black;
            lstEnEspera.FormattingEnabled = true;
            lstEnEspera.ItemHeight = 15;
            lstEnEspera.Location = new Point(30, 185);
            lstEnEspera.Name = "lstEnEspera";
            lstEnEspera.Size = new Size(460, 154);
            lstEnEspera.TabIndex = 5;
            // 
            // btnSubir
            // 
            btnSubir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnSubir.BackColor = Color.FromArgb(46, 125, 50);
            btnSubir.Cursor = Cursors.Hand;
            btnSubir.FlatStyle = FlatStyle.Flat;
            btnSubir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSubir.ForeColor = Color.White;
            btnSubir.Location = new Point(30, 355);
            btnSubir.Name = "btnSubir";
            btnSubir.Size = new Size(460, 50);
            btnSubir.TabIndex = 4;
            btnSubir.Text = ">> Subir a la combi (Iniciar Viaje)";
            btnSubir.UseVisualStyleBackColor = false;
            btnSubir.Click += btnSubir_Click;
            // 
            // btnAnotar
            // 
            btnAnotar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAnotar.BackColor = Color.FromArgb(0, 102, 204);
            btnAnotar.Cursor = Cursors.Hand;
            btnAnotar.FlatStyle = FlatStyle.Flat;
            btnAnotar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAnotar.ForeColor = Color.White;
            btnAnotar.Location = new Point(330, 67);
            btnAnotar.Name = "btnAnotar";
            btnAnotar.Size = new Size(160, 58);
            btnAnotar.TabIndex = 3;
            btnAnotar.Text = "Anotar";
            btnAnotar.UseVisualStyleBackColor = false;
            btnAnotar.Click += btnAnotar_Click;
            // 
            // txtPasajero
            // 
            txtPasajero.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPasajero.Font = new Font("Segoe UI", 10F);
            txtPasajero.ForeColor = Color.Black;
            txtPasajero.Location = new Point(115, 67);
            txtPasajero.Name = "txtPasajero";
            txtPasajero.Size = new Size(200, 25);
            txtPasajero.TabIndex = 2;
            // 
            // lblEnEspera
            // 
            lblEnEspera.AutoSize = true;
            lblEnEspera.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEnEspera.ForeColor = Color.FromArgb(0, 102, 204);
            lblEnEspera.Location = new Point(30, 155);
            lblEnEspera.Name = "lblEnEspera";
            lblEnEspera.Size = new Size(76, 19);
            lblEnEspera.TabIndex = 1;
            lblEnEspera.Text = "En Espera:";
            // 
            // lblPasajero
            // 
            lblPasajero.AutoSize = true;
            lblPasajero.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPasajero.ForeColor = Color.FromArgb(0, 102, 204);
            lblPasajero.Location = new Point(30, 70);
            lblPasajero.Name = "lblPasajero";
            lblPasajero.Size = new Size(71, 19);
            lblPasajero.TabIndex = 0;
            lblPasajero.Text = "Pasajero:";
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(0, 102, 204);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(720, 40);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "SERVICIO DE COMBIS - Terminal Obelisco";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBoxEstadisticas
            // 
            groupBoxEstadisticas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBoxEstadisticas.BackColor = Color.FromArgb(255, 250, 240);
            groupBoxEstadisticas.Controls.Add(lblRecaudacion);
            groupBoxEstadisticas.Controls.Add(lblPasajerosHoy);
            groupBoxEstadisticas.Controls.Add(lblViajesHoy);
            groupBoxEstadisticas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBoxEstadisticas.ForeColor = Color.FromArgb(184, 134, 11);
            groupBoxEstadisticas.Location = new Point(560, 70);
            groupBoxEstadisticas.Name = "groupBoxEstadisticas";
            groupBoxEstadisticas.Size = new Size(180, 150);
            groupBoxEstadisticas.TabIndex = 2;
            groupBoxEstadisticas.TabStop = false;
            groupBoxEstadisticas.Text = "ESTADISTICAS";
            // 
            // lblRecaudacion
            // 
            lblRecaudacion.Font = new Font("Segoe UI", 9F);
            lblRecaudacion.ForeColor = Color.Black;
            lblRecaudacion.Location = new Point(15, 100);
            lblRecaudacion.Name = "lblRecaudacion";
            lblRecaudacion.Size = new Size(150, 35);
            lblRecaudacion.TabIndex = 2;
            lblRecaudacion.Text = "Recaudacion:\r\n$0.00";
            // 
            // lblPasajerosHoy
            // 
            lblPasajerosHoy.Font = new Font("Segoe UI", 9F);
            lblPasajerosHoy.ForeColor = Color.Black;
            lblPasajerosHoy.Location = new Point(15, 65);
            lblPasajerosHoy.Name = "lblPasajerosHoy";
            lblPasajerosHoy.Size = new Size(150, 20);
            lblPasajerosHoy.TabIndex = 1;
            lblPasajerosHoy.Text = "Pasajeros: 0";
            // 
            // lblViajesHoy
            // 
            lblViajesHoy.Font = new Font("Segoe UI", 9F);
            lblViajesHoy.ForeColor = Color.Black;
            lblViajesHoy.Location = new Point(15, 35);
            lblViajesHoy.Name = "lblViajesHoy";
            lblViajesHoy.Size = new Size(150, 20);
            lblViajesHoy.TabIndex = 0;
            lblViajesHoy.Text = "Viajes: 0";
            // 
            // btnReporte
            // 
            btnReporte.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnReporte.BackColor = Color.FromArgb(255, 152, 0);
            btnReporte.Cursor = Cursors.Hand;
            btnReporte.FlatStyle = FlatStyle.Flat;
            btnReporte.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReporte.ForeColor = Color.White;
            btnReporte.Location = new Point(560, 240);
            btnReporte.Name = "btnReporte";
            btnReporte.Size = new Size(180, 50);
            btnReporte.TabIndex = 3;
            btnReporte.Text = "Generar Reporte\r\nDel Dia";
            btnReporte.UseVisualStyleBackColor = false;
            btnReporte.Click += btnReporte_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCerrar.BackColor = Color.FromArgb(211, 47, 47);
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(560, 450);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(180, 50);
            btnCerrar.TabIndex = 4;
            btnCerrar.Text = "Cerrar Aplicacion";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // timerCombi
            // 
            timerCombi.Interval = 1000;
            timerCombi.Tick += timerCombi_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(760, 520);
            Controls.Add(btnCerrar);
            Controls.Add(btnReporte);
            Controls.Add(groupBoxEstadisticas);
            Controls.Add(lblTitulo);
            Controls.Add(groupBoxTerminal);
            MinimumSize = new Size(776, 559);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Servicio de Combis - TP 12 Integración";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            groupBoxTerminal.ResumeLayout(false);
            groupBoxTerminal.PerformLayout();
            groupBoxEstadisticas.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxTerminal;
        private Label lblPasajero;
        private Label lblEnEspera;
        private TextBox txtPasajero;
        private Button btnAnotar;
        private Button btnSubir;
        private ListBox lstEnEspera;
        private Label lblTitulo;
        private Label lblTipoPasajero;
        private ComboBox cmbTipoPasajero;
        private Label lblTiempo;
        private Label lblTiempoRestante;
        private GroupBox groupBoxEstadisticas;
        private Label lblViajesHoy;
        private Label lblPasajerosHoy;
        private Label lblRecaudacion;
        private Button btnReporte;
        private Button btnCerrar;
        private System.Windows.Forms.Timer timerCombi;
    }
}
