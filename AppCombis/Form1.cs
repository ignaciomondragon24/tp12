namespace AppCombis
{
    /// <summary>
    /// Formulario principal para gestionar el Servicio de Combis MEJORADO
    /// Incluye: Tipos de pasajero, temporizador, estadísticas y reportes
    /// </summary>
    public partial class Form1 : Form
    {
        // ============================================
        // DECLARACIONES INICIALES
        // ============================================

        /// <summary>
        /// Cola dinámica para gestionar la fila de espera de pasajeros
        /// Usa Queue<Pasajero> - Estructura dinámica que crece según la necesidad
        /// </summary>
        private Queue<Pasajero> filaDeEspera = new Queue<Pasajero>();

        /// <summary>
        /// Lista dinámica de pasajeros que están en la combi actualmente
        /// Usa List<Pasajero> - Estructura dinámica para gestionar viaje actual
        /// </summary>
        private List<Pasajero> pasajerosEnCombi = new List<Pasajero>();

        /// <summary>
        /// Estadísticas del día
        /// </summary>
        private EstadisticasDiarias estadisticas = new EstadisticasDiarias();

        /// <summary>
        /// Capacidad máxima de la combi (19 lugares disponibles)
        /// </summary>
        private const int CAPACIDAD_COMBI = 19;

        /// <summary>
        /// Tiempo de espera en segundos (20 minutos = 1200 segundos)
        /// </summary>
        private const int TIEMPO_ESPERA_SEGUNDOS = 1200; // 20 minutos

        /// <summary>
        /// Tiempo restante en segundos
        /// </summary>
        private int tiempoRestanteSegundos = TIEMPO_ESPERA_SEGUNDOS;

        /// <summary>
        /// Indica si la combi está en proceso de llenado
        /// </summary>
        private bool combiEnEspera = false;

        /// <summary>
        /// Archivos de persistencia
        /// </summary>
        private const string ARCHIVO_FILA = "fila_espera.txt";
        private const string ARCHIVO_ESTADISTICAS = "estadisticas.txt";

        /// <summary>
        /// Rutas de recorrido disponibles
        /// </summary>
        private enum RutaRecorrido
        {
            ObeliscoPuertoMadero,    // Ruta 1: Obelisco ? Puerto Madero
            ObeliscoRecoleta,        // Ruta 2: Obelisco ? Recoleta
            ObeliscoPalermo          // Ruta 3: Obelisco ? Palermo
        }

        // ============================================
        // CONSTRUCTOR
        // ============================================

        public Form1()
        {
            InitializeComponent();
        }

        // ============================================
        // EVENTOS DEL FORMULARIO
        // ============================================

        /// <summary>
        /// Evento que se ejecuta al cargar el formulario
        /// Inicializa todos los componentes y carga datos guardados
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Inicializar ComboBox de tipos de pasajero
            InicializarTiposPasajero();

            // Cargar datos persistidos
            CargarFilaDeEsperaDesdeArchivo();
            CargarEstadisticas();

            // Inicializar timer (desactivado hasta que haya al menos un pasajero)
            timerCombi.Enabled = false;
            ActualizarTiempoRestante();

            // Actualizar interfaz
            ActualizarListaEnEspera();
            ActualizarEstadisticas();
        }

        /// <summary>
        /// Inicializa el ComboBox con los tipos de pasajero
        /// </summary>
        private void InicializarTiposPasajero()
        {
            cmbTipoPasajero.Items.Clear();
            cmbTipoPasajero.Items.Add("[N] Normal - $500");
            cmbTipoPasajero.Items.Add("[E] Estudiante - $250");
            cmbTipoPasajero.Items.Add("[J] Jubilado - Gratis");
            cmbTipoPasajero.SelectedIndex = 0; // Por defecto: Normal
        }

        /// <summary>
        /// Evento que se ejecuta al cerrar el formulario
        /// Guarda todos los datos antes de cerrar
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si hay pasajeros en espera, preguntar si desea cerrar
            if (filaDeEspera.Count > 0 || pasajerosEnCombi.Count > 0)
            {
                var resultado = MessageBox.Show(
                    "Hay pasajeros en espera o en la combi.\n¿Está seguro que desea cerrar la aplicación?",
                    "Confirmar cierre",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            // Guardar datos
            GuardarFilaDeEsperaEnArchivo();
            GuardarEstadisticas();
        }

        // ============================================
        // EVENTO: BOTÓN "ANOTAR"
        // ============================================

        /// <summary>
        /// Maneja el clic del botón "Anotar"
        /// Agrega un nuevo pasajero a la fila de espera
        /// </summary>
        private void btnAnotar_Click(object sender, EventArgs e)
        {
            // Obtener el nombre del pasajero
            string nombrePasajero = txtPasajero.Text.Trim();

            // VALIDACIÓN: Campo vacío
            if (string.IsNullOrEmpty(nombrePasajero))
            {
                MessageBox.Show(
                    "Por favor, ingrese el nombre del pasajero.",
                    "Campo vacío",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPasajero.Focus();
                return;
            }

            // VALIDACIÓN: Capacidad máxima
            if (filaDeEspera.Count >= CAPACIDAD_COMBI)
            {
                MessageBox.Show(
                    "Combi llena. No se pueden anotar más pasajeros.\n" +
                    "Debe iniciar el viaje para liberar lugares.",
                    "Capacidad completa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Obtener el tipo de pasajero seleccionado
            Pasajero.TipoPasajero tipo = cmbTipoPasajero.SelectedIndex switch
            {
                0 => Pasajero.TipoPasajero.Normal,
                1 => Pasajero.TipoPasajero.Estudiante,
                2 => Pasajero.TipoPasajero.Jubilado,
                _ => Pasajero.TipoPasajero.Normal
            };

            // Crear el nuevo pasajero
            Pasajero nuevoPasajero = new Pasajero(nombrePasajero, tipo);

            // Agregar a la cola (estructura dinámica)
            filaDeEspera.Enqueue(nuevoPasajero);

            // Si es el primer pasajero, iniciar el temporizador
            if (filaDeEspera.Count == 1 && !combiEnEspera)
            {
                IniciarTemporizador();
            }

            // Limpiar campos
            txtPasajero.Clear();
            txtPasajero.Focus();

            // Actualizar interfaz
            ActualizarListaEnEspera();

            // Mensaje de confirmación
            MessageBox.Show(
                $"PASAJERO ANOTADO:\n\n" +
                $"Nombre: {nuevoPasajero.Nombre}\n" +
                $"Tipo: {nuevoPasajero.ObtenerDescripcionTipo()}\n" +
                $"Tarifa: ${nuevoPasajero.Tarifa}\n" +
                $"Hora: {nuevoPasajero.HoraAnotacion:HH:mm:ss}\n\n" +
                $"Posicion en fila: {filaDeEspera.Count}\n" +
                $"Lugares disponibles: {CAPACIDAD_COMBI - filaDeEspera.Count}",
                "Pasajero anotado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ============================================
        // EVENTO: BOTÓN "SUBIR A LA COMBI"
        // ============================================

        /// <summary>
        /// Maneja el clic del botón "Subir a la combi"
        /// Inicia un viaje con todos los pasajeros en espera
        /// </summary>
        private void btnSubir_Click(object sender, EventArgs e)
        {
            // VALIDACIÓN: Verificar si hay pasajeros
            if (filaDeEspera.Count == 0)
            {
                MessageBox.Show(
                    "No hay pasajeros en espera.\n" +
                    "La combi va a la terminal de Puerto Madero vacía.",
                    "Sin pasajeros",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // VALIDACIÓN: Debe haber al menos 1 pasajero
            if (filaDeEspera.Count < 1)
            {
                MessageBox.Show(
                    "Se necesita al menos 1 pasajero para iniciar el viaje.",
                    "Pasajeros insuficientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Confirmar inicio de viaje
            var resultado = MessageBox.Show(
                $"¿Iniciar viaje con {filaDeEspera.Count} pasajeros?\n\n" +
                $"Tiempo de espera actual: {FormatearTiempo(tiempoRestanteSegundos)}",
                "Confirmar viaje",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
                return;

            // Seleccionar ruta de recorrido
            RutaRecorrido ruta = SeleccionarRuta();

            // Subir todos los pasajeros a la combi
            pasajerosEnCombi.Clear();
            while (filaDeEspera.Count > 0)
            {
                pasajerosEnCombi.Add(filaDeEspera.Dequeue());
            }

            // Detener temporizador
            timerCombi.Stop();
            combiEnEspera = false;

            // Registrar el viaje en las estadísticas
            estadisticas.RegistrarViaje(pasajerosEnCombi);

            // Mostrar detalles del viaje
            MostrarDetallesViaje(ruta);

            // Limpiar combi y reiniciar
            pasajerosEnCombi.Clear();
            tiempoRestanteSegundos = TIEMPO_ESPERA_SEGUNDOS;

            // Actualizar interfaz
            ActualizarListaEnEspera();
            ActualizarEstadisticas();
            ActualizarTiempoRestante();
        }

        // ============================================
        // EVENTO: TIMER
        // ============================================

        /// <summary>
        /// Se ejecuta cada segundo para actualizar el temporizador
        /// </summary>
        private void timerCombi_Tick(object sender, EventArgs e)
        {
            tiempoRestanteSegundos--;

            ActualizarTiempoRestante();

            // Si se acabó el tiempo, iniciar viaje automáticamente
            if (tiempoRestanteSegundos <= 0)
            {
                timerCombi.Stop();
                MessageBox.Show(
                    "TIEMPO AGOTADO (20 minutos).\n" +
                    "La combi parte automaticamente.",
                    "Viaje automatico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Simular clic en botón subir
                btnSubir_Click(sender, e);
            }
        }

        // ============================================
        // EVENTO: BOTÓN REPORTE
        // ============================================

        /// <summary>
        /// Genera y descarga el reporte del día
        /// </summary>
        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                // Generar nombre de archivo con fecha
                string nombreArchivo = $"Reporte_Combis_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                // Guardar reporte
                estadisticas.GuardarReporte(nombreArchivo);

                // Mostrar confirmación
                var resultado = MessageBox.Show(
                    $"REPORTE GENERADO EXITOSAMENTE:\n\n" +
                    $"{nombreArchivo}\n\n" +
                    $"Desea abrir el reporte?",
                    "Reporte generado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (resultado == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("notepad.exe", nombreArchivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al generar el reporte:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================
        // EVENTO: BOTÓN CERRAR
        // ============================================

        /// <summary>
        /// Cierra la aplicación
        /// </summary>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ============================================
        // MÉTODOS AUXILIARES
        // ============================================

        /// <summary>
        /// Inicia el temporizador de 20 minutos
        /// </summary>
        private void IniciarTemporizador()
        {
            tiempoRestanteSegundos = TIEMPO_ESPERA_SEGUNDOS;
            combiEnEspera = true;
            timerCombi.Start();

            MessageBox.Show(
                "TEMPORIZADOR INICIADO: 20 minutos.\n\n" +
                "La combi partira automaticamente al finalizar el tiempo\n" +
                "o al presionar 'Subir a la combi'.",
                "Temporizador iniciado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Actualiza la visualización del tiempo restante
        /// </summary>
        private void ActualizarTiempoRestante()
        {
            lblTiempoRestante.Text = FormatearTiempo(tiempoRestanteSegundos);

            // Cambiar color según tiempo restante
            if (tiempoRestanteSegundos <= 60) // Último minuto
                lblTiempoRestante.ForeColor = Color.Red;
            else if (tiempoRestanteSegundos <= 300) // Últimos 5 minutos
                lblTiempoRestante.ForeColor = Color.Orange;
            else
                lblTiempoRestante.ForeColor = Color.FromArgb(0, 102, 204);
        }

        /// <summary>
        /// Formatea segundos a formato MM:SS
        /// </summary>
        private string FormatearTiempo(int segundos)
        {
            int minutos = segundos / 60;
            int segs = segundos % 60;
            return $"{minutos:D2}:{segs:D2}";
        }

        /// <summary>
        /// Permite seleccionar la ruta de recorrido
        /// </summary>
        private RutaRecorrido SeleccionarRuta()
        {
            var form = new Form
            {
                Text = "Seleccionar Ruta",
                Width = 400,
                Height = 250,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var lblTitulo = new Label
            {
                Text = "Seleccione el recorrido de la combi:",
                AutoSize = true,
                Location = new Point(20, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            var rbRuta1 = new RadioButton
            {
                Text = "[1] Ruta 1: Obelisco -> Puerto Madero (20 min)",
                Location = new Point(30, 60),
                Width = 350,
                Checked = true
            };

            var rbRuta2 = new RadioButton
            {
                Text = "[2] Ruta 2: Obelisco -> Recoleta (25 min)",
                Location = new Point(30, 90),
                Width = 350
            };

            var rbRuta3 = new RadioButton
            {
                Text = "[3] Ruta 3: Obelisco -> Palermo (30 min)",
                Location = new Point(30, 120),
                Width = 350
            };

            var btnOk = new Button
            {
                Text = "Confirmar",
                Location = new Point(150, 160),
                Width = 100,
                DialogResult = DialogResult.OK
            };

            form.Controls.Add(lblTitulo);
            form.Controls.Add(rbRuta1);
            form.Controls.Add(rbRuta2);
            form.Controls.Add(rbRuta3);
            form.Controls.Add(btnOk);
            form.AcceptButton = btnOk;

            form.ShowDialog();

            if (rbRuta1.Checked) return RutaRecorrido.ObeliscoPuertoMadero;
            if (rbRuta2.Checked) return RutaRecorrido.ObeliscoRecoleta;
            return RutaRecorrido.ObeliscoPalermo;
        }

        /// <summary>
        /// Muestra los detalles del viaje realizado
        /// </summary>
        private void MostrarDetallesViaje(RutaRecorrido ruta)
        {
            var sb = new System.Text.StringBuilder();
            
            sb.AppendLine("=============================================");
            sb.AppendLine("         VIAJE INICIADO");
            sb.AppendLine("=============================================");
            sb.AppendLine();
            
            // Información de la ruta
            string nombreRuta = ruta switch
            {
                RutaRecorrido.ObeliscoPuertoMadero => "Obelisco -> Puerto Madero (20 min)",
                RutaRecorrido.ObeliscoRecoleta => "Obelisco -> Recoleta (25 min)",
                RutaRecorrido.ObeliscoPalermo => "Obelisco -> Palermo (30 min)",
                _ => "Desconocido"
            };
            
            sb.AppendLine($"Ruta: {nombreRuta}");
            sb.AppendLine($"Hora salida: {DateTime.Now:HH:mm:ss}");
            sb.AppendLine($"Pasajeros: {pasajerosEnCombi.Count}");
            sb.AppendLine();
            
            // Desglose por tipo
            int normales = pasajerosEnCombi.Count(p => p.Tipo == Pasajero.TipoPasajero.Normal);
            int estudiantes = pasajerosEnCombi.Count(p => p.Tipo == Pasajero.TipoPasajero.Estudiante);
            int jubilados = pasajerosEnCombi.Count(p => p.Tipo == Pasajero.TipoPasajero.Jubilado);
            
            sb.AppendLine("---------------------------------------------");
            sb.AppendLine(" DESGLOSE DE PASAJEROS:");
            sb.AppendLine("---------------------------------------------");
            sb.AppendLine($" [N] Normales:    {normales}");
            sb.AppendLine($" [E] Estudiantes: {estudiantes}");
            sb.AppendLine($" [J] Jubilados:   {jubilados}");
            sb.AppendLine();
            
            // Recaudación
            decimal recaudacion = pasajerosEnCombi.Sum(p => p.Tarifa);
            sb.AppendLine("---------------------------------------------");
            sb.AppendLine($" RECAUDACION: ${recaudacion}");
            sb.AppendLine("---------------------------------------------");
            sb.AppendLine();
            
            // Lista de pasajeros
            sb.AppendLine(" PASAJEROS A BORDO:");
            int num = 1;
            foreach (var pasajero in pasajerosEnCombi)
            {
                sb.AppendLine($" {num}. {pasajero}");
                num++;
            }
            
            sb.AppendLine();
            sb.AppendLine("=============================================");
            sb.AppendLine("   Buen viaje!");
            sb.AppendLine("=============================================");
            
            MessageBox.Show(sb.ToString(), "Detalles del Viaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ============================================
        // MÉTODO: ACTUALIZAR LISTA VISUAL
        // ============================================

        /// <summary>
        /// Actualiza el ListBox con los pasajeros actuales en la fila de espera
        /// Muestra el orden en que subirán a la combi
        /// </summary>
        private void ActualizarListaEnEspera()
        {
            // Limpiar el ListBox
            lstEnEspera.Items.Clear();

            // Verificar si la fila está vacía
            if (filaDeEspera.Count == 0)
            {
                lstEnEspera.Items.Add("(No hay pasajeros en espera)");
                groupBoxTerminal.Text = "Terminal Obelisco - Esperando pasajeros...";
                return;
            }

            // Recorrer la cola SIN MODIFICARLA (estructura dinámica)
            // ToArray() crea una copia temporal para iterar
            int posicion = 1;
            foreach (Pasajero pasajero in filaDeEspera.ToArray())
            {
                string tiempoEspera = (DateTime.Now - pasajero.HoraAnotacion).Minutes + " min";
                lstEnEspera.Items.Add($"{posicion}. {pasajero} | Espera: {tiempoEspera}");
                posicion++;
            }

            // Actualizar título con contador
            groupBoxTerminal.Text = $"Terminal Obelisco - Pasajeros: {filaDeEspera.Count}/{CAPACIDAD_COMBI}";
        }

        /// <summary>
        /// Actualiza las estadísticas en la interfaz
        /// </summary>
        private void ActualizarEstadisticas()
        {
            lblViajesHoy.Text = $"Viajes: {estadisticas.TotalViajes}";
            lblPasajerosHoy.Text = $"Pasajeros: {estadisticas.TotalPasajeros}";
            lblRecaudacion.Text = $"Recaudacion:\n${estadisticas.RecaudacionTotal:N2}";
        }

        // ============================================
        // PERSISTENCIA: GUARDAR EN ARCHIVO
        // ============================================

        /// <summary>
        /// Guarda la fila de espera actual en un archivo
        /// </summary>
        private void GuardarFilaDeEsperaEnArchivo()
        {
            try
            {
                // Si no hay pasajeros, eliminar el archivo
                if (filaDeEspera.Count == 0)
                {
                    if (File.Exists(ARCHIVO_FILA))
                        File.Delete(ARCHIVO_FILA);
                    return;
                }

                // Guardar cada pasajero en formato CSV
                using (StreamWriter escritor = new StreamWriter(ARCHIVO_FILA))
                {
                    foreach (Pasajero pasajero in filaDeEspera)
                    {
                        escritor.WriteLine(pasajero.ToCsv());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al guardar la fila de espera:\n{ex.Message}",
                    "Error de guardado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carga la fila de espera desde un archivo
        /// </summary>
        private void CargarFilaDeEsperaDesdeArchivo()
        {
            try
            {
                // Verificar si existe el archivo
                if (!File.Exists(ARCHIVO_FILA))
                {
                    ActualizarListaEnEspera();
                    return;
                }

                // Leer línea por línea
                using (StreamReader lector = new StreamReader(ARCHIVO_FILA))
                {
                    string linea;
                    while ((linea = lector.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(linea) && filaDeEspera.Count < CAPACIDAD_COMBI)
                        {
                            Pasajero pasajero = Pasajero.FromCsv(linea);
                            filaDeEspera.Enqueue(pasajero);
                        }
                    }
                }

                // Actualizar interfaz
                ActualizarListaEnEspera();

                // Si hay pasajeros, iniciar temporizador
                if (filaDeEspera.Count > 0 && !combiEnEspera)
                {
                    // Calcular tiempo transcurrido desde el primer pasajero
                    var primerPasajero = filaDeEspera.Peek();
                    int tiempoTranscurrido = (int)(DateTime.Now - primerPasajero.HoraAnotacion).TotalSeconds;
                    tiempoRestanteSegundos = Math.Max(0, TIEMPO_ESPERA_SEGUNDOS - tiempoTranscurrido);

                    if (tiempoRestanteSegundos > 0)
                    {
                        combiEnEspera = true;
                        timerCombi.Start();
                    }
                }

                // Mensaje informativo
                if (filaDeEspera.Count > 0)
                {
                    MessageBox.Show(
                        $"Se cargaron {filaDeEspera.Count} pasajeros de la sesión anterior.",
                        "Datos restaurados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar la fila de espera:\n{ex.Message}",
                    "Error de carga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                filaDeEspera.Clear();
                ActualizarListaEnEspera();
            }
        }

        /// <summary>
        /// Guarda las estadísticas del día
        /// </summary>
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
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al guardar estadísticas:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carga las estadísticas del día
        /// </summary>
        private void CargarEstadisticas()
        {
            try
            {
                if (!File.Exists(ARCHIVO_ESTADISTICAS))
                    return;

                // Verificar si las estadísticas son del día actual
                bool esHoy = false;
                using (StreamReader lector = new StreamReader(ARCHIVO_ESTADISTICAS))
                {
                    string linea;
                    while ((linea = lector.ReadLine()) != null)
                    {
                        string[] partes = linea.Split('|');
                        if (partes.Length >= 2)
                        {
                            switch (partes[0])
                            {
                                case "Fecha":
                                    DateTime fechaGuardada = DateTime.Parse(partes[1]);
                                    esHoy = fechaGuardada.Date == DateTime.Today;
                                    if (!esHoy) return; // Si no es de hoy, no cargar
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

                ActualizarEstadisticas();
            }
            catch
            {
                // Si hay error, iniciar con estadísticas nuevas
                estadisticas = new EstadisticasDiarias();
            }
        }

        private void cmbTipoPasajero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
