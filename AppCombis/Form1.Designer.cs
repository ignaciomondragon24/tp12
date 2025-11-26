namespace AppCombis
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
            // Controles principales
            lblTitulo = new Label();
            
            // Panel izquierdo - Combis
            groupBoxCombis = new GroupBox();
            lstCombis = new ListBox();
            btnNuevaCombi = new Button();
            btnEliminarCombi = new Button();
            
            // Panel central - Pasajeros
            groupBoxPasajeros = new GroupBox();
            lblInfoCombi = new Label();
            lstPasajeros = new ListBox();
            btnQuitarPasajero = new Button();
            btnIniciarViaje = new Button();
            
            // Panel derecho - Agregar pasajero
            groupBoxAgregar = new GroupBox();
            lblNombrePasajero = new Label();
            txtPasajero = new TextBox();
            lblTipoPasajero = new Label();
            cmbTipoPasajero = new ComboBox();
            btnAgregarPasajero = new Button();
            
            // Panel inferior - Estadísticas y acciones
            groupBoxEstadisticas = new GroupBox();
            lblViajesHoy = new Label();
            lblPasajerosHoy = new Label();
            lblRecaudacion = new Label();
            btnReporte = new Button();
            btnCerrar = new Button();
            
            // Timer
            timerCombis = new System.Windows.Forms.Timer(components);
            
            groupBoxCombis.SuspendLayout();
            groupBoxPasajeros.SuspendLayout();
            groupBoxAgregar.SuspendLayout();
            groupBoxEstadisticas.SuspendLayout();
            SuspendLayout();
            
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(0, 102, 204);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(950, 35);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "SISTEMA DE GESTION DE COMBIS - Terminal Obelisco";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            
            // 
            // groupBoxCombis
            // 
            groupBoxCombis.BackColor = Color.FromArgb(240, 248, 255);
            groupBoxCombis.Controls.Add(lstCombis);
            groupBoxCombis.Controls.Add(btnNuevaCombi);
            groupBoxCombis.Controls.Add(btnEliminarCombi);
            groupBoxCombis.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBoxCombis.ForeColor = Color.FromArgb(0, 102, 204);
            groupBoxCombis.Location = new Point(10, 45);
            groupBoxCombis.Name = "groupBoxCombis";
            groupBoxCombis.Size = new Size(220, 380);
            groupBoxCombis.TabIndex = 1;
            groupBoxCombis.TabStop = false;
            groupBoxCombis.Text = "COMBIS";
            
            // 
            // lstCombis
            // 
            lstCombis.Font = new Font("Segoe UI", 8F);
            lstCombis.FormattingEnabled = true;
            lstCombis.ItemHeight = 13;
            lstCombis.Location = new Point(10, 22);
            lstCombis.Name = "lstCombis";
            lstCombis.Size = new Size(200, 290);
            lstCombis.TabIndex = 0;
            lstCombis.SelectionMode = SelectionMode.One;
            lstCombis.SelectedIndexChanged += lstCombis_SelectedIndexChanged;
            
            // 
            // btnNuevaCombi
            // 
            btnNuevaCombi.BackColor = Color.FromArgb(46, 125, 50);
            btnNuevaCombi.Cursor = Cursors.Hand;
            btnNuevaCombi.FlatStyle = FlatStyle.Flat;
            btnNuevaCombi.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnNuevaCombi.ForeColor = Color.White;
            btnNuevaCombi.Location = new Point(10, 320);
            btnNuevaCombi.Name = "btnNuevaCombi";
            btnNuevaCombi.Size = new Size(200, 25);
            btnNuevaCombi.TabIndex = 1;
            btnNuevaCombi.Text = "+ Nueva Combi";
            btnNuevaCombi.UseVisualStyleBackColor = false;
            btnNuevaCombi.Click += btnNuevaCombi_Click;
            
            // 
            // btnEliminarCombi
            // 
            btnEliminarCombi.BackColor = Color.FromArgb(211, 47, 47);
            btnEliminarCombi.Cursor = Cursors.Hand;
            btnEliminarCombi.FlatStyle = FlatStyle.Flat;
            btnEliminarCombi.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnEliminarCombi.ForeColor = Color.White;
            btnEliminarCombi.Location = new Point(10, 350);
            btnEliminarCombi.Name = "btnEliminarCombi";
            btnEliminarCombi.Size = new Size(200, 25);
            btnEliminarCombi.TabIndex = 2;
            btnEliminarCombi.Text = "- Eliminar";
            btnEliminarCombi.UseVisualStyleBackColor = false;
            btnEliminarCombi.Click += btnEliminarCombi_Click;
            
            // 
            // groupBoxPasajeros
            // 
            groupBoxPasajeros.BackColor = Color.FromArgb(255, 250, 240);
            groupBoxPasajeros.Controls.Add(lblInfoCombi);
            groupBoxPasajeros.Controls.Add(lstPasajeros);
            groupBoxPasajeros.Controls.Add(btnQuitarPasajero);
            groupBoxPasajeros.Controls.Add(btnIniciarViaje);
            groupBoxPasajeros.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBoxPasajeros.ForeColor = Color.FromArgb(184, 134, 11);
            groupBoxPasajeros.Location = new Point(240, 45);
            groupBoxPasajeros.Name = "groupBoxPasajeros";
            groupBoxPasajeros.Size = new Size(420, 380);
            groupBoxPasajeros.TabIndex = 2;
            groupBoxPasajeros.TabStop = false;
            groupBoxPasajeros.Text = "PASAJEROS EN ESPERA";
            
            // 
            // lblInfoCombi
            // 
            lblInfoCombi.Font = new Font("Segoe UI", 8F);
            lblInfoCombi.ForeColor = Color.Black;
            lblInfoCombi.Location = new Point(10, 20);
            lblInfoCombi.Name = "lblInfoCombi";
            lblInfoCombi.Size = new Size(400, 45);
            lblInfoCombi.TabIndex = 0;
            lblInfoCombi.Text = "Seleccione una combi";
            
            // 
            // lstPasajeros
            // 
            lstPasajeros.Font = new Font("Segoe UI", 8F);
            lstPasajeros.FormattingEnabled = true;
            lstPasajeros.ItemHeight = 13;
            lstPasajeros.Location = new Point(10, 70);
            lstPasajeros.Name = "lstPasajeros";
            lstPasajeros.Size = new Size(400, 221);
            lstPasajeros.TabIndex = 1;
            lstPasajeros.SelectionMode = SelectionMode.One;
            
            // 
            // btnQuitarPasajero
            // 
            btnQuitarPasajero.BackColor = Color.FromArgb(255, 152, 0);
            btnQuitarPasajero.Cursor = Cursors.Hand;
            btnQuitarPasajero.FlatStyle = FlatStyle.Flat;
            btnQuitarPasajero.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnQuitarPasajero.ForeColor = Color.White;
            btnQuitarPasajero.Location = new Point(10, 300);
            btnQuitarPasajero.Name = "btnQuitarPasajero";
            btnQuitarPasajero.Size = new Size(400, 30);
            btnQuitarPasajero.TabIndex = 2;
            btnQuitarPasajero.Text = "- Quitar Pasajero Seleccionado";
            btnQuitarPasajero.UseVisualStyleBackColor = false;
            btnQuitarPasajero.Click += btnQuitarPasajero_Click;
            
            // 
            // btnIniciarViaje
            // 
            btnIniciarViaje.BackColor = Color.FromArgb(46, 125, 50);
            btnIniciarViaje.Cursor = Cursors.Hand;
            btnIniciarViaje.FlatStyle = FlatStyle.Flat;
            btnIniciarViaje.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnIniciarViaje.ForeColor = Color.White;
            btnIniciarViaje.Location = new Point(10, 335);
            btnIniciarViaje.Name = "btnIniciarViaje";
            btnIniciarViaje.Size = new Size(400, 35);
            btnIniciarViaje.TabIndex = 3;
            btnIniciarViaje.Text = ">> INICIAR VIAJE";
            btnIniciarViaje.UseVisualStyleBackColor = false;
            btnIniciarViaje.Click += btnIniciarViaje_Click;
            
            // 
            // groupBoxAgregar
            // 
            groupBoxAgregar.BackColor = Color.FromArgb(240, 255, 240);
            groupBoxAgregar.Controls.Add(lblNombrePasajero);
            groupBoxAgregar.Controls.Add(txtPasajero);
            groupBoxAgregar.Controls.Add(lblTipoPasajero);
            groupBoxAgregar.Controls.Add(cmbTipoPasajero);
            groupBoxAgregar.Controls.Add(btnAgregarPasajero);
            groupBoxAgregar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBoxAgregar.ForeColor = Color.FromArgb(34, 139, 34);
            groupBoxAgregar.Location = new Point(670, 45);
            groupBoxAgregar.Name = "groupBoxAgregar";
            groupBoxAgregar.Size = new Size(270, 200);
            groupBoxAgregar.TabIndex = 3;
            groupBoxAgregar.TabStop = false;
            groupBoxAgregar.Text = "AGREGAR PASAJERO";
            
            // 
            // lblNombrePasajero
            // 
            lblNombrePasajero.AutoSize = true;
            lblNombrePasajero.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblNombrePasajero.Location = new Point(10, 25);
            lblNombrePasajero.Name = "lblNombrePasajero";
            lblNombrePasajero.Size = new Size(50, 13);
            lblNombrePasajero.TabIndex = 0;
            lblNombrePasajero.Text = "Nombre:";
            
            // 
            // txtPasajero
            // 
            txtPasajero.Font = new Font("Segoe UI", 9F);
            txtPasajero.Location = new Point(10, 42);
            txtPasajero.Name = "txtPasajero";
            txtPasajero.Size = new Size(250, 23);
            txtPasajero.TabIndex = 1;
            
            // 
            // lblTipoPasajero
            // 
            lblTipoPasajero.AutoSize = true;
            lblTipoPasajero.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblTipoPasajero.Location = new Point(10, 75);
            lblTipoPasajero.Name = "lblTipoPasajero";
            lblTipoPasajero.Size = new Size(32, 13);
            lblTipoPasajero.TabIndex = 2;
            lblTipoPasajero.Text = "Tipo:";
            
            // 
            // cmbTipoPasajero
            // 
            cmbTipoPasajero.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoPasajero.Font = new Font("Segoe UI", 9F);
            cmbTipoPasajero.FormattingEnabled = true;
            cmbTipoPasajero.Location = new Point(10, 92);
            cmbTipoPasajero.Name = "cmbTipoPasajero";
            cmbTipoPasajero.Size = new Size(250, 23);
            cmbTipoPasajero.TabIndex = 3;
            
            // 
            // btnAgregarPasajero
            // 
            btnAgregarPasajero.BackColor = Color.FromArgb(0, 102, 204);
            btnAgregarPasajero.Cursor = Cursors.Hand;
            btnAgregarPasajero.FlatStyle = FlatStyle.Flat;
            btnAgregarPasajero.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAgregarPasajero.ForeColor = Color.White;
            btnAgregarPasajero.Location = new Point(10, 130);
            btnAgregarPasajero.Name = "btnAgregarPasajero";
            btnAgregarPasajero.Size = new Size(250, 55);
            btnAgregarPasajero.TabIndex = 4;
            btnAgregarPasajero.Text = "+ AGREGAR PASAJERO\na Combi Seleccionada";
            btnAgregarPasajero.UseVisualStyleBackColor = false;
            btnAgregarPasajero.Click += btnAgregarPasajero_Click;
            
            // 
            // groupBoxEstadisticas
            // 
            groupBoxEstadisticas.BackColor = Color.FromArgb(255, 248, 240);
            groupBoxEstadisticas.Controls.Add(lblViajesHoy);
            groupBoxEstadisticas.Controls.Add(lblPasajerosHoy);
            groupBoxEstadisticas.Controls.Add(lblRecaudacion);
            groupBoxEstadisticas.Controls.Add(btnReporte);
            groupBoxEstadisticas.Controls.Add(btnCerrar);
            groupBoxEstadisticas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBoxEstadisticas.ForeColor = Color.FromArgb(184, 134, 11);
            groupBoxEstadisticas.Location = new Point(670, 255);
            groupBoxEstadisticas.Name = "groupBoxEstadisticas";
            groupBoxEstadisticas.Size = new Size(270, 170);
            groupBoxEstadisticas.TabIndex = 4;
            groupBoxEstadisticas.TabStop = false;
            groupBoxEstadisticas.Text = "ESTADISTICAS DEL DIA";
            
            // 
            // lblViajesHoy
            // 
            lblViajesHoy.Font = new Font("Segoe UI", 8F);
            lblViajesHoy.ForeColor = Color.Black;
            lblViajesHoy.Location = new Point(10, 20);
            lblViajesHoy.Name = "lblViajesHoy";
            lblViajesHoy.Size = new Size(250, 15);
            lblViajesHoy.TabIndex = 0;
            lblViajesHoy.Text = "Viajes: 0";
            
            // 
            // lblPasajerosHoy
            // 
            lblPasajerosHoy.Font = new Font("Segoe UI", 8F);
            lblPasajerosHoy.ForeColor = Color.Black;
            lblPasajerosHoy.Location = new Point(10, 38);
            lblPasajerosHoy.Name = "lblPasajerosHoy";
            lblPasajerosHoy.Size = new Size(250, 15);
            lblPasajerosHoy.TabIndex = 1;
            lblPasajerosHoy.Text = "Pasajeros: 0";
            
            // 
            // lblRecaudacion
            // 
            lblRecaudacion.Font = new Font("Segoe UI", 8F);
            lblRecaudacion.ForeColor = Color.Black;
            lblRecaudacion.Location = new Point(10, 56);
            lblRecaudacion.Name = "lblRecaudacion";
            lblRecaudacion.Size = new Size(250, 15);
            lblRecaudacion.TabIndex = 2;
            lblRecaudacion.Text = "Recaudacion: $0.00";
            
            // 
            // btnReporte
            // 
            btnReporte.BackColor = Color.FromArgb(255, 152, 0);
            btnReporte.Cursor = Cursors.Hand;
            btnReporte.FlatStyle = FlatStyle.Flat;
            btnReporte.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnReporte.ForeColor = Color.White;
            btnReporte.Location = new Point(10, 85);
            btnReporte.Name = "btnReporte";
            btnReporte.Size = new Size(250, 35);
            btnReporte.TabIndex = 3;
            btnReporte.Text = "Generar Reporte del Dia";
            btnReporte.UseVisualStyleBackColor = false;
            btnReporte.Click += btnReporte_Click;
            
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.FromArgb(120, 120, 120);
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(10, 125);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(250, 35);
            btnCerrar.TabIndex = 4;
            btnCerrar.Text = "Cerrar Aplicacion";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            
            // 
            // timerCombis
            // 
            timerCombis.Interval = 1000;
            timerCombis.Tick += timerCombis_Tick;
            
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(950, 435);
            Controls.Add(groupBoxEstadisticas);
            Controls.Add(groupBoxAgregar);
            Controls.Add(groupBoxPasajeros);
            Controls.Add(groupBoxCombis);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Combis - Gestion Integrada";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            
            groupBoxCombis.ResumeLayout(false);
            groupBoxPasajeros.ResumeLayout(false);
            groupBoxAgregar.ResumeLayout(false);
            groupBoxAgregar.PerformLayout();
            groupBoxEstadisticas.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        
        // Panel combis
        private GroupBox groupBoxCombis;
        private ListBox lstCombis;
        private Button btnNuevaCombi;
        private Button btnEliminarCombi;
        
        // Panel pasajeros
        private GroupBox groupBoxPasajeros;
        private Label lblInfoCombi;
        private ListBox lstPasajeros;
        private Button btnQuitarPasajero;
        private Button btnIniciarViaje;
        
        // Panel agregar
        private GroupBox groupBoxAgregar;
        private Label lblNombrePasajero;
        private TextBox txtPasajero;
        private Label lblTipoPasajero;
        private ComboBox cmbTipoPasajero;
        private Button btnAgregarPasajero;
        
        // Panel estadísticas
        private GroupBox groupBoxEstadisticas;
        private Label lblViajesHoy;
        private Label lblPasajerosHoy;
        private Label lblRecaudacion;
        private Button btnReporte;
        private Button btnCerrar;
        
        // Timer
        private System.Windows.Forms.Timer timerCombis;
    }
}
