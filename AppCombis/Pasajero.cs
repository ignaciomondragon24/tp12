namespace AppCombis
{
    /// <summary>
    /// Representa un pasajero del servicio de combis
    /// Incluye información sobre el tipo de pasajero y tarifa correspondiente
    /// </summary>
    public class Pasajero
    {
        // ============================================
        // ENUMERACIÓN: TIPOS DE PASAJERO
        // ============================================
        
        /// <summary>
        /// Define los tipos de pasajero disponibles con sus respectivas tarifas
        /// </summary>
        public enum TipoPasajero
        {
            Normal,      // Tarifa completa: $500
            Estudiante,  // Tarifa reducida: $250 (50% descuento)
            Jubilado     // Tarifa gratuita: $0 (viaja gratis)
        }

        // ============================================
        // PROPIEDADES
        // ============================================
        
        /// <summary>
        /// Nombre completo del pasajero
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Tipo de pasajero (Normal, Estudiante o Jubilado)
        /// </summary>
        public TipoPasajero Tipo { get; set; }

        /// <summary>
        /// Hora en que el pasajero se anotó en la fila
        /// </summary>
        public DateTime HoraAnotacion { get; set; }

        /// <summary>
        /// Tarifa que debe pagar el pasajero según su tipo
        /// </summary>
        public decimal Tarifa
        {
            get
            {
                return Tipo switch
                {
                    TipoPasajero.Normal => 500m,      // Tarifa completa
                    TipoPasajero.Estudiante => 250m,  // 50% descuento
                    TipoPasajero.Jubilado => 0m,      // Viaja gratis
                    _ => 500m
                };
            }
        }

        // ============================================
        // CONSTRUCTORES
        // ============================================
        
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Pasajero()
        {
            Nombre = string.Empty;
            Tipo = TipoPasajero.Normal;
            HoraAnotacion = DateTime.Now;
        }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        /// <param name="nombre">Nombre del pasajero</param>
        /// <param name="tipo">Tipo de pasajero</param>
        public Pasajero(string nombre, TipoPasajero tipo)
        {
            Nombre = nombre;
            Tipo = tipo;
            HoraAnotacion = DateTime.Now;
        }

        // ============================================
        // MÉTODOS
        // ============================================
        
        /// <summary>
        /// Obtiene el símbolo que representa al tipo de pasajero
        /// </summary>
        public string ObtenerSimbolo()
        {
            return Tipo switch
            {
                TipoPasajero.Normal => "[N]",
                TipoPasajero.Estudiante => "[E]",
                TipoPasajero.Jubilado => "[J]",
                _ => "[N]"
            };
        }

        /// <summary>
        /// Obtiene una descripción del tipo de pasajero
        /// </summary>
        public string ObtenerDescripcionTipo()
        {
            return Tipo switch
            {
                TipoPasajero.Normal => "Normal",
                TipoPasajero.Estudiante => "Estudiante",
                TipoPasajero.Jubilado => "Jubilado",
                _ => "Normal"
            };
        }

        /// <summary>
        /// Representación en string del pasajero
        /// </summary>
        public override string ToString()
        {
            return $"{ObtenerSimbolo()} {Nombre} ({ObtenerDescripcionTipo()}) - ${Tarifa}";
        }

        /// <summary>
        /// Serializa el pasajero a formato CSV para guardar en archivo
        /// </summary>
        public string ToCsv()
        {
            return $"{Nombre}|{(int)Tipo}|{HoraAnotacion:yyyy-MM-dd HH:mm:ss}";
        }

        /// <summary>
        /// Deserializa un pasajero desde formato CSV
        /// </summary>
        public static Pasajero FromCsv(string csv)
        {
            try
            {
                string[] partes = csv.Split('|');
                if (partes.Length >= 3)
                {
                    return new Pasajero
                    {
                        Nombre = partes[0],
                        Tipo = (TipoPasajero)int.Parse(partes[1]),
                        HoraAnotacion = DateTime.Parse(partes[2])
                    };
                }
            }
            catch
            {
                // Si hay error, crear pasajero normal por defecto
            }

            return new Pasajero(csv, TipoPasajero.Normal);
        }
    }
}
