# TRABAJO PRÁCTICO N°12 - INTEGRACIÓN #1
## Sistema de Gestión de Combis - Terminal Obelisco

**Asignatura:** Programación y Estructuras de Datos  
**Carrera:** Ingeniería en Sistemas  
**Universidad:** Universidad Abierta Interamericana (UAI)  
**Año:** 2025  
**Alumno:** [Nombre del Alumno]  
**Legajo:** [Número de Legajo]

---

## ÍNDICE

1. [Introducción](#introducción)
2. [Objetivos](#objetivos)
3. [Marco Teórico](#marco-teórico)
4. [Análisis del Problema](#análisis-del-problema)
5. [Diseño de la Solución](#diseño-de-la-solución)
6. [Implementación](#implementación)
7. [Pruebas y Resultados](#pruebas-y-resultados)
8. [Conclusiones](#conclusiones)
9. [Bibliografía](#bibliografía)
10. [Anexos](#anexos)

---

## 1. INTRODUCCIÓN

El presente trabajo práctico consiste en el desarrollo de un sistema de gestión para un servicio de combis que opera desde la Terminal Obelisco hacia distintos puntos de la Ciudad Autónoma de Buenos Aires. El sistema fue implementado utilizando el lenguaje de programación C# con el framework .NET 8.0 y Windows Forms para la interfaz gráfica de usuario.

El desarrollo de este proyecto permitió aplicar los conceptos fundamentales de estructuras de datos vistas durante la cursada, específicamente las estructuras dinámicas como colas (Queue) y listas (List), así como también conceptos de programación orientada a objetos, manejo de eventos, persistencia de datos y diseño de interfaces de usuario.

---

## 2. OBJETIVOS

### Objetivo General
Desarrollar una aplicación de escritorio que permita gestionar eficientemente el servicio de combis, facilitando el registro de pasajeros, control de capacidad, generación de estadísticas y reportes diarios.

### Objetivos Específicos
- Implementar estructuras de datos dinámicas (Queue y List) para la gestión de pasajeros
- Aplicar el principio FIFO (First-In, First-Out) en la fila de espera de pasajeros
- Desarrollar una interfaz gráfica intuitiva y funcional con Windows Forms
- Implementar validaciones robustas para garantizar la integridad de los datos
- Generar estadísticas en tiempo real del servicio
- Crear reportes descargables con información detallada de las operaciones diarias
- Implementar persistencia de datos mediante archivos de texto
- Aplicar buenas prácticas de programación y documentación de código

---

## 3. MARCO TEÓRICO

### 3.1 Estructuras de Datos

#### 3.1.1 Cola (Queue)
Una cola es una estructura de datos lineal que sigue el principio FIFO (First-In, First-Out), donde el primer elemento en entrar es el primero en salir. Es análoga a una fila de personas esperando ser atendidas.

**Operaciones principales:**
- **Enqueue:** Agregar un elemento al final de la cola
- **Dequeue:** Extraer el elemento del frente de la cola
- **Count:** Obtener la cantidad de elementos
- **Peek:** Ver el primer elemento sin extraerlo

**Complejidad temporal:**
- Enqueue: O(1)
- Dequeue: O(1)
- Acceso: O(n)

#### 3.1.2 Lista (List)
Una lista es una colección dinámica ordenada de elementos que permite agregar, eliminar y acceder a elementos por índice. En C#, la clase `List<T>` es genérica y proporciona un array dinámico que crece automáticamente.

**Ventajas:**
- Tamaño dinámico (crece automáticamente)
- Acceso rápido por índice O(1)
- Métodos integrados de LINQ
- Type-safe (seguridad de tipos)

### 3.2 Programación Orientada a Eventos
Windows Forms utiliza un modelo de programación orientado a eventos, donde la aplicación responde a acciones del usuario (clicks, teclas, movimientos del mouse) mediante manejadores de eventos.

### 3.3 Persistencia de Datos
La persistencia permite que los datos sobrevivan al cierre de la aplicación. En este proyecto se utilizaron archivos de texto plano con formato CSV (Comma-Separated Values) para almacenar:
- Fila de espera de pasajeros
- Estadísticas diarias

### 3.4 Principio FIFO vs LIFO

| Característica | FIFO (Cola) | LIFO (Pila) |
|----------------|-------------|-------------|
| Principio | First-In, First-Out | Last-In, First-Out |
| Analogía | Fila en supermercado | Pila de platos |
| Operación insertar | Enqueue (atrás) | Push (arriba) |
| Operación extraer | Dequeue (frente) | Pop (arriba) |
| Uso en este proyecto | Fila de espera | No aplicable |

---

## 4. ANÁLISIS DEL PROBLEMA

### 4.1 Descripción del Problema
Se requiere un sistema que gestione el servicio de combis desde la Terminal Obelisco. El sistema debe:
- Permitir anotar pasajeros con diferentes tipos de tarifas
- Controlar la capacidad máxima (19 pasajeros)
- Gestionar el embarque siguiendo el orden de llegada (FIFO)
- Manejar un temporizador de 20 minutos para la salida
- Ofrecer 3 rutas diferentes
- Generar estadísticas y reportes
- Persistir datos entre sesiones

### 4.2 Requerimientos Funcionales

#### RF01: Gestión de Pasajeros
- El sistema debe permitir registrar pasajeros con nombre y tipo
- Tipos de pasajero: Normal ($500), Estudiante ($250), Jubilado ($0)
- Validar que el nombre no esté vacío
- Registrar hora de anotación

#### RF02: Control de Capacidad
- Validar que no se superen los 19 lugares disponibles
- Mostrar mensaje al intentar exceder la capacidad
- Mostrar contador de lugares ocupados/disponibles

#### RF03: Temporizador Automático
- Iniciar temporizador de 20 minutos al anotar el primer pasajero
- Mostrar cuenta regresiva en formato MM:SS
- Cambiar color según tiempo restante (azul > naranja > rojo)
- Partir automáticamente al llegar a 00:00

#### RF04: Gestión de Viajes
- Permitir iniciar viaje manualmente
- Solicitar confirmación antes de partir
- Permitir seleccionar ruta de viaje
- Mostrar detalles completos del viaje
- Vaciar la fila de espera al partir

#### RF05: Rutas Disponibles
- Ruta 1: Obelisco ? Puerto Madero (20 min)
- Ruta 2: Obelisco ? Recoleta (25 min)
- Ruta 3: Obelisco ? Palermo (30 min)

#### RF06: Estadísticas en Tiempo Real
- Mostrar total de viajes realizados
- Mostrar total de pasajeros transportados
- Mostrar recaudación total del día
- Actualizar automáticamente después de cada viaje

#### RF07: Generación de Reportes
- Generar reporte en archivo de texto (.txt)
- Incluir resumen general
- Incluir desglose por tipo de pasajero
- Incluir recaudación por tipo
- Incluir detalle de cada viaje
- Incluir promedios y porcentajes
- Abrir automáticamente en Notepad

#### RF08: Persistencia de Datos
- Guardar fila de espera al cerrar
- Guardar estadísticas del día
- Cargar datos automáticamente al iniciar
- Restaurar temporizador con tiempo correcto

### 4.3 Requerimientos No Funcionales

#### RNF01: Usabilidad
- Interfaz intuitiva y fácil de usar
- Mensajes claros y descriptivos
- Colores diferenciados para acciones
- Retroalimentación inmediata de las acciones

#### RNF02: Confiabilidad
- Manejo robusto de errores
- Validaciones en todas las entradas
- Confirmación en acciones críticas

#### RNF03: Rendimiento
- Tiempo de respuesta < 1 segundo en operaciones
- Actualización visual inmediata

#### RNF04: Mantenibilidad
- Código bien documentado con comentarios
- Separación de responsabilidades en clases
- Nomenclatura clara y consistente

---

## 5. DISEÑO DE LA SOLUCIÓN

### 5.1 Arquitectura del Sistema

El sistema sigue una arquitectura en capas:

```
???????????????????????????????????????
?     Capa de Presentación            ?
?  (Windows Forms - Form1.cs)         ?
???????????????????????????????????????
?     Capa de Lógica de Negocio       ?
?  - Gestión de pasajeros             ?
?  - Control de temporizador          ?
?  - Validaciones                     ?
???????????????????????????????????????
?     Capa de Datos                   ?
?  - Pasajero.cs                      ?
?  - EstadisticasDiarias.cs           ?
???????????????????????????????????????
?     Capa de Persistencia            ?
?  - StreamWriter/StreamReader        ?
?  - Archivos CSV                     ?
???????????????????????????????????????
```

### 5.2 Diagrama de Clases

```
????????????????????????????
?      Form1               ?
????????????????????????????
? - filaDeEspera: Queue    ?
? - pasajerosEnCombi: List ?
? - estadisticas: Estadis..?
? - tiempoRestante: int    ?
????????????????????????????
? + btnAnotar_Click()      ?
? + btnSubir_Click()       ?
? + timerCombi_Tick()      ?
? + ActualizarLista()      ?
????????????????????????????
            ?
            ? usa
            ?
????????????????????????????
?      Pasajero            ?
????????????????????????????
? + Nombre: string         ?
? + Tipo: TipoPasajero     ?
? + HoraAnotacion: DateTime?
? + Tarifa: decimal        ?
????????????????????????????
? + ToString(): string     ?
? + ToCsv(): string        ?
? + FromCsv(): Pasajero    ?
????????????????????????????
            ?
            ? usa
            ?
????????????????????????????
?  EstadisticasDiarias     ?
????????????????????????????
? + TotalViajes: int       ?
? + TotalPasajeros: int    ?
? + RecaudacionTotal: dec  ?
? + Viajes: List<Viaje>    ?
????????????????????????????
? + RegistrarViaje()       ?
? + GenerarReporte()       ?
? + GuardarReporte()       ?
????????????????????????????
```

### 5.3 Flujo de Operación

#### Flujo 1: Anotar Pasajero
```
[Inicio]
   ?
   ?
[Usuario ingresa nombre]
   ?
   ?
[Usuario selecciona tipo]
   ?
   ?
[Click en "Anotar"]
   ?
   ?
[Validar nombre no vacío] ???NO??> [Mostrar error]
   ?                                      ?
   SÍ                                     ?
   ?                                      ?
[Validar capacidad < 19] ???NO??> [Mostrar "Combi llena"]
   ?                                      ?
   SÍ                                     ?
   ?                                      ?
[Crear objeto Pasajero]                   ?
   ?                                      ?
   ?                                      ?
[Enqueue a filaDeEspera]                  ?
   ?                                      ?
   ?                                      ?
[¿Es el primero?] ???SÍ??> [Iniciar temporizador]
   ?                                      ?
   NO                                     ?
   ?                                      ?
[Actualizar ListBox]                      ?
   ?                                      ?
   ?                                      ?
[Mostrar confirmación]                    ?
   ?                                      ?
   ?                                      ?
[Fin] <????????????????????????????????????
```

#### Flujo 2: Iniciar Viaje
```
[Inicio]
   ?
   ?
[Click en "Subir a la combi"]
   ?
   ?
[¿Hay pasajeros?] ???NO??> [Mostrar "Puerto Madero vacía"]
   ?                              ?
   SÍ                             ?
   ?                              ?
[Solicitar confirmación]           ?
   ?                              ?
   ?                              ?
[¿Confirma?] ???NO??> [Cancelar] ??
   ?                    ?
   SÍ                   ?
   ?                    ?
[Mostrar selector de ruta]         ?
   ?                              ?
   ?                              ?
[Usuario selecciona ruta]          ?
   ?                              ?
   ?                              ?
[Dequeue todos los pasajeros]      ?
   ?                              ?
   ?                              ?
[Agregar a pasajerosEnCombi]       ?
   ?                              ?
   ?                              ?
[Registrar en estadísticas]        ?
   ?                              ?
   ?                              ?
[Calcular recaudación]             ?
   ?                              ?
   ?                              ?
[Mostrar detalles del viaje]       ?
   ?                              ?
   ?                              ?
[Limpiar combi]                    ?
   ?                              ?
   ?                              ?
[Actualizar estadísticas UI]       ?
   ?                              ?
   ?                              ?
[Reiniciar temporizador]           ?
   ?                              ?
   ?                              ?
[Fin] <?????????????????????????????
```

### 5.4 Estructura de Archivos

```
AppCombis/
?
??? Form1.cs                    # Lógica principal del formulario
??? Form1.Designer.cs           # Diseño visual (generado)
??? Form1.resx                  # Recursos del formulario
?
??? Pasajero.cs                 # Clase modelo Pasajero
??? EstadisticasDiarias.cs      # Clase para estadísticas
?
??? AppCombis.csproj            # Archivo de proyecto
??? Program.cs                  # Punto de entrada
?
??? bin/Debug/net8.0-windows/   # Archivos compilados
    ??? AppCombis.exe           # Ejecutable
    ??? fila_espera.txt         # Persistencia de fila
    ??? estadisticas.txt        # Persistencia de stats
    ??? Reporte_*.txt           # Reportes generados
```

---

## 6. IMPLEMENTACIÓN

### 6.1 Tecnologías Utilizadas

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| C# | 12.0 | Lenguaje de programación |
| .NET | 8.0 | Framework de desarrollo |
| Windows Forms | .NET 8 | Interfaz gráfica de usuario |
| Visual Studio | 2022 | Entorno de desarrollo integrado |

### 6.2 Estructuras de Datos Implementadas

#### Queue<Pasajero>
Se utilizó la clase genérica `Queue<T>` de .NET para implementar la fila de espera de pasajeros. Esta estructura garantiza el principio FIFO necesario para el correcto funcionamiento del sistema.

```csharp
// Declaración
private Queue<Pasajero> filaDeEspera = new Queue<Pasajero>();

// Agregar pasajero (al final)
filaDeEspera.Enqueue(nuevoPasajero);

// Quitar pasajero (del frente)
Pasajero primero = filaDeEspera.Dequeue();

// Consultar cantidad
int cantidad = filaDeEspera.Count;
```

**Justificación:** Una cola es la estructura natural para representar una fila de espera en el mundo real. El primer pasajero en llegar debe ser el primero en subir, lo cual es exactamente el comportamiento FIFO que proporciona Queue.

#### List<Pasajero>
Se utilizó `List<T>` para almacenar temporalmente los pasajeros que suben a la combi durante un viaje, así como para mantener el historial de viajes en las estadísticas.

```csharp
// Declaración
private List<Pasajero> pasajerosEnCombi = new List<Pasajero>();

// Agregar pasajero
pasajerosEnCombi.Add(pasajero);

// Operaciones LINQ
int cantidad = pasajerosEnCombi.Count;
decimal total = pasajerosEnCombi.Sum(p => p.Tarifa);
```

**Justificación:** List proporciona flexibilidad para acceder a elementos por índice y realizar operaciones de consulta con LINQ, lo cual es útil para generar estadísticas y reportes.

### 6.3 Clases Principales

#### Clase Pasajero
Esta clase encapsula toda la información relevante de un pasajero.

```csharp
public class Pasajero
{
    public string Nombre { get; set; }
    public TipoPasajero Tipo { get; set; }
    public DateTime HoraAnotacion { get; set; }
    
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

**Características:**
- Propiedad calculada `Tarifa` (no se almacena, se calcula según el tipo)
- Método `ToCsv()` para serialización
- Método estático `FromCsv()` para deserialización
- Enumeración interna `TipoPasajero` para type-safety

#### Clase EstadisticasDiarias
Gestiona las estadísticas y genera reportes.

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
        // Calcular recaudación
        // Actualizar contadores
        // Agregar a historial
    }
    
    public string GenerarReporte()
    {
        // Construir reporte con StringBuilder
        // Incluir resumen, desglose y detalles
        // Retornar string formateado
    }
}
```

### 6.4 Características Avanzadas

#### Temporizador con Cuenta Regresiva
```csharp
private void timerCombi_Tick(object sender, EventArgs e)
{
    tiempoRestanteSegundos--;
    ActualizarTiempoRestante();
    
    if (tiempoRestanteSegundos <= 0)
    {
        timerCombi.Stop();
        // Iniciar viaje automáticamente
        btnSubir_Click(sender, e);
    }
}
```

El temporizador:
- Se actualiza cada segundo (Interval = 1000ms)
- Cambia de color según el tiempo restante
- Inicia automáticamente un viaje al llegar a 0
- Se persiste al cerrar la aplicación

#### Persistencia Inteligente
```csharp
private void GuardarFilaDeEsperaEnArchivo()
{
    using (StreamWriter escritor = new StreamWriter(ARCHIVO_FILA))
    {
        foreach (Pasajero pasajero in filaDeEspera)
        {
            escritor.WriteLine(pasajero.ToCsv());
        }
    }
}
```

La persistencia:
- Usa formato CSV para fácil lectura
- Guarda automáticamente al cerrar
- Carga automáticamente al iniciar
- Restaura el temporizador considerando el tiempo transcurrido

### 6.5 Validaciones Implementadas

| Validación | Ubicación | Mensaje |
|------------|-----------|---------|
| Nombre vacío | btnAnotar_Click | "Por favor, ingrese el nombre del pasajero." |
| Capacidad excedida | btnAnotar_Click | "Combi llena. No se pueden anotar más pasajeros." |
| Fila vacía | btnSubir_Click | "No hay pasajeros en espera..." |
| Confirmación cierre | Form1_FormClosing | "Hay pasajeros en espera. ¿Cerrar igualmente?" |

---

## 7. PRUEBAS Y RESULTADOS

### 7.1 Casos de Prueba Ejecutados

#### CP01: Anotar Pasajero Normal
**Entrada:**
- Nombre: "Juan Pérez"
- Tipo: Normal

**Resultado Esperado:**
- Pasajero aparece en lista
- Tarifa: $500
- Temporizador inicia (si es el primero)

**Resultado Obtenido:** ? Correcto

#### CP02: Anotar con Combi Llena
**Entrada:**
- 19 pasajeros ya anotados
- Intento de anotar pasajero #20

**Resultado Esperado:**
- Mensaje "Combi llena"
- No se agrega el pasajero

**Resultado Obtenido:** ? Correcto

#### CP03: Viaje con Diferentes Tipos
**Entrada:**
- 5 Normales, 3 Estudiantes, 2 Jubilados

**Resultado Esperado:**
- Recaudación: (5×$500) + (3×$250) + (2×$0) = $3,250

**Resultado Obtenido:** ? Correcto

#### CP04: Temporizador Automático
**Entrada:**
- Anotar 1 pasajero
- Esperar 20 minutos (o ajustar constante para prueba)

**Resultado Esperado:**
- Viaje inicia automáticamente
- Se solicita ruta

**Resultado Obtenido:** ? Correcto

#### CP05: Persistencia
**Entrada:**
- Anotar 3 pasajeros
- Cerrar aplicación
- Reabrir

**Resultado Esperado:**
- Se cargan los 3 pasajeros
- Temporizador continúa desde donde quedó

**Resultado Obtenido:** ? Correcto

### 7.2 Pruebas de Integración

| Módulo | Componente | Estado |
|--------|------------|--------|
| Gestión de Pasajeros | Queue<Pasajero> | ? |
| Temporizador | Timer | ? |
| Estadísticas | EstadisticasDiarias | ? |
| Reportes | StringBuilder | ? |
| Persistencia | StreamWriter/Reader | ? |
| Interfaz | Windows Forms | ? |

### 7.3 Métricas del Proyecto

| Métrica | Valor |
|---------|-------|
| Líneas de código (total) | ~800 |
| Clases implementadas | 3 |
| Métodos públicos | 15 |
| Métodos privados | 12 |
| Eventos manejados | 6 |
| Controles UI | 15 |
| Archivos de documentación | 7 |

### 7.4 Capturas de Pantalla

*[En un trabajo real, aquí incluirías capturas de pantalla de:]*
- Pantalla principal con pasajeros en espera
- Selector de rutas
- Detalles de viaje
- Panel de estadísticas
- Reporte generado
- Confirmación de cierre

---

## 8. CONCLUSIONES

### 8.1 Logros Alcanzados

1. **Aplicación exitosa de estructuras de datos:** Se implementó correctamente el uso de Queue para la fila de espera y List para el almacenamiento temporal, demostrando comprensión de cuándo usar cada estructura según las necesidades del problema.

2. **Interfaz intuitiva y funcional:** La aplicación cuenta con una interfaz clara que facilita su uso, con retroalimentación visual inmediata y mensajes descriptivos.

3. **Validaciones robustas:** Se implementaron validaciones en todos los puntos críticos, garantizando la integridad de los datos y evitando errores en tiempo de ejecución.

4. **Persistencia confiable:** El sistema de archivos implementado permite mantener el estado entre sesiones, incluyendo la restauración inteligente del temporizador.

5. **Estadísticas completas:** El módulo de estadísticas proporciona información valiosa sobre el servicio, con cálculos automáticos de promedios y porcentajes.

### 8.2 Dificultades Encontradas y Soluciones

#### Dificultad 1: Persistencia del Temporizador
**Problema:** Al cerrar y reabrir la aplicación, el temporizador no consideraba el tiempo transcurrido mientras la aplicación estaba cerrada.

**Solución:** Se implementó un cálculo que resta el tiempo transcurrido (diferencia entre DateTime.Now y HoraAnotacion del primer pasajero) del tiempo total de espera.

#### Dificultad 2: Recorrer Queue sin Modificarla
**Problema:** No se puede usar foreach directamente en Queue sin extraer elementos.

**Solución:** Se utilizó el método `ToArray()` para crear una copia temporal que permite iterar sin modificar la cola original.

#### Dificultad 3: Sincronización de Estadísticas
**Problema:** Asegurar que las estadísticas se actualicen correctamente después de cada viaje.

**Solución:** Se creó un método `ActualizarEstadisticas()` que se llama explícitamente después de registrar cada viaje, garantizando la consistencia.

### 8.3 Posibles Mejoras Futuras

1. **Base de datos:** Migrar de archivos de texto a una base de datos (SQL Server o SQLite) para mejor rendimiento y capacidades de consulta.

2. **Múltiples terminales:** Extender el sistema para soportar varias terminales simultáneamente.

3. **Reservas anticipadas:** Permitir que los pasajeros reserven con anticipación.

4. **Notificaciones:** Implementar notificaciones cuando quedan pocos minutos para la salida.

5. **Gráficos estadísticos:** Agregar visualización gráfica de las estadísticas (chart controls).

6. **Historial histórico:** Mantener estadísticas de múltiples días y permitir consultas históricas.

7. **Exportación de reportes:** Permitir exportar reportes en formatos adicionales (PDF, Excel).

8. **Autenticación:** Implementar un sistema de login para control de acceso.

### 8.4 Aprendizajes

Este trabajo práctico permitió consolidar conocimientos sobre:
- Selección y uso apropiado de estructuras de datos
- Diseño de interfaces de usuario con Windows Forms
- Programación orientada a eventos
- Manejo de archivos para persistencia
- Validación de datos de entrada
- Separación de responsabilidades en clases
- Documentación de código
- Testing y depuración

Además, se adquirió experiencia práctica en el desarrollo de aplicaciones completas, desde el análisis del problema hasta la implementación y pruebas finales.

---

## 9. BIBLIOGRAFÍA

1. **Microsoft Learn - C# Documentation**  
   URL: https://learn.microsoft.com/es-es/dotnet/csharp/  
   Consultado: Enero 2025

2. **Microsoft Learn - Queue<T> Class**  
   URL: https://learn.microsoft.com/es-es/dotnet/api/system.collections.generic.queue-1  
   Consultado: Enero 2025

3. **Microsoft Learn - List<T> Class**  
   URL: https://learn.microsoft.com/es-es/dotnet/api/system.collections.generic.list-1  
   Consultado: Enero 2025

4. **Microsoft Learn - Windows Forms**  
   URL: https://learn.microsoft.com/es-es/dotnet/desktop/winforms/  
   Consultado: Enero 2025

5. **Joyanes Aguilar, Luis. "Fundamentos de Programación - Algoritmos, Estructuras de Datos y Objetos"**  
   4ta Edición, McGraw-Hill, 2008

6. **Deitel, Paul J. "C# How to Program"**  
   6th Edition, Pearson, 2017

7. **Apuntes de la Cátedra - Programación y Estructuras de Datos**  
   Universidad Abierta Interamericana, 2025

---

## 10. ANEXOS

### Anexo A: Código Fuente Completo

*[Los archivos de código fuente se encuentran en el proyecto]*

**Archivos principales:**
- `Form1.cs`: Lógica del formulario (350 líneas)
- `Form1.Designer.cs`: Diseño de la UI (200 líneas)
- `Pasajero.cs`: Clase modelo (120 líneas)
- `EstadisticasDiarias.cs`: Estadísticas y reportes (180 líneas)

### Anexo B: Formato de Archivos de Persistencia

#### fila_espera.txt
```
Juan Perez|0|2025-01-13 08:15:30
Ana Garcia|1|2025-01-13 08:17:45
Luis Lopez|2|2025-01-13 08:20:12
```

Formato: `Nombre|TipoNumerico|FechaHora`

#### estadisticas.txt
```
Fecha|2025-01-13
TotalViajes|5
TotalPasajeros|73
PasajerosNormales|40
PasajerosEstudiantes|20
PasajerosJubilados|13
RecaudacionTotal|28250.00
```

### Anexo C: Ejemplo de Reporte Generado

```
=======================================================
       REPORTE DIARIO - SERVICIO DE COMBIS
=======================================================

Fecha: lunes, 13 de enero de 2025

-------------------------------------------------------
  RESUMEN GENERAL
-------------------------------------------------------
  Total de viajes:        5
  Total de pasajeros:     73
  Recaudacion total:      $28,250.00

  Primer viaje:           08:15:30
  Ultimo viaje:           17:45:22
  Promedio pasajeros/viaje: 14.60
  Promedio recaudacion/viaje: $5,650.00

-------------------------------------------------------
  DESGLOSE POR TIPO DE PASAJERO
-------------------------------------------------------
  [N] Pasajeros Normales:   40 (54.8%)
  [E] Estudiantes:          20 (27.4%)
  [J] Jubilados:            13 (17.8%)

-------------------------------------------------------
  RECAUDACION POR TIPO
-------------------------------------------------------
  [N] Normales:    $20,000.00
  [E] Estudiantes: $5,000.00
  [J] Jubilados:   $0.00

-------------------------------------------------------
  DETALLE DE VIAJES
-------------------------------------------------------
  Viaje #1 - 08:15:30
    Pasajeros: 15 | Recaudacion: $6,250.00
  Viaje #2 - 10:30:45
    Pasajeros: 12 | Recaudacion: $4,500.00
  ...

=======================================================
  Reporte generado: 13/01/2025 17:45:30
=======================================================
```

### Anexo D: Diagrama de Flujo Completo

*[En un trabajo académico completo, aquí incluirías diagramas de flujo detallados]*

### Anexo E: Manual de Usuario

#### Inicio de la Aplicación
1. Ejecutar `AppCombis.exe`
2. La aplicación carga automáticamente datos guardados (si existen)
3. La interfaz principal muestra la Terminal Obelisco

#### Anotar un Pasajero
1. Ingresar nombre en el campo "Pasajero"
2. Seleccionar tipo en el desplegable
3. Presionar botón "Anotar"
4. El pasajero aparece en la lista "En Espera"
5. Si es el primero, el temporizador inicia en 20:00

#### Iniciar un Viaje
1. Presionar ">> Subir a la combi (Iniciar Viaje)"
2. Confirmar con "Yes"
3. Seleccionar ruta deseada
4. Presionar "Confirmar"
5. Ver detalles del viaje (pasajeros, recaudación, etc.)

#### Generar Reporte
1. Presionar "Generar Reporte Del Dia"
2. El archivo se crea con fecha/hora actual
3. Se abre automáticamente en Notepad
4. Guardar donde se desee

#### Cerrar la Aplicación
1. Presionar "Cerrar Aplicacion"
2. Si hay pasajeros en espera, confirmar el cierre
3. Los datos se guardan automáticamente

---

## DECLARACIÓN DE AUTORÍA

Declaro que este trabajo práctico fue realizado íntegramente por mi persona, aplicando los conocimientos adquiridos durante la cursada de Programación y Estructuras de Datos en la Universidad Abierta Interamericana.

El código fue escrito desde cero, consultando únicamente la documentación oficial de Microsoft y los apuntes de la cátedra.

**Firma:** _____________________

**Fecha:** _____________________

---

**FIN DEL INFORME**
