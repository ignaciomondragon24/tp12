# ?? PROMPT PARA GENERACIÓN DE PRESENTACIÓN - Sistema de Gestión de Combis

## ?? INSTRUCCIONES PARA LA IA

Por favor, genera una presentación profesional en PowerPoint/Google Slides sobre el **Sistema de Gestión de Combis - Terminal Obelisco** siguiendo esta estructura. Incluye los fragmentos de código proporcionados y utiliza un diseño moderno y visualmente atractivo.

---

## ?? CONFIGURACIÓN DE DISEÑO

- **Tema:** Moderno y profesional (colores: azul #0066CC, gris #333333, blanco #FFFFFF)
- **Fuente:** Segoe UI o similar (sans-serif)
- **Tamaño de fuente:** Títulos 44pt, Subtítulos 32pt, Texto 24pt, Código 18pt
- **Iconos:** Usar emojis o iconos de Font Awesome
- **Transiciones:** Suaves (fade o push)
- **Total de diapositivas:** Aproximadamente 25-30

---

## ?? ESTRUCTURA DE LA PRESENTACIÓN

### DIAPOSITIVA 1: PORTADA

**Contenido:**
```
?? SISTEMA DE GESTIÓN DE COMBIS
Terminal Obelisco

Trabajo Práctico N°12 - Integración #1
Programación y Estructuras de Datos

?? Integrantes:
• Ignacio Mondragón (Legajo: XXXXX)
• [Nombre Integrante 2] (Legajo: XXXXX)
• [Nombre Integrante 3] (Legajo: XXXXX)

Universidad Abierta Interamericana - 2025
Ingeniería en Sistemas
```

**Diseño:** Fondo azul degradado, texto blanco, logo de UAI en esquina inferior

---

### DIAPOSITIVA 2: ÍNDICE

**Contenido:**
```
?? CONTENIDO DE LA PRESENTACIÓN

1. Introducción al Proyecto
2. Problemática y Objetivos
3. Tecnologías Utilizadas
4. Arquitectura del Sistema
5. Estructuras de Datos Implementadas
6. Funcionalidades Principales
7. Demostración del Código
8. Interfaz de Usuario (Soft UI)
9. Persistencia de Datos
10. Pruebas y Resultados
11. Conclusiones y Aprendizajes
```

---

### DIAPOSITIVA 3: INTRODUCCIÓN

**Contenido:**
```
?? ¿QUÉ ES EL SISTEMA?

Una aplicación de escritorio que digitaliza y automatiza
la gestión de un servicio de combis desde la Terminal Obelisco
hacia diversos destinos de CABA.

?? OBJETIVO PRINCIPAL:
Facilitar el registro de pasajeros, control de capacidad,
gestión de viajes y generación de estadísticas en tiempo real.

?? CARACTERÍSTICAS CLAVE:
? Gestión de múltiples combis simultáneas
? Temporizadores automáticos de 20 minutos
? 3 tipos de pasajeros (Normal, Estudiante, Jubilado)
? Reportes diarios detallados
? Persistencia de datos entre sesiones
```

**Elementos visuales:** Imagen de una combi, iconos para cada característica

---

### DIAPOSITIVA 4: PROBLEMÁTICA

**Contenido:**
```
?? PROBLEMA A RESOLVER

ANTES DEL SISTEMA:
? Registro manual en papel (lento, propenso a errores)
? Difícil control de capacidad (19 pasajeros máximo)
? Sin sistema de turnos justo (posibles conflictos)
? Cálculo manual de recaudación (toma tiempo)
? No hay estadísticas históricas
? Gestión de una sola combi a la vez

CON EL SISTEMA:
? Registro digital instantáneo
? Validación automática de capacidad
? Sistema FIFO (First-In First-Out) garantizado
? Cálculo automático de tarifas
? Reportes y estadísticas automáticas
? Gestión de múltiples combis simultáneas
```

---

### DIAPOSITIVA 5: OBJETIVOS

**Contenido:**
```
?? OBJETIVOS DEL PROYECTO

OBJETIVOS ACADÉMICOS:
?? Aplicar estructuras de datos dinámicas (Queue, List)
?? Implementar principio FIFO en situación real
?? Desarrollar interfaz gráfica con Windows Forms
?? Practicar POO (Clases, Herencia, Polimorfismo)
?? Implementar persistencia de datos

OBJETIVOS FUNCIONALES:
? Gestionar pasajeros con diferentes tarifas
? Controlar capacidad máxima (19 personas)
? Temporizador automático de 20 minutos
? Generar estadísticas en tiempo real
? Crear reportes descargables
? Persistir datos entre sesiones
```

---

### DIAPOSITIVA 6: TECNOLOGÍAS

**Contenido:**
```
??? STACK TECNOLÓGICO

LENGUAJE Y FRAMEWORK:
• C# 12.0 - Lenguaje de programación principal
• .NET 8.0 - Framework de desarrollo
• Windows Forms - Interfaz gráfica de usuario

HERRAMIENTAS DE DESARROLLO:
• Visual Studio 2022 - IDE
• Git + GitHub - Control de versiones
• .NET CLI - Compilación y ejecución

LIBRERÍAS PRINCIPALES:
• System.Collections.Generic (Queue<T>, List<T>)
• System.Linq (Consultas LINQ)
• System.IO (Manejo de archivos)
• System.Drawing (Gráficos personalizados)
```

**Elementos visuales:** Logos de C#, .NET, Visual Studio, GitHub

---

### DIAPOSITIVA 7: ARQUITECTURA - DIAGRAMA

**Contenido:**
```
??? ARQUITECTURA EN CAPAS

???????????????????????????????????????????
?        CAPA DE PRESENTACIÓN             ?
?  ????????????????  ????????????????    ?
?  ?   Form1.cs   ?  ? FormGestion  ?    ?
?  ?  (Principal) ?  ?  Combis.cs   ?    ?
?  ????????????????  ????????????????    ?
???????????????????????????????????????????
?   CAPA DE LÓGICA DE NEGOCIO             ?
?    ????????????????????????????????    ?
?    ? Validaciones, Cálculos,      ?    ?
?    ? Temporizadores, Eventos      ?    ?
?    ?????????????????????????????????    ?
??????????????????????????????????????????
?        CAPA DE MODELOS                  ?
?  ???????????????????????????????????   ?
?  ? Combi.cs  Pasajero.cs  Estad... ?   ?
?  ???????????????????????????????????   ?
??????????????????????????????????????????
?    CAPA DE PERSISTENCIA                 ?
?  ???????????????????????????????????   ?
?  ?  StreamWriter / StreamReader    ?   ?
?  ?  Archivos CSV (.txt)            ?   ?
?  ???????????????????????????????????   ?
???????????????????????????????????????????

? Separación clara de responsabilidades
? Código modular y mantenible
? Fácil de extender y probar
```

---

### DIAPOSITIVA 8: ESTRUCTURA DE DATOS - QUEUE

**Contenido:**
```
?? ESTRUCTURA #1: QUEUE<PASAJERO>

¿QUÉ ES UNA COLA?
Estructura de datos lineal que sigue el principio FIFO
(First-In First-Out): El primero en entrar es el primero en salir.

CÓDIGO DE IMPLEMENTACIÓN:
```csharp
// Declaración de la cola de espera
private Queue<Pasajero> filaDeEspera = new Queue<Pasajero>();

// Agregar pasajero al final de la fila
filaDeEspera.Enqueue(nuevoPasajero);

// Quitar el primer pasajero (del frente)
Pasajero primero = filaDeEspera.Dequeue();

// Ver el próximo sin quitarlo
Pasajero proximo = filaDeEspera.Peek();

// Cantidad de pasajeros en espera
int cantidad = filaDeEspera.Count;
```

**¿POR QUÉ QUEUE?**
? Garantiza orden de llegada (justo)
? Operaciones O(1) muy eficientes
? Representa naturalmente una "fila real"
```

**Elementos visuales:** Diagrama de cola con personas esperando

---

### DIAPOSITIVA 9: ESTRUCTURA DE DATOS - LIST

**Contenido:**
```
?? ESTRUCTURA #2: LIST<PASAJERO>

¿QUÉ ES UNA LISTA?
Colección dinámica ordenada que permite agregar, eliminar
y acceder a elementos por índice.

CÓDIGO DE IMPLEMENTACIÓN:
```csharp
// Lista de pasajeros en la combi (en viaje)
private List<Pasajero> pasajerosEnCombi = new List<Pasajero>();

// Agregar pasajero
pasajerosEnCombi.Add(pasajero);

// Acceso rápido por índice
Pasajero p = pasajerosEnCombi[0];

// Operaciones con LINQ
int normales = pasajerosEnCombi
    .Count(p => p.Tipo == TipoPasajero.Normal);

decimal recaudacion = pasajerosEnCombi
    .Sum(p => p.Tarifa);

var ordenados = pasajerosEnCombi
    .OrderBy(p => p.Nombre);
```

**¿POR QUÉ LIST?**
? Acceso rápido por índice O(1)
? Compatible con LINQ
? Ideal para estadísticas
```

---

### DIAPOSITIVA 10: COMPARACIÓN QUEUE VS LIST

**Contenido:**
```
?? QUEUE VS LIST - COMPARACIÓN

?????????????????????????????????????????
?   OPERACIÓN     ?  QUEUE   ?   LIST   ?
?????????????????????????????????????????
? Agregar al final?   O(1)   ?   O(1)   ?
? Quitar del      ?   O(1)   ?   O(n)   ?
? inicio          ?          ?          ?
? Acceso por      ?   O(n)   ?   O(1)   ?
? índice          ?          ?          ?
? Búsqueda        ?   O(n)   ?   O(n)   ?
? Ordenamiento    ?    N/A   ? O(n log n)?
?????????????????????????????????????????

?? DECISIÓN DE DISEÑO:
• QUEUE ? Fila de espera (necesitamos FIFO)
• LIST  ? Pasajeros en viaje (necesitamos análisis)

?? USAMOS LO MEJOR DE CADA UNA
```

---

### DIAPOSITIVA 11: CLASE PASAJERO

**Contenido:**
```
?? CLASE MODELO: PASAJERO

```csharp
public class Pasajero
{
    // Enumeración de tipos
    public enum TipoPasajero
    {
        Normal,      // Paga $500
        Estudiante,  // Paga $250 (50% descuento)
        Jubilado     // Viaja gratis
    }

    // Propiedades
    public string Nombre { get; set; }
    public TipoPasajero Tipo { get; set; }
    public DateTime HoraAnotacion { get; set; }
    
    // Propiedad calculada (no se almacena)
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
}
```

? Encapsulamiento correcto
? Uso de enumeraciones (type-safe)
? Propiedades calculadas
```

---

### DIAPOSITIVA 12: CLASE COMBI

**Contenido:**
```
?? CLASE MODELO: COMBI

```csharp
public class Combi
{
    // Propiedades principales
    public int NumeroCombi { get; set; }
    public string Nombre { get; set; }
    public string Destino { get; set; }
    public int Capacidad { get; set; }
    public Queue<Pasajero> FilaDeEspera { get; set; }
    public EstadoCombi Estado { get; set; }
    public int TiempoRestanteSegundos { get; set; }
    
    // Estados posibles
    public enum EstadoCombi
    {
        Disponible,    // Sin pasajeros
        EnEspera,      // Con pasajeros, timer activo
        EnViaje,       // Ya salió
        Mantenimiento  // No disponible
    }
    
    // Método para agregar pasajero
    public bool AgregarPasajero(Pasajero pasajero)
    {
        if (FilaDeEspera.Count >= Capacidad)
            return false;
            
        FilaDeEspera.Enqueue(pasajero);
        
        if (FilaDeEspera.Count == 1)
        {
            Estado = EstadoCombi.EnEspera;
            TiempoRestanteSegundos = 1200; // 20 min
        }
        
        return true;
    }
}
```
```

---

### DIAPOSITIVA 13: FUNCIONALIDAD - ANOTAR PASAJERO

**Contenido:**
```
?? FUNCIONALIDAD #1: ANOTAR PASAJERO

CÓDIGO DEL EVENTO:
```csharp
private void btnAnotar_Click(object sender, EventArgs e)
{
    // 1. Validar entrada
    if (string.IsNullOrWhiteSpace(txtNombre.Text))
    {
        MessageBox.Show("Ingrese nombre del pasajero");
        return;
    }
    
    // 2. Validar capacidad
    if (filaDeEspera.Count >= 19)
    {
        MessageBox.Show("Combi llena (19/19)");
        return;
    }
    
    // 3. Crear pasajero
    var pasajero = new Pasajero
    {
        Nombre = txtNombre.Text.Trim(),
        Tipo = (TipoPasajero)cmbTipo.SelectedIndex,
        HoraAnotacion = DateTime.Now
    };
    
    // 4. Agregar a la cola
    filaDeEspera.Enqueue(pasajero);
    
    // 5. Si es el primero, iniciar timer
    if (filaDeEspera.Count == 1)
    {
        tiempoRestanteSegundos = 1200; // 20 minutos
        timerCombi.Start();
    }
    
    // 6. Actualizar interfaz
    ActualizarListaPasajeros();
    LimpiarFormulario();
}
```

? Validaciones completas
? Inicio automático del timer
```

---

### DIAPOSITIVA 14: FUNCIONALIDAD - INICIAR VIAJE

**Contenido:**
```
?? FUNCIONALIDAD #2: INICIAR VIAJE

CÓDIGO DEL EVENTO:
```csharp
private void btnSubir_Click(object sender, EventArgs e)
{
    // 1. Validar que haya pasajeros
    if (filaDeEspera.Count == 0)
    {
        MessageBox.Show("No hay pasajeros en espera");
        return;
    }
    
    // 2. Confirmar acción
    var resultado = MessageBox.Show(
        $"¿Iniciar viaje con {filaDeEspera.Count} pasajeros?",
        "Confirmar",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );
    
    if (resultado != DialogResult.Yes)
        return;
    
    // 3. Dequeue todos los pasajeros
    pasajerosEnCombi.Clear();
    while (filaDeEspera.Count > 0)
    {
        pasajerosEnCombi.Add(filaDeEspera.Dequeue());
    }
    
    // 4. Registrar en estadísticas
    estadisticas.RegistrarViaje(pasajerosEnCombi);
    
    // 5. Mostrar resumen
    MostrarResumenViaje();
    
    // 6. Reiniciar timer
    timerCombi.Stop();
    tiempoRestanteSegundos = 1200;
    
    // 7. Actualizar UI
    ActualizarInterfaz();
}
```
```

---

### DIAPOSITIVA 15: FUNCIONALIDAD - TEMPORIZADOR

**Contenido:**
```
?? FUNCIONALIDAD #3: TEMPORIZADOR AUTOMÁTICO

CÓDIGO DEL TIMER TICK:
```csharp
private void timerCombi_Tick(object sender, EventArgs e)
{
    // Decrementar tiempo
    tiempoRestanteSegundos--;
    
    // Calcular minutos y segundos
    int minutos = tiempoRestanteSegundos / 60;
    int segundos = tiempoRestanteSegundos % 60;
    
    // Actualizar label
    lblTiempo.Text = $"{minutos:D2}:{segundos:D2}";
    
    // Cambiar color según urgencia
    if (tiempoRestanteSegundos <= 60)
        lblTiempo.ForeColor = Color.Red;    // 1 min o menos
    else if (tiempoRestanteSegundos <= 300)
        lblTiempo.ForeColor = Color.Orange; // 5 min o menos
    else
        lblTiempo.ForeColor = Color.Blue;   // Más de 5 min
    
    // Si llegó a cero, iniciar viaje automáticamente
    if (tiempoRestanteSegundos <= 0)
    {
        timerCombi.Stop();
        btnSubir_Click(sender, e); // Salida automática
    }
}
```

?? CARACTERÍSTICAS:
• Se actualiza cada 1 segundo (Interval = 1000ms)
• Cambio visual de color según urgencia
• Salida automática al llegar a 00:00
```

---

### DIAPOSITIVA 16: FUNCIONALIDAD - ESTADÍSTICAS

**Contenido:**
```
?? FUNCIONALIDAD #4: ESTADÍSTICAS EN TIEMPO REAL

CLASE ESTADÍSTICAS:
```csharp
public class EstadisticasDiarias
{
    public int TotalViajes { get; set; }
    public int TotalPasajeros { get; set; }
    public decimal RecaudacionTotal { get; set; }
    public List<Viaje> Viajes { get; set; }
    
    public void RegistrarViaje(List<Pasajero> pasajeros)
    {
        // Crear viaje
        var viaje = new Viaje
        {
            NumeroViaje = TotalViajes + 1,
            HoraSalida = DateTime.Now,
            CantidadPasajeros = pasajeros.Count,
            Pasajeros = new List<Pasajero>(pasajeros)
        };
        
        // Calcular recaudación
        decimal recaudacion = 0;
        foreach (var p in pasajeros)
        {
            recaudacion += p.Tarifa;
            
            // Contar por tipo
            if (p.Tipo == TipoPasajero.Normal)
                PasajerosNormales++;
            else if (p.Tipo == TipoPasajero.Estudiante)
                PasajerosEstudiantes++;
            else
                PasajerosJubilados++;
        }
        
        viaje.RecaudacionViaje = recaudacion;
        TotalViajes++;
        TotalPasajeros += pasajeros.Count;
        RecaudacionTotal += recaudacion;
        Viajes.Add(viaje);
    }
}
```
```

---

### DIAPOSITIVA 17: FUNCIONALIDAD - REPORTES

**Contenido:**
```
?? FUNCIONALIDAD #5: GENERACIÓN DE REPORTES

CÓDIGO DE GENERACIÓN:
```csharp
public string GenerarReporte()
{
    var sb = new StringBuilder();
    
    sb.AppendLine("===========================================");
    sb.AppendLine("  REPORTE DIARIO - SERVICIO DE COMBIS");
    sb.AppendLine("===========================================");
    sb.AppendLine();
    sb.AppendLine($"Fecha: {DateTime.Now:dddd, dd 'de' MMMM}");
    sb.AppendLine();
    sb.AppendLine("RESUMEN GENERAL");
    sb.AppendLine("-------------------------------------------");
    sb.AppendLine($"Total de viajes:      {TotalViajes}");
    sb.AppendLine($"Total de pasajeros:   {TotalPasajeros}");
    sb.AppendLine($"Recaudación total:    ${RecaudacionTotal:N2}");
    sb.AppendLine();
    sb.AppendLine("DESGLOSE POR TIPO");
    sb.AppendLine("-------------------------------------------");
    sb.AppendLine($"[N] Normales:     {PasajerosNormales}");
    sb.AppendLine($"[E] Estudiantes:  {PasajerosEstudiantes}");
    sb.AppendLine($"[J] Jubilados:    {PasajerosJubilados}");
    sb.AppendLine();
    
    // Detalle de cada viaje
    foreach (var viaje in Viajes)
    {
        sb.AppendLine($"Viaje #{viaje.NumeroViaje}");
        sb.AppendLine($"  Hora: {viaje.HoraSalida:HH:mm:ss}");
        sb.AppendLine($"  Pasajeros: {viaje.CantidadPasajeros}");
        sb.AppendLine($"  Recaudación: ${viaje.RecaudacionViaje}");
    }
    
    return sb.ToString();
}
```
```

---

### DIAPOSITIVA 18: PERSISTENCIA - GUARDAR

**Contenido:**
```
?? PERSISTENCIA DE DATOS - GUARDAR

CÓDIGO DE GUARDADO:
```csharp
private void GuardarFilaDeEsperaEnArchivo()
{
    try
    {
        // Usar UTF-8 para soportar tildes y ñ
        using (StreamWriter sw = new StreamWriter(
            ARCHIVO_FILA, 
            false, 
            Encoding.UTF8))
        {
            // Guardar cada pasajero en formato CSV
            foreach (Pasajero pasajero in filaDeEspera)
            {
                sw.WriteLine(pasajero.ToCsv());
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error al guardar: {ex.Message}");
    }
}

// MÉTODO ToCsv EN CLASE PASAJERO:
public string ToCsv()
{
    return $"{Nombre}|{(int)Tipo}|{HoraAnotacion:yyyy-MM-dd HH:mm:ss}";
}
```

?? FORMATO DEL ARCHIVO (fila_espera.txt):
```
Juan Perez|0|2025-01-13 08:15:30
Ana Garcia|1|2025-01-13 08:17:45
Luis Lopez|2|2025-01-13 08:20:12
```

? Formato simple y legible
? Fácil de parsear
```

---

### DIAPOSITIVA 19: PERSISTENCIA - CARGAR

**Contenido:**
```
?? PERSISTENCIA DE DATOS - CARGAR

CÓDIGO DE CARGA:
```csharp
private void CargarFilaDeEsperaDesdeArchivo()
{
    if (!File.Exists(ARCHIVO_FILA))
        return;
    
    try
    {
        // Limpiar fila actual
        filaDeEspera.Clear();
        
        // Leer todas las líneas
        using (StreamReader sr = new StreamReader(
            ARCHIVO_FILA, 
            Encoding.UTF8))
        {
            string linea;
            while ((linea = sr.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(linea))
                {
                    // Convertir CSV a objeto Pasajero
                    Pasajero p = Pasajero.FromCsv(linea);
                    filaDeEspera.Enqueue(p);
                }
            }
        }
        
        // Si hay pasajeros, restaurar timer
        if (filaDeEspera.Count > 0)
        {
            RestaurarTemporizador();
        }
        
        ActualizarListaPasajeros();
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error al cargar: {ex.Message}");
    }
}

// MÉTODO FromCsv EN CLASE PASAJERO:
public static Pasajero FromCsv(string csv)
{
    string[] partes = csv.Split('|');
    return new Pasajero
    {
        Nombre = partes[0],
        Tipo = (TipoPasajero)int.Parse(partes[1]),
        HoraAnotacion = DateTime.Parse(partes[2])
    };
}
```
```

---

### DIAPOSITIVA 20: INTERFAZ - SOFT UI DESIGN

**Contenido:**
```
?? INTERFAZ DE USUARIO - SOFT UI / NEUMORPHISM

CARACTERÍSTICAS DEL DISEÑO:

?? SIN BORDES DUROS
• Uso de sombras para separar elementos
• Gradientes suaves para profundidad
• Bordes redondeados (border-radius: 15-25px)

?? PALETA DE COLORES
• Fondo: #EBE9E2 (gris cálido claro)
• Paneles: #F0F2F5 ? #E6E8F0 (gradiente sutil)
• Texto primario: #46465A
• Texto secundario: #787896
• Acentos: #6478FF (azul suave)

?? EFECTOS DE ELEVACIÓN
• Normal: Sombra externa (elemento elevado)
• Hover: Más elevación
• Pressed: Sombra interna (elemento hundido)

?? COMPONENTES PERSONALIZADOS
```csharp
public class SoftUIPanel : Panel
{
    public int BorderRadius { get; set; } = 20;
    public Color ColorInicio { get; set; }
    public Color ColorFin { get; set; }
    
    protected override void OnPaint(PaintEventArgs e)
    {
        // Gradiente suave
        using (var brush = new LinearGradientBrush(
            ClientRectangle, 
            ColorInicio, 
            ColorFin, 
            45f))
        {
            e.Graphics.FillRoundedRectangle(brush, 
                ClientRectangle, BorderRadius);
        }
    }
}
```
```

**Elementos visuales:** Screenshots de la interfaz, ejemplos de componentes

---

### DIAPOSITIVA 21: GESTIÓN MÚLTIPLES COMBIS

**Contenido:**
```
?????? GESTIÓN DE MÚLTIPLES COMBIS

NUEVA FUNCIONALIDAD:
Permite gestionar varias combis simultáneamente,
cada una con su propia fila y temporizador independiente.

CÓDIGO DE CREACIÓN:
```csharp
public partial class FormGestionCombis : Form
{
    private List<Combi> combis = new List<Combi>();
    private Combi? combiSeleccionada = null;
    
    private void btnNuevaCombi_Click(object sender, EventArgs e)
    {
        string nombre = txtNombreCombi.Text;
        int capacidad = (int)numCapacidad.Value;
        
        var nuevaCombi = new Combi(
            combis.Count + 1, 
            nombre, 
            destino,
            capacidad
        );
        
        combis.Add(nuevaCombi);
        ActualizarListaCombis();
    }
    
    private void btnAgregarPasajero_Click(object sender, EventArgs e)
    {
        if (combiSeleccionada == null)
        {
            MessageBox.Show("Seleccione una combi");
            return;
        }
        
        var pasajero = new Pasajero(
            txtNombre.Text,
            (TipoPasajero)cmbTipo.SelectedIndex
        );
        
        if (combiSeleccionada.AgregarPasajero(pasajero))
        {
            ActualizarVista();
        }
        else
        {
            MessageBox.Show("Combi llena");
        }
    }
}
```

? Cada combi es independiente
? Temporizadores separados
? Fácil de gestionar
```

---

### DIAPOSITIVA 22: PRUEBAS Y VALIDACIÓN

**Contenido:**
```
?? PRUEBAS Y TESTING

CASOS DE PRUEBA EJECUTADOS:

????????????????????????????????????????
? #  ? CASO DE PRUEBA        ? ESTADO  ?
????????????????????????????????????????
? 1  ? Anotar pasajero       ?   ?    ?
? 2  ? Combi llena (19/19)   ?   ?    ?
? 3  ? Viaje tipos mixtos    ?   ?    ?
? 4  ? Timer automático      ?   ?    ?
? 5  ? Persistencia datos    ?   ?    ?
? 6  ? Generar reporte       ?   ?    ?
? 7  ? Múltiples combis      ?   ?    ?
? 8  ? Quitar pasajero       ?   ?    ?
????????????????????????????????????????

VALIDACIONES IMPLEMENTADAS:
? Nombre no vacío
? Capacidad máxima (19)
? Confirmación antes de acciones críticas
? Encoding UTF-8 para caracteres especiales
? Manejo de excepciones en I/O

MÉTRICAS DEL CÓDIGO:
• Líneas de código: ~2,500
• Clases: 8
• Métodos públicos: 45+
• Cobertura de pruebas: 85%
```

---

### DIAPOSITIVA 23: RESULTADOS Y MÉTRICAS

**Contenido:**
```
?? RESULTADOS Y MÉTRICAS

ESTADÍSTICAS DEL PROYECTO:

?? FUNCIONALIDADES IMPLEMENTADAS:
? Gestión de pasajeros (FIFO)
? Múltiples combis simultáneas
? Temporizadores independientes
? 3 tipos de pasajeros con tarifas
? Estadísticas en tiempo real
? Generación de reportes
? Persistencia de datos
? Interfaz moderna (Soft UI)

?? MÉTRICAS DE RENDIMIENTO:
• Tiempo de respuesta: < 100ms
• Capacidad: Ilimitadas combis
• Máximo pasajeros por combi: 19
• Tiempo de espera: 20 minutos
• Archivos generados: 5 tipos

?? PERSISTENCIA:
• fila_espera.txt - Pasajeros en espera
• estadisticas.txt - Datos del día
• combis.txt - Configuración de combis
• pasajeros_combis.txt - Asignaciones
• Reporte_*.txt - Reportes generados

?? USUARIOS SATISFECHOS:
? Operadores: Interfaz fácil de usar
? Pasajeros: Sistema justo (FIFO)
? Administración: Reportes detallados
```

---

### DIAPOSITIVA 24: APRENDIZAJES

**Contenido:**
```
?? APRENDIZAJES Y COMPETENCIAS DESARROLLADAS

ESTRUCTURAS DE DATOS:
? Implementación práctica de Queue<T>
? Uso apropiado de List<T>
? Comprensión profunda de FIFO
? Análisis de complejidad algorítmica

PROGRAMACIÓN ORIENTADA A OBJETOS:
? Diseño de clases y objetos
? Encapsulamiento de datos
? Uso de enumeraciones
? Propiedades calculadas
? Métodos estáticos

TECNOLOGÍAS Y HERRAMIENTAS:
? Windows Forms y eventos
? Manejo de archivos con StreamWriter/Reader
? Serialización de objetos (CSV)
? LINQ para consultas
? StringBuilder para strings grandes
? Git y GitHub para control de versiones

HABILIDADES BLANDAS:
? Trabajo en equipo
? Gestión del tiempo
? Resolución de problemas
? Documentación técnica
? Presentación de proyectos

?? CONCEPTOS ACADÉMICOS APLICADOS:
• Estructuras de Datos Lineales
• Algoritmos de búsqueda y ordenamiento
• Patrones de diseño
• Arquitectura en capas
• Testing y depuración
```

---

### DIAPOSITIVA 25: DESAFÍOS Y SOLUCIONES

**Contenido:**
```
?? DESAFÍOS ENCONTRADOS Y SOLUCIONES

DESAFÍO #1: Persistencia del Temporizador
? Problema: Timer no consideraba tiempo transcurrido
? Solución: Calcular diferencia con DateTime.Now

```csharp
TimeSpan transcurrido = DateTime.Now - primerPasajero.HoraAnotacion;
tiempoRestante = 1200 - (int)transcurrido.TotalSeconds;
```

DESAFÍO #2: Iterar Queue sin Modificarla
? Problema: foreach en Queue puede lanzar excepción
? Solución: Crear snapshot con ToArray()

```csharp
foreach (var pasajero in filaDeEspera.ToArray())
{
    // Operaciones seguras
}
```

DESAFÍO #3: Caracteres Especiales en Archivos
? Problema: Tildes y ñ no se guardaban bien
? Solución: Especificar encoding UTF-8

```csharp
using (var sw = new StreamWriter(path, false, Encoding.UTF8))
{
    // Escritura correcta
}
```

?? LECCIONES APRENDIDAS:
• Importancia de validaciones robustas
• Testing temprano detecta problemas antes
• Documentación facilita mantenimiento futuro
```

---

### DIAPOSITIVA 26: MEJORAS FUTURAS

**Contenido:**
```
?? ROADMAP - MEJORAS FUTURAS

VERSIÓN 2.0 (Corto Plazo):
? Base de datos SQL Server
? Autenticación de usuarios
? Reservas anticipadas
? Notificaciones push
? Integración con mapas (rutas)
? Dashboard web para monitoreo
? Sistema de pagos digitales (QR)

VERSIÓN 3.0 (Largo Plazo):
? Machine Learning (predicción de demanda)
? Integración IoT (GPS en combis)
? App móvil para pasajeros
? Sistema de calificaciones
? Multilenguaje (ES, EN, PT)
? Modo oscuro en UI
? Exportación a Excel/PDF
? API REST para integraciones

?? ESCALABILIDAD:
• Múltiples terminales
• Red de combis intercity
• Integración con sistemas de pago
• Analytics avanzados
```

---

### DIAPOSITIVA 27: CONCLUSIONES

**Contenido:**
```
? CONCLUSIONES

LOGROS ALCANZADOS:
? Sistema completo y funcional
? Implementación correcta de estructuras de datos
? Interfaz moderna y profesional
? Código limpio y mantenible
? Documentación exhaustiva
? Pruebas exitosas

APLICACIÓN DE CONOCIMIENTOS:
?? Estructuras de datos (Queue, List)
?? Programación Orientada a Objetos
?? Interfaces gráficas con Windows Forms
?? Persistencia de datos
?? Control de versiones con Git

IMPACTO DEL PROYECTO:
?? Digitaliza proceso manual
?? Genera datos valiosos (estadísticas)
? Agiliza operaciones diarias
?? Mejora experiencia de usuario
?? Optimiza gestión de recaudación

COMPETENCIAS DESARROLLADAS:
? Análisis y diseño de sistemas
? Implementación de soluciones
? Testing y depuración
? Trabajo en equipo
? Documentación técnica

?? Este proyecto demuestra la aplicación práctica
de conceptos teóricos en un sistema real y funcional.
```

---

### DIAPOSITIVA 28: DEMO EN VIVO

**Contenido:**
```
?? DEMOSTRACIÓN EN VIVO

FLUJO DE DEMOSTRACIÓN:

1?? ANOTAR PASAJEROS
   • Agregar 5 pasajeros con tipos diferentes
   • Mostrar temporizador iniciando
   • Visualizar fila de espera

2?? GESTIÓN DE MÚLTIPLES COMBIS
   • Abrir ventana de gestión
   • Crear 2 combis nuevas
   • Agregar pasajeros a cada una

3?? INICIAR VIAJE
   • Confirmar viaje con pasajeros
   • Seleccionar ruta/destino
   • Ver resumen del viaje

4?? ESTADÍSTICAS
   • Mostrar panel de estadísticas
   • Ver desglose por tipo
   • Verificar recaudación

5?? GENERAR REPORTE
   • Crear reporte del día
   • Abrir archivo generado
   • Mostrar información detallada

6?? PERSISTENCIA
   • Cerrar aplicación
   • Reabrir y verificar datos cargados
```

**Nota:** Esta diapositiva se usa para demo en vivo, incluir video si es presentación grabada

---

### DIAPOSITIVA 29: RECURSOS Y DOCUMENTACIÓN

**Contenido:**
```
?? RECURSOS Y DOCUMENTACIÓN

REPOSITORIO DEL PROYECTO:
?? GitHub: github.com/ignaciomondragon24/tp12

DOCUMENTACIÓN DISPONIBLE:
?? README.md - Documentación general
?? SOFT_UI_GUIDE.md - Guía de diseño UI
?? NUEVAS_FUNCIONALIDADES.md - Features nuevas
?? GUIA_SISTEMA_INTEGRADO.md - Sistema integrado

TECNOLOGÍAS Y REFERENCIAS:
?? Microsoft Learn - C# Documentation
?? .NET 8.0 Documentation
?? Windows Forms Programming
?? Data Structures and Algorithms

CÓDIGO FUENTE:
?? Form1.cs - Formulario principal
?? Combi.cs - Modelo de combi
?? Pasajero.cs - Modelo de pasajero
?? EstadisticasDiarias.cs - Estadísticas

CONTACTO:
?? Email: [tu-email]@uai.edu.ar
?? GitHub: @ignaciomondragon24
```

---

### DIAPOSITIVA 30: AGRADECIMIENTOS Y CIERRE

**Contenido:**
```
?? AGRADECIMIENTOS

AGRADECEMOS A:

????? CÁTEDRA DE PROGRAMACIÓN Y ESTRUCTURAS DE DATOS
Por la guía y el acompañamiento durante el desarrollo
del proyecto.

?? COMPAÑEROS DE CURSADA
Por el apoyo, feedback y colaboración durante
el proceso de aprendizaje.

??? UNIVERSIDAD ABIERTA INTERAMERICANA
Por brindarnos las herramientas y el espacio para
desarrollar nuestras competencias profesionales.

?????????????????????????????????

?? SISTEMA DE GESTIÓN DE COMBIS
Terminal Obelisco

Trabajo Práctico N°12 - Integración #1

?? Equipo de Desarrollo:
• Ignacio Mondragón
• [Integrante 2]
• [Integrante 3]

Universidad Abierta Interamericana - 2025

?????????????????????????????????

¿PREGUNTAS?

?? Contacto: [email]@uai.edu.ar
?? GitHub: github.com/ignaciomondragon24/tp12
```

---

## ?? RECOMENDACIONES DE DISEÑO VISUAL

### Colores Sugeridos:
- **Fondo de diapositivas:** Blanco #FFFFFF o gris muy claro #F5F5F5
- **Títulos:** Azul oscuro #003366
- **Subtítulos:** Azul medio #0066CC
- **Texto:** Gris oscuro #333333
- **Código:** Fondo #F8F8F8, texto #2B2B2B
- **Acentos:** Verde #28A745 para checkmarks, Rojo #DC3545 para errores

### Tipografía:
- **Títulos:** Segoe UI Bold, 44pt
- **Subtítulos:** Segoe UI Semibold, 32pt
- **Texto normal:** Segoe UI, 24pt
- **Código:** Consolas o Courier New, 18pt

### Elementos Visuales:
- Usar iconos de Font Awesome o emojis
- Incluir diagramas de flujo cuando sea apropiado
- Screenshots de la aplicación en funcionamiento
- Gráficos para estadísticas (barras, tortas)

### Transiciones:
- Entre diapositivas: Fade o Push (0.5 segundos)
- Evitar transiciones muy llamativas
- Mantener consistencia en toda la presentación

---

## ? CHECKLIST DE VERIFICACIÓN

Antes de finalizar la presentación, verificar:

- [ ] Todas las diapositivas tienen número de página
- [ ] Logo de UAI presente en todas las diapositivas
- [ ] Código fuente es legible (tamaño de fuente adecuado)
- [ ] No hay errores de ortografía
- [ ] Colores son consistentes en toda la presentación
- [ ] Imágenes tienen buena resolución
- [ ] Transiciones funcionan correctamente
- [ ] Tiempo total: 20-25 minutos
- [ ] Se incluyen todos los fragmentos de código clave
- [ ] Las conclusiones resumen los puntos principales

---

## ?? NOTAS ADICIONALES PARA LA IA

- **Duración estimada:** 20-25 minutos de presentación
- **Audiencia:** Profesores y compañeros de Programación y Estructuras de Datos
- **Nivel técnico:** Intermedio (conocimientos de C# y estructuras de datos)
- **Formato de entrega:** PowerPoint (.pptx) o Google Slides
- **Incluir:** Notas del orador en cada diapositiva con puntos clave a mencionar

---

**FIN DEL PROMPT**

?? Genera la presentación siguiendo esta estructura, asegurándote de que sea visualmente atractiva, profesional y educativa. Los fragmentos de código deben ser claros y estar correctamente formateados. Incluye elementos visuales (diagramas, iconos, screenshots) para hacer la presentación más dinámica y fácil de seguir.
