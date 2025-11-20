namespace AppCombis
{
    // Sistema de Combis - TP 12
   
    public partial class Form1 : Form
    {
   
        // VARIABLES GLOBALES
      

        // Cola para la fila de pasajeros (FIFO - el primero que llega es el primero que sube)
        private Queue<Pasajero> filaDeEspera = new Queue<Pasajero>();
        
        // Lista de pasajeros que están en la combi en este momento
        private List<Pasajero> pasajerosEnCombi = new List<Pasajero>();

        // Para llevar las estadísticas del día
        private EstadisticasDiarias estadisticas = new EstadisticasDiarias();

        // La combi tiene 19 lugares
        private const int CAPACIDAD_COMBI = 19;

        // Tiempo de espera: 20 minutos = 1200 segundos
        private const int TIEMPO_ESPERA_SEGUNDOS = 1200;

        // Cuántos segundos quedan en el temporizador
        private int tiempoRestanteSegundos = TIEMPO_ESPERA_SEGUNDOS;

        // Si la combi está esperando para salir
        private bool combiEnEspera = false;

        // Nombres de los archivos donde guardo los datos
        private const string ARCHIVO_FILA = "fila_espera.txt";
        private const string ARCHIVO_ESTADISTICAS = "estadisticas.txt";

        // Las 3 rutas que puede tomar la combi
        private enum RutaRecorrido
        {
            ObeliscoPuertoMadero,    // Ruta 1: 20 minutos
            ObeliscoRecoleta,        // Ruta 2: 25 minutos
            ObeliscoPalermo          // Ruta 3: 30 minutos
        }

        // ============================================
        // CUANDO SE CARGA EL FORMULARIO
        // ============================================

        public Form1()
        {
            InitializeComponent();
        }

        // Esto se ejecuta cuando arranca el programa
        private void Form1_Load(object sender, EventArgs e)
        {
            // Cargo los tipos de pasajero en el combo
            InicializarTiposPasajero();

            // Cargo los datos que quedaron guardados de la vez anterior
            CargarFilaDeEsperaDesdeArchivo();
            CargarEstadisticas();

            // El timer arranca apagado
            timerCombi.Enabled = false;
            ActualizarTiempoRestante();

            // Actualizo la pantalla
            ActualizarListaEnEspera();
            ActualizarEstadisticas();
        }

        // Cargo los tipos de pasajero en el ComboBox
        private void InicializarTiposPasajero()
        {
            cmbTipoPasajero.Items.Clear();
            cmbTipoPasajero.Items.Add("[N] Normal - $500");
            cmbTipoPasajero.Items.Add("[E] Estudiante - $250");
            cmbTipoPasajero.Items.Add("[J] Jubilado - Gratis");
            cmbTipoPasajero.SelectedIndex = 0; // Normal por defecto
        }

        // Cuando cierro el programa
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si hay pasajeros esperando, pregunto si está seguro
            if (filaDeEspera.Count > 0 || pasajerosEnCombi.Count > 0)
            {
                var resultado = MessageBox.Show(
                    "Hay pasajeros en espera o en la combi.\n¿Está seguro que desea cerrar la aplicación?",
                    "Confirmar cierre",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                {
                    e.Cancel = true; // Cancelo el cierre
                    return;
                }
            }

            // Guardo todo antes de cerrar
            GuardarFilaDeEsperaEnArchivo();
            GuardarEstadisticas();
        }

        // ============================================
        // BOTÓN ANOTAR
        // ============================================

        // Cuando clickean el botón "Anotar"
        private void btnAnotar_Click(object sender, EventArgs e)
        {
            // Agarro el nombre que escribieron
            string nombrePasajero = txtPasajero.Text.Trim();

            // Verifico que no esté vacío
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

            // Me fijo si ya está llena la combi
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

            // Veo qué tipo de pasajero seleccionó
            Pasajero.TipoPasajero tipo = cmbTipoPasajero.SelectedIndex switch
            {
                0 => Pasajero.TipoPasajero.Normal,
                1 => Pasajero.TipoPasajero.Estudiante,
                2 => Pasajero.TipoPasajero.Jubilado,
                _ => Pasajero.TipoPasajero.Normal
            };

            // Creo el pasajero nuevo
            Pasajero nuevoPasajero = new Pasajero(nombrePasajero, tipo);

            // Lo agrego a la fila (al final)
            filaDeEspera.Enqueue(nuevoPasajero);

            // Si es el primero, arranco el timer
            if (filaDeEspera.Count == 1 && !combiEnEspera)
            {
                IniciarTemporizador();
            }

            // Limpio el campo de texto
            txtPasajero.Clear();
            txtPasajero.Focus();

            // Actualizo la lista en pantalla
            ActualizarListaEnEspera();

            // Muestro mensaje de confirmación
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
        // BOTÓN SUBIR A LA COMBI
        // ============================================

        // Cuando clickean "Subir a la combi"
        private void btnSubir_Click(object sender, EventArgs e)
        {
            // Me fijo si hay alguien esperando
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

            // Pido confirmación
            var resultado = MessageBox.Show(
                $"¿Iniciar viaje con {filaDeEspera.Count} pasajeros?\n\n" +
                $"Tiempo de espera actual: {FormatearTiempo(tiempoRestanteSegundos)}",
                "Confirmar viaje",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
                return;

            // Le pido que elija la ruta
            RutaRecorrido ruta = SeleccionarRuta();

            // Subo a todos los pasajeros (voy sacando de la fila)
            pasajerosEnCombi.Clear();
            while (filaDeEspera.Count > 0)
            {
                pasajerosEnCombi.Add(filaDeEspera.Dequeue());
            }

            // Paro el timer
            timerCombi.Stop();
            combiEnEspera = false;

            // Guardo el viaje en las estadísticas
            estadisticas.RegistrarViaje(pasajerosEnCombi);

            // Muestro los detalles del viaje
            MostrarDetallesViaje(ruta);

            // Limpio todo para el próximo viaje
            pasajerosEnCombi.Clear();
            tiempoRestanteSegundos = TIEMPO_ESPERA_SEGUNDOS;

            // Actualizo la pantalla
            ActualizarListaEnEspera();
            ActualizarEstadisticas();
            ActualizarTiempoRestante();
        }

        
        // TEMPORIZADOR
     

        // Esto se ejecuta cada segundo
        private void timerCombi_Tick(object sender, EventArgs e)
        {
            // Resto 1 segundo
            tiempoRestanteSegundos--;

            // Actualizo el label con el tiempo
            ActualizarTiempoRestante();

            // Si se acabó el tiempo, la combi sale sola
            if (tiempoRestanteSegundos <= 0)
            {
                timerCombi.Stop();
                MessageBox.Show(
                    "TIEMPO AGOTADO (20 minutos).\n" +
                    "La combi parte automaticamente.",
                    "Viaje automatico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Hago como si hubieran clickeado el botón subir
                btnSubir_Click(sender, e);
            }
        }

        // ============================================
        // BOTÓN GENERAR REPORTE
        // ============================================

        // Cuando clickean "Generar Reporte"
        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                // Creo el nombre del archivo con la fecha y hora
                string nombreArchivo = $"Reporte_Combis_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                // Guardo el reporte
                estadisticas.GuardarReporte(nombreArchivo);

                // Pregunto si lo quiere abrir
                var resultado = MessageBox.Show(
                    $"REPORTE GENERADO EXITOSAMENTE:\n\n" +
                    $"{nombreArchivo}\n\n" +
                    $"Desea abrir el reporte?",
                    "Reporte generado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (resultado == DialogResult.Yes)
                {
                    // Abro el archivo en el Notepad
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
        // BOTÓN CERRAR
        // ============================================

        // Cuando clickean el botón "Cerrar Aplicación"
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ============================================
        // FUNCIONES AUXILIARES
        // ============================================

        // Arranco el temporizador de 20 minutos
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

        // Actualizo el label del tiempo y le cambio el color
        private void ActualizarTiempoRestante()
        {
            lblTiempoRestante.Text = FormatearTiempo(tiempoRestanteSegundos);

            // Cambio el color según cuánto tiempo queda
            if (tiempoRestanteSegundos <= 60) // Menos de 1 minuto
                lblTiempoRestante.ForeColor = Color.Red;
            else if (tiempoRestanteSegundos <= 300) // Menos de 5 minutos
                lblTiempoRestante.ForeColor = Color.Orange;
            else
                lblTiempoRestante.ForeColor = Color.FromArgb(0, 102, 204);
        }

        // Convierto los segundos a formato MM:SS
        private string FormatearTiempo(int segundos)
        {
            int minutos = segundos / 60;
            int segs = segundos % 60;
            return $"{minutos:D2}:{segs:D2}";
        }

        // Le pido al usuario que elija una ruta
        private RutaRecorrido SeleccionarRuta()
        {
            // Creo un formulario nuevo para elegir la ruta
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
                Checked = true // Esta es la que viene marcada por defecto
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

            // Agrego todos los controles al formulario
            form.Controls.Add(lblTitulo);
            form.Controls.Add(rbRuta1);
            form.Controls.Add(rbRuta2);
            form.Controls.Add(rbRuta3);
            form.Controls.Add(btnOk);
            form.AcceptButton = btnOk;

            // Muestro el formulario y espero
            form.ShowDialog();

            // Devuelvo la ruta que eligió
            if (rbRuta1.Checked) return RutaRecorrido.ObeliscoPuertoMadero;
            if (rbRuta2.Checked) return RutaRecorrido.ObeliscoRecoleta;
            return RutaRecorrido.ObeliscoPalermo;
        }

        // Muestro un mensaje con todos los detalles del viaje
        private void MostrarDetallesViaje(RutaRecorrido ruta)
        {
            var sb = new System.Text.StringBuilder();
            
            sb.AppendLine("=============================================");
            sb.AppendLine("         VIAJE INICIADO");
            sb.AppendLine("=============================================");
            sb.AppendLine();
            
            // Muestro qué ruta eligió
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
            
            // Cuento cuántos hay de cada tipo
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
            
            // Calculo cuánta plata se recaudó
            decimal recaudacion = pasajerosEnCombi.Sum(p => p.Tarifa);
            sb.AppendLine("---------------------------------------------");
            sb.AppendLine($" RECAUDACION: ${recaudacion}");
            sb.AppendLine("---------------------------------------------");
            sb.AppendLine();
            
            // Lista todos los pasajeros
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
        // ACTUALIZAR LA LISTA EN PANTALLA
        // ============================================

        // Actualizo el ListBox con los pasajeros que están esperando
        private void ActualizarListaEnEspera()
        {
            // Limpio lo que había antes
            lstEnEspera.Items.Clear();

            // Si no hay nadie esperando
            if (filaDeEspera.Count == 0)
            {
                lstEnEspera.Items.Add("(No hay pasajeros en espera)");
                groupBoxTerminal.Text = "Terminal Obelisco - Esperando pasajeros...";
                return;
            }

            // Recorro la fila y los voy mostrando
            // ToArray() me deja ver la cola sin modificarla
            int posicion = 1;
            foreach (Pasajero pasajero in filaDeEspera.ToArray())
            {
                // Calculo cuánto tiempo lleva esperando
                string tiempoEspera = (DateTime.Now - pasajero.HoraAnotacion).Minutes + " min";
                lstEnEspera.Items.Add($"{posicion}. {pasajero} | Espera: {tiempoEspera}");
                posicion++;
            }

            // Actualizo el título con cuántos hay
            groupBoxTerminal.Text = $"Terminal Obelisco - Pasajeros: {filaDeEspera.Count}/{CAPACIDAD_COMBI}";
        }

        // Actualizo los labels de las estadísticas
        private void ActualizarEstadisticas()
        {
            lblViajesHoy.Text = $"Viajes: {estadisticas.TotalViajes}";
            lblPasajerosHoy.Text = $"Pasajeros: {estadisticas.TotalPasajeros}";
            lblRecaudacion.Text = $"Recaudacion:\n${estadisticas.RecaudacionTotal:N2}";
        }

        // ============================================
        // GUARDAR Y CARGAR DATOS
        // ============================================

        // Guardo la fila de espera en un archivo de texto
        private void GuardarFilaDeEsperaEnArchivo()
        {
            try
            {
                // Si no hay nadie, borro el archivo
                if (filaDeEspera.Count == 0)
                {
                    if (File.Exists(ARCHIVO_FILA))
                        File.Delete(ARCHIVO_FILA);
                    return;
                }

                // Guardo cada pasajero en una línea
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

        // Cargo la fila de espera desde el archivo
        private void CargarFilaDeEsperaDesdeArchivo()
        {
            try
            {
                // Me fijo si existe el archivo
                if (!File.Exists(ARCHIVO_FILA))
                {
                    ActualizarListaEnEspera();
                    return;
                }

                // Leo línea por línea
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

                // Actualizo la pantalla
                ActualizarListaEnEspera();

                // Si hay pasajeros, arranco el timer desde donde quedó
                if (filaDeEspera.Count > 0 && !combiEnEspera)
                {
                    // Veo cuánto tiempo pasó desde que se anotó el primero
                    var primerPasajero = filaDeEspera.Peek();
                    int tiempoTranscurrido = (int)(DateTime.Now - primerPasajero.HoraAnotacion).TotalSeconds;
                    tiempoRestanteSegundos = Math.Max(0, TIEMPO_ESPERA_SEGUNDOS - tiempoTranscurrido);

                    if (tiempoRestanteSegundos > 0)
                    {
                        combiEnEspera = true;
                        timerCombi.Start();
                    }
                }

                // Aviso que se cargaron los datos
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

        // Guardo las estadísticas en un archivo
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

        // Cargo las estadísticas desde el archivo
        private void CargarEstadisticas()
        {
            try
            {
                if (!File.Exists(ARCHIVO_ESTADISTICAS))
                    return;

                // Solo cargo las estadísticas si son de hoy
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
                                    if (!esHoy) return; // Si no es de hoy, no cargo nada
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
                // Si hay error, empiezo de cero
                estadisticas = new EstadisticasDiarias();
            }
        }

        private void cmbTipoPasajero_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Este método está vacío pero lo dejo por si después necesito usarlo
        }
    }
}
