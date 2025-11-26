namespace AppCombis
{
    public partial class Form1 : Form
    {
        // Lista de todas las combis
        private List<Combi> combis = new List<Combi>();

        // Combi actualmente seleccionada
        private Combi? combiSeleccionada = null;

        // Estadísticas globales
        private EstadisticasDiarias estadisticas = new EstadisticasDiarias();

        // Archivos para persistencia
        private const string ARCHIVO_COMBIS = "combis.txt";
        private const string ARCHIVO_PASAJEROS_COMBIS = "pasajeros_combis.txt";
        private const string ARCHIVO_ESTADISTICAS = "estadisticas.txt";

        public Form1()
        {
            InitializeComponent();
        }

        // ============================================
        // CARGA INICIAL
        // ============================================

        private void Form1_Load(object sender, EventArgs e)
        {
            // Inicializo tipos de pasajero
            InicializarTiposPasajero();

            // Cargo datos guardados
            CargarCombis();
            CargarEstadisticas();

            // Si no hay combis, creo 2 por defecto
            if (combis.Count == 0)
            {
                combis.Add(new Combi(1, "Combi 1", "Puerto Madero (20 min)", 19));
                combis.Add(new Combi(2, "Combi 2", "Recoleta (25 min)", 19));
            }

            // Actualizo interfaz
            ActualizarListaCombis();
            ActualizarEstadisticas();

            // Arranco el timer
            timerCombis.Start();
        }

        private void InicializarTiposPasajero()
        {
            cmbTipoPasajero.Items.Clear();
            cmbTipoPasajero.Items.Add("[N] Normal - $500");
            cmbTipoPasajero.Items.Add("[E] Estudiante - $250");
            cmbTipoPasajero.Items.Add("[J] Jubilado - Gratis");
            cmbTipoPasajero.SelectedIndex = 0;
        }

        // ============================================
        // GESTIÓN DE COMBIS
        // ============================================

        private void btnNuevaCombi_Click(object sender, EventArgs e)
        {
            // Creo formulario personalizado
            var formNueva = new Form
            {
                Text = "Nueva Combi",
                Width = 400,
                Height = 280,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Nombre
            var lblNombre = new Label
            {
                Text = "Nombre de la combi:",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            var txtNombre = new TextBox
            {
                Location = new Point(20, 45),
                Width = 340,
                Text = $"Combi {combis.Count + 1}",
                Font = new Font("Segoe UI", 10F)
            };

            // Destino
            var lblDestino = new Label
            {
                Text = "Destino:",
                Location = new Point(20, 80),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            var cmbDestino = new ComboBox
            {
                Location = new Point(20, 105),
                Width = 340,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cmbDestino.Items.AddRange(Combi.DestinosDisponibles);
            cmbDestino.SelectedIndex = 0;

            // Capacidad
            var lblCapacidad = new Label
            {
                Text = "Capacidad (pasajeros):",
                Location = new Point(20, 140),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            var numCapacidad = new NumericUpDown
            {
                Location = new Point(20, 165),
                Width = 100,
                Minimum = 1,
                Maximum = 50,
                Value = 19,
                Font = new Font("Segoe UI", 10F)
            };

            // Botones
            var btnOk = new Button
            {
                Text = "Crear",
                Location = new Point(180, 200),
                Width = 90,
                Height = 35,
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            var btnCancel = new Button
            {
                Text = "Cancelar",
                Location = new Point(280, 200),
                Width = 90,
                Height = 35,
                DialogResult = DialogResult.Cancel,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            // Agrego controles
            formNueva.Controls.AddRange(new Control[] {
                lblNombre, txtNombre,
                lblDestino, cmbDestino,
                lblCapacidad, numCapacidad,
                btnOk, btnCancel
            });

            formNueva.AcceptButton = btnOk;
            formNueva.CancelButton = btnCancel;

            // Muestro el formulario
            if (formNueva.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese un nombre para la combi.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int numeroCombi = combis.Count > 0 ? combis.Max(c => c.NumeroCombi) + 1 : 1;
                var nuevaCombi = new Combi(
                    numeroCombi,
                    txtNombre.Text,
                    cmbDestino.SelectedItem?.ToString() ?? "Puerto Madero (20 min)",
                    (int)numCapacidad.Value
                );

                combis.Add(nuevaCombi);
                ActualizarListaCombis();

                MessageBox.Show(
                    $"Combi creada exitosamente!\n\n" +
                    $"Nombre: {nuevaCombi.Nombre}\n" +
                    $"Destino: {nuevaCombi.Destino}\n" +
                    $"Capacidad: {nuevaCombi.Capacidad} pasajeros",
                    "Combi Creada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnEliminarCombi_Click(object sender, EventArgs e)
        {
            if (combiSeleccionada == null)
            {
                MessageBox.Show("Seleccione una combi para eliminar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (combiSeleccionada.FilaDeEspera.Count > 0)
            {
                var resultado = MessageBox.Show(
                    $"La combi '{combiSeleccionada.Nombre}' tiene {combiSeleccionada.FilaDeEspera.Count} pasajeros.\n" +
                    "¿Está seguro que desea eliminarla?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.No)
                    return;
            }

            combis.Remove(combiSeleccionada);
            combiSeleccionada = null;

            ActualizarListaCombis();
            ActualizarListaPasajeros();

            MessageBox.Show("Combi eliminada exitosamente.", "Eliminada",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lstCombis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCombis.SelectedIndex >= 0 && lstCombis.SelectedIndex < combis.Count)
            {
                combiSeleccionada = combis[lstCombis.SelectedIndex];
                ActualizarListaPasajeros();
            }
        }

        // ============================================
        // GESTIÓN DE PASAJEROS
        // ============================================

        private void btnAgregarPasajero_Click(object sender, EventArgs e)
        {
            if (combiSeleccionada == null)
            {
                MessageBox.Show("Seleccione una combi primero.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtPasajero.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Ingrese el nombre del pasajero.", "Campo vacio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPasajero.Focus();
                return;
            }

            int cantidadPasajeros = (int)numCantidadPasajeros.Value;
            
            // Verifico capacidad disponible
            int lugaresDisponibles = combiSeleccionada.Capacidad - combiSeleccionada.FilaDeEspera.Count;
            if (cantidadPasajeros > lugaresDisponibles)
            {
                MessageBox.Show(
                    $"No hay suficiente espacio en la combi '{combiSeleccionada.Nombre}'.\n\n" +
                    $"Lugares disponibles: {lugaresDisponibles}\n" +
                    $"Pasajeros solicitados: {cantidadPasajeros}\n\n" +
                    $"Capacidad total: {combiSeleccionada.Capacidad}",
                    "Capacidad insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Pasajero.TipoPasajero tipo = cmbTipoPasajero.SelectedIndex switch
            {
                0 => Pasajero.TipoPasajero.Normal,
                1 => Pasajero.TipoPasajero.Estudiante,
                2 => Pasajero.TipoPasajero.Jubilado,
                _ => Pasajero.TipoPasajero.Normal
            };

            // Contador de pasajeros agregados exitosamente
            int agregados = 0;
            bool esPrimerPasajero = combiSeleccionada.FilaDeEspera.Count == 0;

            // Agrego el pasajero principal (quien reserva)
            var pasajeroPrincipal = new Pasajero(nombre, tipo, esReservaPrincipal: true);
            if (combiSeleccionada.AgregarPasajero(pasajeroPrincipal))
            {
                agregados++;
            }

            // Agrego los acompañantes (si los hay)
            for (int i = 1; i < cantidadPasajeros; i++)
            {
                var acompanante = new Pasajero(
                    nombre: nombre,  // Mismo nombre base
                    tipo: tipo,      // Mismo tipo
                    esReservaPrincipal: false,
                    nombreReservante: nombre,
                    numeroAcompanante: i
                );

                if (combiSeleccionada.AgregarPasajero(acompanante))
                {
                    agregados++;
                }
                else
                {
                    break; // Si no se pudo agregar, salgo del loop
                }
            }

            // Limpio el formulario
            txtPasajero.Clear();
            numCantidadPasajeros.Value = 1;
            txtPasajero.Focus();

            // Actualizo las listas
            ActualizarListaCombis();
            ActualizarListaPasajeros();

            // Muestro mensaje de confirmación
            if (agregados == cantidadPasajeros)
            {
                decimal recaudacionTotal = agregados * pasajeroPrincipal.Tarifa;
                
                string mensaje;
                if (cantidadPasajeros == 1)
                {
                    // Mensaje para un solo pasajero
                    mensaje = $"Pasajero agregado\n\n" +
                             $"Nombre: {pasajeroPrincipal.Nombre}\n" +
                             $"Tipo: {pasajeroPrincipal.ObtenerDescripcionTipo()}\n" +
                             $"Tarifa: ${pasajeroPrincipal.Tarifa}\n\n" +
                             $"Combi: {combiSeleccionada.Nombre}\n" +
                             $"Pasajeros en combi: {combiSeleccionada.FilaDeEspera.Count}/{combiSeleccionada.Capacidad}";
                }
                else
                {
                    // Mensaje para reserva grupal
                    mensaje = $"RESERVA GRUPAL EXITOSA\n\n" +
                             $"Reservante: {nombre}\n" +
                             $"Tipo: {pasajeroPrincipal.ObtenerDescripcionTipo()}\n" +
                             $"Cantidad total: {cantidadPasajeros} personas\n" +
                             $"  - 1 pasajero principal: {nombre}\n" +
                             $"  - {cantidadPasajeros - 1} acompañante(s): {nombre} #1, #2...\n\n" +
                             $"Tarifa por persona: ${pasajeroPrincipal.Tarifa}\n" +
                             $"Total a pagar: ${recaudacionTotal}\n\n" +
                             $"Combi: {combiSeleccionada.Nombre}\n" +
                             $"Pasajeros en combi: {combiSeleccionada.FilaDeEspera.Count}/{combiSeleccionada.Capacidad}";
                }

                // Si es el primer pasajero, agrego info del timer
                if (esPrimerPasajero)
                {
                    mensaje = "PRIMER PASAJERO - TIMER INICIADO\n" +
                             "Timer: 20:00 minutos\n\n" + mensaje;
                }

                MessageBox.Show(mensaje,
                    cantidadPasajeros == 1 ? "Pasajero Agregado" : "Reserva Grupal",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    $"Solo se pudieron agregar {agregados} de {cantidadPasajeros} pasajeros.\n" +
                    $"La combi no tiene suficiente capacidad.",
                    "Agregado parcial",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnQuitarPasajero_Click(object sender, EventArgs e)
        {
            if (combiSeleccionada == null)
            {
                MessageBox.Show("Seleccione una combi primero.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lstPasajeros.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un pasajero para quitar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pasajeros = combiSeleccionada.FilaDeEspera.ToArray();
            if (lstPasajeros.SelectedIndex >= pasajeros.Length)
                return;

            var pasajero = pasajeros[lstPasajeros.SelectedIndex];

            var resultado = MessageBox.Show(
                $"Quitar a '{pasajero.Nombre}' de la combi '{combiSeleccionada.Nombre}'?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                if (combiSeleccionada.QuitarPasajero(pasajero))
                {
                    ActualizarListaCombis();
                    ActualizarListaPasajeros();

                    MessageBox.Show($"Pasajero '{pasajero.Nombre}' quitado.", "Quitado",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnIniciarViaje_Click(object sender, EventArgs e)
        {
            if (combiSeleccionada == null)
            {
                MessageBox.Show("Seleccione una combi primero.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (combiSeleccionada.FilaDeEspera.Count == 0)
            {
                MessageBox.Show("No hay pasajeros en la combi seleccionada.", "Sin pasajeros",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var resultado = MessageBox.Show(
                $"Iniciar viaje?\n\n" +
                $"Combi: {combiSeleccionada.Nombre}\n" +
                $"Destino: {combiSeleccionada.Destino}\n" +
                $"Pasajeros: {combiSeleccionada.FilaDeEspera.Count}\n" +
                $"Recaudacion: ${combiSeleccionada.RecaudacionEnEspera}",
                "Confirmar Viaje",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                var pasajeros = combiSeleccionada.IniciarViaje();
                estadisticas.RegistrarViaje(pasajeros);

                MostrarDetallesViaje(pasajeros, combiSeleccionada.Nombre, combiSeleccionada.Destino);

                combiSeleccionada.FinalizarViaje();

                ActualizarListaCombis();
                ActualizarListaPasajeros();
                ActualizarEstadisticas();
            }
        }

        // ============================================
        // TIMER GLOBAL
        // ============================================

        private void timerCombis_Tick(object sender, EventArgs e)
        {
            // Actualizo el timer de cada combi
            List<Combi> combisAPartir = new List<Combi>();

            foreach (var combi in combis)
            {
                if (combi.ActualizarTimer())
                {
                    // Esta combi debe partir automáticamente
                    combisAPartir.Add(combi);
                }
            }

            // Actualizo la visualización
            ActualizarListaCombis();
            if (combiSeleccionada != null)
            {
                ActualizarListaPasajeros();
            }

            // Hago partir las combis que agotaron tiempo
            foreach (var combi in combisAPartir)
            {
                var pasajeros = combi.IniciarViaje();
                estadisticas.RegistrarViaje(pasajeros);

                MessageBox.Show(
                    $"TIEMPO AGOTADO (20 minutos)\n\n" +
                    $"Combi: {combi.Nombre}\n" +
                    $"Destino: {combi.Destino}\n" +
                    $"Pasajeros: {pasajeros.Count}\n" +
                    $"Recaudacion: ${pasajeros.Sum(p => p.Tarifa)}\n\n" +
                    $"La combi partio automaticamente.",
                    "Viaje Automatico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                combi.FinalizarViaje();
            }

            if (combisAPartir.Count > 0)
            {
                ActualizarListaCombis();
                ActualizarListaPasajeros();
                ActualizarEstadisticas();
            }
        }

        // ============================================
        // ACTUALIZACIÓN DE INTERFAZ
        // ============================================

        private void ActualizarListaCombis()
        {
            // Guardo el índice seleccionado y la combi actual
            int indiceSeleccionado = lstCombis.SelectedIndex;
            
            lstCombis.Items.Clear();

            foreach (var combi in combis)
            {
                lstCombis.Items.Add(combi.ToString());
            }

            // Restauro la selección
            if (indiceSeleccionado >= 0 && indiceSeleccionado < lstCombis.Items.Count)
            {
                lstCombis.SelectedIndex = indiceSeleccionado;
            }
        }

        private void ActualizarListaPasajeros()
        {
            // Guardo el índice seleccionado actualmente
            int indiceSeleccionado = lstPasajeros.SelectedIndex;
            
            lstPasajeros.Items.Clear();

            if (combiSeleccionada == null)
            {
                lblInfoCombi.Text = "Seleccione una combi de la lista";
                groupBoxPasajeros.Text = "PASAJEROS EN ESPERA";
                return;
            }

            var (normales, estudiantes, jubilados) = combiSeleccionada.ContarPorTipo();
            
            string estadoTexto = combiSeleccionada.Estado == Combi.EstadoCombi.EnEspera && combiSeleccionada.FilaDeEspera.Count > 0
                ? $"Timer: {combiSeleccionada.TiempoRestanteFormateado}"
                : "Esperando pasajeros";

            lblInfoCombi.Text =
                $"Combi: {combiSeleccionada.Nombre} | Destino: {combiSeleccionada.Destino}\n" +
                $"Pasajeros: {combiSeleccionada.FilaDeEspera.Count}/{combiSeleccionada.Capacidad} | " +
                $"Recaudacion: ${combiSeleccionada.RecaudacionEnEspera:N2}\n" +
                $"N:{normales} | E:{estudiantes} | J:{jubilados} | {estadoTexto}";

            groupBoxPasajeros.Text = $"PASAJEROS - {combiSeleccionada.Nombre}";

            if (combiSeleccionada.FilaDeEspera.Count == 0)
            {
                lstPasajeros.Items.Add("(No hay pasajeros)");
                return;
            }

            int pos = 1;
            foreach (var pasajero in combiSeleccionada.FilaDeEspera)
            {
                int minutosEspera = (int)(DateTime.Now - pasajero.HoraAnotacion).TotalMinutes;
                lstPasajeros.Items.Add($"{pos}. {pasajero} | Espera: {minutosEspera} min");
                pos++;
            }

            // Restauro la selección si todavía es válida
            if (indiceSeleccionado >= 0 && indiceSeleccionado < lstPasajeros.Items.Count)
            {
                lstPasajeros.SelectedIndex = indiceSeleccionado;
            }
        }

        private void ActualizarEstadisticas()
        {
            lblViajesHoy.Text = $"Viajes: {estadisticas.TotalViajes}";
            lblPasajerosHoy.Text = $"Pasajeros: {estadisticas.TotalPasajeros}";
            lblRecaudacion.Text = $"Recaudacion: ${estadisticas.RecaudacionTotal:N2}";
        }

        // ============================================
        // UTILIDADES
        // ============================================

        private void MostrarDetallesViaje(List<Pasajero> pasajeros, string nombreCombi, string destino)
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("=============================================");
            sb.AppendLine("         VIAJE INICIADO");
            sb.AppendLine("=============================================");
            sb.AppendLine();
            sb.AppendLine($"Combi: {nombreCombi}");
            sb.AppendLine($"Destino: {destino}");
            sb.AppendLine($"Hora salida: {DateTime.Now:HH:mm:ss}");
            sb.AppendLine($"Pasajeros: {pasajeros.Count}");
            sb.AppendLine();

            int normales = pasajeros.Count(p => p.Tipo == Pasajero.TipoPasajero.Normal);
            int estudiantes = pasajeros.Count(p => p.Tipo == Pasajero.TipoPasajero.Estudiante);
            int jubilados = pasajeros.Count(p => p.Tipo == Pasajero.TipoPasajero.Jubilado);

            sb.AppendLine("---------------------------------------------");
            sb.AppendLine(" DESGLOSE:");
            sb.AppendLine("---------------------------------------------");
            sb.AppendLine($" [N] Normales:    {normales}");
            sb.AppendLine($" [E] Estudiantes: {estudiantes}");
            sb.AppendLine($" [J] Jubilados:   {jubilados}");
            sb.AppendLine();

            decimal recaudacion = pasajeros.Sum(p => p.Tarifa);
            sb.AppendLine($" RECAUDACION: ${recaudacion:N2}");
            sb.AppendLine();
            
            sb.AppendLine(" PASAJEROS A BORDO:");
            int num = 1;
            foreach (var pasajero in pasajeros)
            {
                sb.AppendLine($" {num}. {pasajero}");
                num++;
            }
            
            sb.AppendLine();
            sb.AppendLine("=============================================");
            sb.AppendLine("   Buen viaje!");
            sb.AppendLine("=============================================");

            MessageBox.Show(sb.ToString(), "Detalles del Viaje",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ============================================
        // REPORTES Y ESTADÍSTICAS
        // ============================================

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreArchivo = $"Reporte_Combis_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                estadisticas.GuardarReporte(nombreArchivo);

                var resultado = MessageBox.Show(
                    $"REPORTE GENERADO\n\n{nombreArchivo}\n\nDesea abrirlo?",
                    "Reporte Generado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (resultado == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("notepad.exe", nombreArchivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el reporte:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================
        // PERSISTENCIA
        // ============================================

        private void CargarCombis()
        {
            try
            {
                if (!File.Exists(ARCHIVO_COMBIS))
                    return;

                combis.Clear();
                var lineas = File.ReadAllLines(ARCHIVO_COMBIS);
                foreach (var linea in lineas)
                {
                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        var combi = Combi.FromCsv(linea);
                        combis.Add(combi);
                    }
                }

                if (File.Exists(ARCHIVO_PASAJEROS_COMBIS))
                {
                    var lineasPasajeros = File.ReadAllLines(ARCHIVO_PASAJEROS_COMBIS);
                    foreach (var linea in lineasPasajeros)
                    {
                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            var partes = linea.Split('|');
                            if (partes.Length >= 2)
                            {
                                int numeroCombi = int.Parse(partes[0]);
                                string csvPasajero = partes[1];

                                var combi = combis.FirstOrDefault(c => c.NumeroCombi == numeroCombi);
                                if (combi != null)
                                {
                                    var pasajero = Pasajero.FromCsv(csvPasajero);
                                    combi.FilaDeEspera.Enqueue(pasajero);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void GuardarCombis()
        {
            try
            {
                var lineasCombis = new List<string>();
                foreach (var combi in combis)
                {
                    lineasCombis.Add(combi.ToCsv());
                }
                File.WriteAllLines(ARCHIVO_COMBIS, lineasCombis);

                var lineasPasajeros = new List<string>();
                foreach (var combi in combis)
                {
                    foreach (var pasajero in combi.FilaDeEspera)
                    {
                        lineasPasajeros.Add($"{combi.NumeroCombi}|{pasajero.ToCsv()}");
                    }
                }
                File.WriteAllLines(ARCHIVO_PASAJEROS_COMBIS, lineasPasajeros);
            }
            catch { }
        }

        private void CargarEstadisticas()
        {
            try
            {
                if (!File.Exists(ARCHIVO_ESTADISTICAS))
                    return;

                using (StreamReader lector = new StreamReader(ARCHIVO_ESTADISTICAS))
                {
                    string? linea;
                    while ((linea = lector.ReadLine()) != null)
                    {
                        string[] partes = linea.Split('|');
                        if (partes.Length >= 2)
                        {
                            switch (partes[0])
                            {
                                case "Fecha":
                                    DateTime fechaGuardada = DateTime.Parse(partes[1]);
                                    if (fechaGuardada.Date != DateTime.Today) return;
                                    break;
                                case "TotalViajes":
                                    estadisticas.TotalViajes = int.Parse(partes[1]);
                                    break;
                                case "TotalPasajeros":
                                    estadisticas.TotalPasajeros = int.Parse(partes[1]);
                                    break;
                                case "PasajerosNormales":
                                    estadisticas.PasajerosNormales = int.Parse(partes[1]);
                                    break;
                                case "PasajerosEstudiantes":
                                    estadisticas.PasajerosEstudiantes = int.Parse(partes[1]);
                                    break;
                                case "PasajerosJubilados":
                                    estadisticas.PasajerosJubilados = int.Parse(partes[1]);
                                    break;
                                case "RecaudacionTotal":
                                    estadisticas.RecaudacionTotal = decimal.Parse(partes[1]);
                                    break;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void GuardarEstadisticas()
        {
            try
            {
                using (StreamWriter escritor = new StreamWriter(ARCHIVO_ESTADISTICAS))
                {
                    escritor.WriteLine($"Fecha|{estadisticas.Fecha:yyyy-MM-dd}");
                    escritor.WriteLine($"TotalViajes|{estadisticas.TotalViajes}");
                    escritor.WriteLine($"TotalPasajeros|{estadisticas.TotalPasajeros}");
                    escritor.WriteLine($"PasajerosNormales|{estadisticas.PasajerosNormales}");
                    escritor.WriteLine($"PasajerosEstudiantes|{estadisticas.PasajerosEstudiantes}");
                    escritor.WriteLine($"PasajerosJubilados|{estadisticas.PasajerosJubilados}");
                    escritor.WriteLine($"RecaudacionTotal|{estadisticas.RecaudacionTotal}");
                }
            }
            catch { }
        }

        // ============================================
        // CIERRE
        // ============================================

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            int totalPasajeros = combis.Sum(c => c.FilaDeEspera.Count);

            if (totalPasajeros > 0)
            {
                var resultado = MessageBox.Show(
                    $"Hay {totalPasajeros} pasajeros esperando en las combis.\n" +
                    "¿Está seguro que desea cerrar?",
                    "Confirmar cierre",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            timerCombis.Stop();
            GuardarCombis();
            GuardarEstadisticas();
        }
    }
}
