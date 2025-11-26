namespace AppCombis
{
    // Representa una combi con su número, fila de pasajeros y estado
    public class Combi
    {
        // Identificación de la combi
        public int NumeroCombi { get; set; }
        public string Nombre { get; set; }
        public string Destino { get; set; }  // Nuevo: Destino de la combi

        // Cola de pasajeros esperando
        public Queue<Pasajero> FilaDeEspera { get; set; }

        // Capacidad máxima de la combi
        public int Capacidad { get; set; }

        // Estado de la combi
        public EstadoCombi Estado { get; set; }

        // Tiempo restante en segundos hasta que salga (20 minutos = 1200 segundos)
        public int TiempoRestanteSegundos { get; set; }

        // Hora en que se anotó el primer pasajero
        public DateTime? HoraInicioEspera { get; set; }

        // Estados posibles de una combi
        public enum EstadoCombi
        {
            Disponible,      // No hay pasajeros, esperando
            EnEspera,        // Hay pasajeros, timer corriendo
            EnViaje,         // Ya salió en viaje
            Mantenimiento    // No está disponible
        }

        // Rutas/Destinos disponibles
        public static readonly string[] DestinosDisponibles = new[]
        {
            "Puerto Madero (20 min)",
            "Recoleta (25 min)",
            "Palermo (30 min)",
            "Retiro (15 min)",
            "San Telmo (25 min)",
            "Belgrano (35 min)"
        };

        // Constructor
        public Combi(int numeroCombi, string nombre, string destino, int capacidad = 19)
        {
            NumeroCombi = numeroCombi;
            Nombre = nombre;
            Destino = destino;
            Capacidad = capacidad;
            FilaDeEspera = new Queue<Pasajero>();
            Estado = EstadoCombi.Disponible;
            TiempoRestanteSegundos = 1200; // 20 minutos por defecto
        }

        // Agregar un pasajero a la fila
        public bool AgregarPasajero(Pasajero pasajero)
        {
            if (FilaDeEspera.Count >= Capacidad)
                return false;

            FilaDeEspera.Enqueue(pasajero);

            // Si es el primero, inicio el tiempo de espera
            if (FilaDeEspera.Count == 1 && Estado == EstadoCombi.Disponible)
            {
                HoraInicioEspera = DateTime.Now;
                TiempoRestanteSegundos = 1200; // Reseteo a 20 minutos
                Estado = EstadoCombi.EnEspera;
            }

            return true;
        }

        // Quitar un pasajero específico de la fila
        public bool QuitarPasajero(Pasajero pasajero)
        {
            // Convierto la cola a lista para poder modificarla
            var lista = FilaDeEspera.ToList();
            
            // Busco el pasajero
            var encontrado = lista.FirstOrDefault(p => 
                p.Nombre == pasajero.Nombre && 
                p.HoraAnotacion == pasajero.HoraAnotacion);

            if (encontrado == null)
                return false;

            // Lo quito
            lista.Remove(encontrado);

            // Recreo la cola
            FilaDeEspera.Clear();
            foreach (var p in lista)
            {
                FilaDeEspera.Enqueue(p);
            }

            // Si quedó vacía, reseteo el estado
            if (FilaDeEspera.Count == 0)
            {
                Estado = EstadoCombi.Disponible;
                HoraInicioEspera = null;
                TiempoRestanteSegundos = 1200;
            }

            return true;
        }

        // Actualizar el timer (se llama cada segundo)
        public bool ActualizarTimer()
        {
            if (Estado == EstadoCombi.EnEspera && FilaDeEspera.Count > 0)
            {
                TiempoRestanteSegundos--;

                // Retorna true si se acabó el tiempo
                if (TiempoRestanteSegundos <= 0)
                {
                    return true; // Tiempo agotado, debe partir
                }
            }
            return false;
        }

        // Obtener todos los pasajeros y vaciar la fila (para iniciar viaje)
        public List<Pasajero> IniciarViaje()
        {
            var pasajeros = new List<Pasajero>();
            
            while (FilaDeEspera.Count > 0)
            {
                pasajeros.Add(FilaDeEspera.Dequeue());
            }

            Estado = EstadoCombi.EnViaje;
            return pasajeros;
        }

        // Finalizar viaje y volver a estado disponible
        public void FinalizarViaje()
        {
            Estado = EstadoCombi.Disponible;
            FilaDeEspera.Clear();
            TiempoRestanteSegundos = 1200;
            HoraInicioEspera = null;
        }

        // Calcular recaudación actual en la fila
        public decimal RecaudacionEnEspera
        {
            get
            {
                return FilaDeEspera.Sum(p => p.Tarifa);
            }
        }

        // Contar pasajeros por tipo
        public (int normales, int estudiantes, int jubilados) ContarPorTipo()
        {
            int normales = FilaDeEspera.Count(p => p.Tipo == Pasajero.TipoPasajero.Normal);
            int estudiantes = FilaDeEspera.Count(p => p.Tipo == Pasajero.TipoPasajero.Estudiante);
            int jubilados = FilaDeEspera.Count(p => p.Tipo == Pasajero.TipoPasajero.Jubilado);
            
            return (normales, estudiantes, jubilados);
        }

        // Formatear tiempo restante
        public string TiempoRestanteFormateado
        {
            get
            {
                int minutos = TiempoRestanteSegundos / 60;
                int segundos = TiempoRestanteSegundos % 60;
                return $"{minutos:D2}:{segundos:D2}";
            }
        }

        // Color del tiempo según urgencia
        public Color ColorTiempo
        {
            get
            {
                if (TiempoRestanteSegundos <= 60) return Color.Red;
                if (TiempoRestanteSegundos <= 300) return Color.Orange;
                return Color.FromArgb(0, 102, 204);
            }
        }

        // Convertir a CSV para guardar
        public string ToCsv()
        {
            return $"{NumeroCombi}|{Nombre}|{Destino}|{Capacidad}|{(int)Estado}|{TiempoRestanteSegundos}|{HoraInicioEspera?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""}";
        }

        // Crear desde CSV
        public static Combi FromCsv(string csv)
        {
            try
            {
                string[] partes = csv.Split('|');
                if (partes.Length >= 7)
                {
                    var combi = new Combi(
                        int.Parse(partes[0]),
                        partes[1],
                        partes[2],
                        int.Parse(partes[3])
                    )
                    {
                        Estado = (EstadoCombi)int.Parse(partes[4]),
                        TiempoRestanteSegundos = int.Parse(partes[5])
                    };

                    if (!string.IsNullOrEmpty(partes[6]))
                    {
                        combi.HoraInicioEspera = DateTime.Parse(partes[6]);
                    }

                    return combi;
                }
            }
            catch
            {
                // Si hay error, devuelvo una combi por defecto
            }

            return new Combi(1, "Combi 1", "Puerto Madero (20 min)");
        }

        // Representación en texto para el ListBox
        public override string ToString()
        {
            string info = $"Combi: {Nombre}";
            
            if (Estado == EstadoCombi.EnEspera && FilaDeEspera.Count > 0)
            {
                info += $" [{TiempoRestanteFormateado}]";
            }
            
            info += $"\n   Destino: {Destino}";
            info += $"\n   Pasajeros: {FilaDeEspera.Count}/{Capacidad}";
            
            if (FilaDeEspera.Count > 0)
            {
                info += $" | Recaudacion: ${RecaudacionEnEspera}";
            }

            return info;
        }
    }
}
