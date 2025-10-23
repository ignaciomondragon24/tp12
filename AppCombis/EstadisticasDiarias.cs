namespace AppCombis
{
    /// <summary>
    /// Gestiona las estadísticas diarias del servicio de combis
    /// Registra viajes, pasajeros y recaudación
    /// </summary>
    public class EstadisticasDiarias
    {
        // ============================================
        // PROPIEDADES
        // ============================================
        
        /// <summary>
        /// Fecha de las estadísticas
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Total de viajes realizados en el día
        /// </summary>
        public int TotalViajes { get; set; }

        /// <summary>
        /// Total de pasajeros transportados
        /// </summary>
        public int TotalPasajeros { get; set; }

        /// <summary>
        /// Cantidad de pasajeros normales
        /// </summary>
        public int PasajerosNormales { get; set; }

        /// <summary>
        /// Cantidad de estudiantes
        /// </summary>
        public int PasajerosEstudiantes { get; set; }

        /// <summary>
        /// Cantidad de jubilados
        /// </summary>
        public int PasajerosJubilados { get; set; }

        /// <summary>
        /// Recaudación total del día
        /// </summary>
        public decimal RecaudacionTotal { get; set; }

        /// <summary>
        /// Hora del primer viaje
        /// </summary>
        public DateTime? HoraPrimerViaje { get; set; }

        /// <summary>
        /// Hora del último viaje
        /// </summary>
        public DateTime? HoraUltimoViaje { get; set; }

        /// <summary>
        /// Lista de viajes realizados (con detalles)
        /// </summary>
        public List<Viaje> Viajes { get; set; }

        // ============================================
        // CLASE INTERNA: VIAJE
        // ============================================
        
        /// <summary>
        /// Representa un viaje individual de la combi
        /// </summary>
        public class Viaje
        {
            public int NumeroViaje { get; set; }
            public DateTime HoraSalida { get; set; }
            public int CantidadPasajeros { get; set; }
            public decimal RecaudacionViaje { get; set; }
            public List<Pasajero> Pasajeros { get; set; }

            public Viaje()
            {
                Pasajeros = new List<Pasajero>();
                HoraSalida = DateTime.Now;
            }
        }

        // ============================================
        // CONSTRUCTOR
        // ============================================
        
        public EstadisticasDiarias()
        {
            Fecha = DateTime.Today;
            TotalViajes = 0;
            TotalPasajeros = 0;
            PasajerosNormales = 0;
            PasajerosEstudiantes = 0;
            PasajerosJubilados = 0;
            RecaudacionTotal = 0;
            Viajes = new List<Viaje>();
        }

        // ============================================
        // MÉTODOS
        // ============================================
        
        /// <summary>
        /// Registra un nuevo viaje con sus pasajeros
        /// </summary>
        public void RegistrarViaje(List<Pasajero> pasajeros)
        {
            if (pasajeros == null || pasajeros.Count == 0)
                return;

            // Crear el viaje
            var viaje = new Viaje
            {
                NumeroViaje = TotalViajes + 1,
                HoraSalida = DateTime.Now,
                CantidadPasajeros = pasajeros.Count,
                Pasajeros = new List<Pasajero>(pasajeros)
            };

            // Calcular recaudación del viaje
            decimal recaudacionViaje = 0;
            foreach (var pasajero in pasajeros)
            {
                recaudacionViaje += pasajero.Tarifa;

                // Contabilizar por tipo
                switch (pasajero.Tipo)
                {
                    case Pasajero.TipoPasajero.Normal:
                        PasajerosNormales++;
                        break;
                    case Pasajero.TipoPasajero.Estudiante:
                        PasajerosEstudiantes++;
                        break;
                    case Pasajero.TipoPasajero.Jubilado:
                        PasajerosJubilados++;
                        break;
                }
            }

            viaje.RecaudacionViaje = recaudacionViaje;

            // Actualizar estadísticas generales
            TotalViajes++;
            TotalPasajeros += pasajeros.Count;
            RecaudacionTotal += recaudacionViaje;

            // Registrar horarios
            if (HoraPrimerViaje == null)
                HoraPrimerViaje = viaje.HoraSalida;
            HoraUltimoViaje = viaje.HoraSalida;

            // Agregar el viaje a la lista
            Viajes.Add(viaje);
        }

        /// <summary>
        /// Calcula el promedio de pasajeros por viaje
        /// </summary>
        public double PromedioPasajerosPorViaje
        {
            get
            {
                if (TotalViajes == 0) return 0;
                return (double)TotalPasajeros / TotalViajes;
            }
        }

        /// <summary>
        /// Calcula el promedio de recaudación por viaje
        /// </summary>
        public decimal PromedioRecaudacionPorViaje
        {
            get
            {
                if (TotalViajes == 0) return 0;
                return RecaudacionTotal / TotalViajes;
            }
        }

        /// <summary>
        /// Genera un reporte en texto de las estadísticas
        /// </summary>
        public string GenerarReporte()
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("=======================================================");
            sb.AppendLine("       REPORTE DIARIO - SERVICIO DE COMBIS");
            sb.AppendLine("=======================================================");
            sb.AppendLine();
            sb.AppendLine($"Fecha: {Fecha:dddd, dd 'de' MMMM 'de' yyyy}");
            sb.AppendLine();

            // Resumen general
            sb.AppendLine("-------------------------------------------------------");
            sb.AppendLine("  RESUMEN GENERAL");
            sb.AppendLine("-------------------------------------------------------");
            sb.AppendLine($"  Total de viajes:        {TotalViajes}");
            sb.AppendLine($"  Total de pasajeros:     {TotalPasajeros}");
            sb.AppendLine($"  Recaudacion total:      ${RecaudacionTotal:N2}");
            sb.AppendLine();

            if (TotalViajes > 0)
            {
                sb.AppendLine($"  Primer viaje:           {HoraPrimerViaje:HH:mm:ss}");
                sb.AppendLine($"  Ultimo viaje:           {HoraUltimoViaje:HH:mm:ss}");
                sb.AppendLine($"  Promedio pasajeros/viaje: {PromedioPasajerosPorViaje:F2}");
                sb.AppendLine($"  Promedio recaudacion/viaje: ${PromedioRecaudacionPorViaje:N2}");
            }
            sb.AppendLine();

            // Desglose por tipo de pasajero
            sb.AppendLine("-------------------------------------------------------");
            sb.AppendLine("  DESGLOSE POR TIPO DE PASAJERO");
            sb.AppendLine("-------------------------------------------------------");
            sb.AppendLine($"  [N] Pasajeros Normales:   {PasajerosNormales} ({PorcentajeTipo(PasajerosNormales):F1}%)");
            sb.AppendLine($"  [E] Estudiantes:          {PasajerosEstudiantes} ({PorcentajeTipo(PasajerosEstudiantes):F1}%)");
            sb.AppendLine($"  [J] Jubilados:            {PasajerosJubilados} ({PorcentajeTipo(PasajerosJubilados):F1}%)");
            sb.AppendLine();

            // Recaudación por tipo
            sb.AppendLine("-------------------------------------------------------");
            sb.AppendLine("  RECAUDACION POR TIPO");
            sb.AppendLine("-------------------------------------------------------");
            sb.AppendLine($"  [N] Normales:    ${PasajerosNormales * 500m:N2}");
            sb.AppendLine($"  [E] Estudiantes: ${PasajerosEstudiantes * 250m:N2}");
            sb.AppendLine($"  [J] Jubilados:   ${PasajerosJubilados * 0m:N2}");
            sb.AppendLine();

            // Detalle de viajes
            if (Viajes.Count > 0)
            {
                sb.AppendLine("-------------------------------------------------------");
                sb.AppendLine("  DETALLE DE VIAJES");
                sb.AppendLine("-------------------------------------------------------");
                foreach (var viaje in Viajes)
                {
                    sb.AppendLine($"  Viaje #{viaje.NumeroViaje} - {viaje.HoraSalida:HH:mm:ss}");
                    sb.AppendLine($"    Pasajeros: {viaje.CantidadPasajeros} | Recaudacion: ${viaje.RecaudacionViaje:N2}");
                }
                sb.AppendLine();
            }

            sb.AppendLine("=======================================================");
            sb.AppendLine($"  Reporte generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine("=======================================================");

            return sb.ToString();
        }

        /// <summary>
        /// Calcula el porcentaje de un tipo de pasajero
        /// </summary>
        private double PorcentajeTipo(int cantidad)
        {
            if (TotalPasajeros == 0) return 0;
            return (double)cantidad / TotalPasajeros * 100;
        }

        /// <summary>
        /// Guarda el reporte en un archivo de texto
        /// </summary>
        public void GuardarReporte(string rutaArchivo)
        {
            try
            {
                File.WriteAllText(rutaArchivo, GenerarReporte());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar el reporte: {ex.Message}");
            }
        }
    }
}
