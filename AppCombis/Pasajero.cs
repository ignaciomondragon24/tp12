namespace AppCombis
{
    // Representa a un pasajero que se anota para viajar
    public class Pasajero
    {
        // Los 3 tipos de pasajero que existen
        public enum TipoPasajero
        {
            Normal,      // Paga $500
            Estudiante,  // Paga $250 (mitad de precio)
            Jubilado     // Viaja gratis
        }

        // Datos del pasajero
        public string Nombre { get; set; }
        public TipoPasajero Tipo { get; set; }
        public DateTime HoraAnotacion { get; set; }
        
        // Nuevo: Para reservas grupales
        public bool EsReservaPrincipal { get; set; }  // Si es quien hizo la reserva
        public string? NombreReservante { get; set; }  // Nombre de quien reservó (si es acompañante)
        public int NumeroAcompanante { get; set; }     // Número de acompañante (0 si es principal)

        // Calcula cuánto paga según su tipo
        public decimal Tarifa
        {
            get
            {
                return Tipo switch
                {
                    TipoPasajero.Normal => 500m,
                    TipoPasajero.Estudiante => 250m,
                    TipoPasajero.Jubilado => 0m,
                    _ => 500m
                };
            }
        }

        // Constructor vacío
        public Pasajero()
        {
            Nombre = string.Empty;
            Tipo = TipoPasajero.Normal;
            HoraAnotacion = DateTime.Now;
            EsReservaPrincipal = true;
            NumeroAcompanante = 0;
        }

        // Constructor con datos
        public Pasajero(string nombre, TipoPasajero tipo, bool esReservaPrincipal = true, string? nombreReservante = null, int numeroAcompanante = 0)
        {
            Nombre = nombre;
            Tipo = tipo;
            HoraAnotacion = DateTime.Now;
            EsReservaPrincipal = esReservaPrincipal;
            NombreReservante = nombreReservante;
            NumeroAcompanante = numeroAcompanante;
        }

        // Devuelve el símbolo del tipo de pasajero
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

        // Devuelve el nombre del tipo de pasajero
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

        // Convierte el pasajero a texto para mostrarlo en pantalla
        public override string ToString()
        {
            string nombreCompleto = EsReservaPrincipal || string.IsNullOrEmpty(NombreReservante)
                ? Nombre
                : $"{NombreReservante} #{NumeroAcompanante}";
            
            return $"{ObtenerSimbolo()} {nombreCompleto} ({ObtenerDescripcionTipo()}) - ${Tarifa}";
        }

        // Convierte el pasajero a formato CSV para guardarlo en archivo
        // Formato: Nombre|Tipo|Fecha|EsReservaPrincipal|NombreReservante|NumeroAcompanante
        public string ToCsv()
        {
            return $"{Nombre}|{(int)Tipo}|{HoraAnotacion:yyyy-MM-dd HH:mm:ss}|{EsReservaPrincipal}|{NombreReservante ?? ""}|{NumeroAcompanante}";
        }

        // Crea un pasajero desde una línea CSV del archivo
        public static Pasajero FromCsv(string csv)
        {
            try
            {
                // Separo la línea por el caracter |
                string[] partes = csv.Split('|');
                if (partes.Length >= 3)
                {
                    var pasajero = new Pasajero
                    {
                        Nombre = partes[0],
                        Tipo = (TipoPasajero)int.Parse(partes[1]),
                        HoraAnotacion = DateTime.Parse(partes[2])
                    };

                    // Cargo los nuevos campos si existen
                    if (partes.Length >= 4)
                        pasajero.EsReservaPrincipal = bool.Parse(partes[3]);
                    
                    if (partes.Length >= 5 && !string.IsNullOrEmpty(partes[4]))
                        pasajero.NombreReservante = partes[4];
                    
                    if (partes.Length >= 6)
                        pasajero.NumeroAcompanante = int.Parse(partes[5]);

                    return pasajero;
                }
            }
            catch
            {
                // Si hay error, devuelvo un pasajero normal
            }

            return new Pasajero(csv, TipoPasajero.Normal);
        }
    }
}
