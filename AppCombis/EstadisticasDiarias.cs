namespace AppCombis
{
    // Guarda las estadísticas del día (viajes, pasajeros, plata)
    public class EstadisticasDiarias
    {
        // Datos generales
        public DateTime Fecha { get; set; }
        public int TotalViajes { get; set; }
        public int TotalPasajeros { get; set; }
        
        // Contadores por tipo de pasajero
        public int PasajerosNormales { get; set; }
        public int PasajerosEstudiantes { get; set; }
        public int PasajerosJubilados { get; set; }
        
        // Plata total recaudada
        public decimal RecaudacionTotal { get; set; }
        
        // Horarios
        public DateTime? HoraPrimerViaje { get; set; }
        public DateTime? HoraUltimoViaje { get; set; }
        
        // Lista de todos los viajes del día
        public List<Viaje> Viajes { get; set; }

        // Representa un viaje individual
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

        // Constructor
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

        // Registra un viaje nuevo con todos sus datos
        public void RegistrarViaje(List<Pasajero> pasajeros)
        {
            if (pasajeros == null || pasajeros.Count == 0)
                return;

            // Creo el viaje
            var viaje = new Viaje
            {
                NumeroViaje = TotalViajes + 1,
                HoraSalida = DateTime.Now,
                CantidadPasajeros = pasajeros.Count,
                Pasajeros = new List<Pasajero>(pasajeros)
            };

            // Calculo cuánta plata se recaudó y cuento por tipo
            decimal recaudacionViaje = 0;
            foreach (var pasajero in pasajeros)
            {
                recaudacionViaje += pasajero.Tarifa;

                // Sumo 1 al contador correspondiente
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

            // Actualizo los totales generales
            TotalViajes++;
            TotalPasajeros += pasajeros.Count;
            RecaudacionTotal += recaudacionViaje;

            // Guardo los horarios
            if (HoraPrimerViaje == null)
                HoraPrimerViaje = viaje.HoraSalida;
            HoraUltimoViaje = viaje.HoraSalida;

            // Agrego el viaje a la lista
            Viajes.Add(viaje);
        }

        // Calcula el promedio de pasajeros por viaje
        public double PromedioPasajerosPorViaje
        {
            get
            {
                if (TotalViajes == 0) return 0;
                return (double)TotalPasajeros / TotalViajes;
            }
        }

        // Calcula el promedio de recaudación por viaje
        public decimal PromedioRecaudacionPorViaje
        {
            get
            {
                if (TotalViajes == 0) return 0;
                return RecaudacionTotal / TotalViajes;
            }
        }

        // Genera el reporte completo en texto
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

            // Desglose por tipo
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

            // Detalle de cada viaje
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

        // Calcula qué porcentaje representa un tipo de pasajero
        private double PorcentajeTipo(int cantidad)
        {
            if (TotalPasajeros == 0) return 0;
            return (double)cantidad / TotalPasajeros * 100;
        }

        // Guarda el reporte en un archivo
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
