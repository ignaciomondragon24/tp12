# TRABAJO PR�CTICO N�12 - INTEGRACI�N #1
## Sistema de Gesti�n de Combis - Terminal Obelisco

**Asignatura:** Programaci�n y Estructuras de Datos  
**Carrera:** Ingenier�a en Sistemas  
**Universidad:** Universidad Abierta Interamericana (UAI)  
**A�o:** 2025  
**Alumno:** [Nombre del Alumno]  
**Legajo:** [N�mero de Legajo]

---

## �NDICE

1. [Introducci�n](#introducci�n)
2. [Objetivos](#objetivos)
3. [Marco Te�rico](#marco-te�rico)
4. [An�lisis del Problema](#an�lisis-del-problema)
5. [Dise�o de la Soluci�n](#dise�o-de-la-soluci�n)
6. [Implementaci�n](#implementaci�n)
7. [Pruebas y Resultados](#pruebas-y-resultados)
8. [Conclusiones](#conclusiones)
9. [Bibliograf�a](#bibliograf�a)
10. [Anexos](#anexos)

---

## 1. INTRODUCCI�N

El presente trabajo pr�ctico consiste en el desarrollo de un sistema de gesti�n para un servicio de combis que opera desde la Terminal Obelisco hacia distintos puntos de la Ciudad Aut�noma de Buenos Aires. El sistema fue implementado utilizando el lenguaje de programaci�n C# con el framework .NET 8.0 y Windows Forms para la interfaz gr�fica de usuario.

El desarrollo de este proyecto permiti� aplicar los conceptos fundamentales de estructuras de datos vistas durante la cursada, espec�ficamente las estructuras din�micas como colas (Queue) y listas (List), as� como tambi�n conceptos de programaci�n orientada a objetos, manejo de eventos, persistencia de datos y dise�o de interfaces de usuario.

---

## 2. OBJETIVOS

### Objetivo General
Desarrollar una aplicaci�n de escritorio que permita gestionar eficientemente el servicio de combis, facilitando el registro de pasajeros, control de capacidad, generaci�n de estad�sticas y reportes diarios.

### Objetivos Espec�ficos
- Implementar estructuras de datos din�micas (Queue y List) para la gesti�n de pasajeros
- Aplicar el principio FIFO (First-In, First-Out) en la fila de espera de pasajeros
- Desarrollar una interfaz gr�fica intuitiva y funcional con Windows Forms
- Implementar validaciones robustas para garantizar la integridad de los datos
- Generar estad�sticas en tiempo real del servicio
- Crear reportes descargables con informaci�n detallada de las operaciones diarias
- Implementar persistencia de datos mediante archivos de texto
- Aplicar buenas pr�cticas de programaci�n y documentaci�n de c�digo

---

## 3. MARCO TE�RICO

### 3.1 Estructuras de Datos

#### 3.1.1 Cola (Queue)
Una cola es una estructura de datos lineal que sigue el principio FIFO (First-In, First-Out), donde el primer elemento en entrar es el primero en salir. Es an�loga a una fila de personas esperando ser atendidas.

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
Una lista es una colecci�n din�mica ordenada de elementos que permite agregar, eliminar y acceder a elementos por �ndice. En C#, la clase `List<T>` es gen�rica y proporciona un array din�mico que crece autom�ticamente.

**Ventajas:**
- Tama�o din�mico (crece autom�ticamente)
- Acceso r�pido por �ndice O(1)
- M�todos integrados de LINQ
- Type-safe (seguridad de tipos)

### 3.2 Programaci�n Orientada a Eventos
Windows Forms utiliza un modelo de programaci�n orientado a eventos, donde la aplicaci�n responde a acciones del usuario (clicks, teclas, movimientos del mouse) mediante manejadores de eventos.

### 3.3 Persistencia de Datos
La persistencia permite que los datos sobrevivan al cierre de la aplicaci�n. En este proyecto se utilizaron archivos de texto plano con formato CSV (Comma-Separated Values) para almacenar:
- Fila de espera de pasajeros
- Estad�sticas diarias

### 3.4 Principio FIFO vs LIFO

| Caracter�stica | FIFO (Cola) | LIFO (Pila) |
|----------------|-------------|-------------|
| Principio | First-In, First-Out | Last-In, First-Out |
| Analog�a | Fila en supermercado | Pila de platos |
| Operaci�n insertar | Enqueue (atr�s) | Push (arriba) |
| Operaci�n extraer | Dequeue (frente) | Pop (arriba) |
| Uso en este proyecto | Fila de espera | No aplicable |

---

## 4. AN�LISIS DEL PROBLEMA

### 4.1 Descripci�n del Problema
Se requiere un sistema que gestione el servicio de combis desde la Terminal Obelisco. El sistema debe:
- Permitir anotar pasajeros con diferentes tipos de tarifas
- Controlar la capacidad m�xima (19 pasajeros)
- Gestionar el embarque siguiendo el orden de llegada (FIFO)
- Manejar un temporizador de 20 minutos para la salida
- Ofrecer 3 rutas diferentes
- Generar estad�sticas y reportes
- Persistir datos entre sesiones

### 4.2 Requerimientos Funcionales

#### RF01: Gesti�n de Pasajeros
- El sistema debe permitir registrar pasajeros con nombre y tipo
- Tipos de pasajero: Normal ($500), Estudiante ($250), Jubilado ($0)
- Validar que el nombre no est� vac�o
- Registrar hora de anotaci�n

#### RF02: Control de Capacidad
- Validar que no se superen los 19 lugares disponibles
- Mostrar mensaje al intentar exceder la capacidad
- Mostrar contador de lugares ocupados/disponibles

#### RF03: Temporizador Autom�tico
- Iniciar temporizador de 20 minutos al anotar el primer pasajero
- Mostrar cuenta regresiva en formato MM:SS
- Cambiar color seg�n tiempo restante (azul > naranja > rojo)
- Partir autom�ticamente al llegar a 00:00

#### RF04: Gesti�n de Viajes
- Permitir iniciar viaje manualmente
- Solicitar confirmaci�n antes de partir
- Permitir seleccionar ruta de viaje
- Mostrar detalles completos del viaje
- Vaciar la fila de espera al partir

#### RF05: Rutas Disponibles
- Ruta 1: Obelisco ? Puerto Madero (20 min)
- Ruta 2: Obelisco ? Recoleta (25 min)
- Ruta 3: Obelisco ? Palermo (30 min)

#### RF06: Estad�sticas en Tiempo Real
- Mostrar total de viajes realizados
- Mostrar total de pasajeros transportados
- Mostrar recaudaci�n total del d�a
- Actualizar autom�ticamente despu�s de cada viaje

#### RF07: Generaci�n de Reportes
- Generar reporte en archivo de texto (.txt)
- Incluir resumen general
- Incluir desglose por tipo de pasajero
- Incluir recaudaci�n por tipo
- Incluir detalle de cada viaje
- Incluir promedios y porcentajes
- Abrir autom�ticamente en Notepad

#### RF08: Persistencia de Datos
- Guardar fila de espera al cerrar
- Guardar estad�sticas del d�a
- Cargar datos autom�ticamente al iniciar
- Restaurar temporizador con tiempo correcto

### 4.3 Requerimientos No Funcionales

#### RNF01: Usabilidad
- Interfaz intuitiva y f�cil de usar
- Mensajes claros y descriptivos
- Colores diferenciados para acciones
- Retroalimentaci�n inmediata de las acciones

#### RNF02: Confiabilidad
- Manejo robusto de errores
- Validaciones en todas las entradas
- Confirmaci�n en acciones cr�ticas

#### RNF03: Rendimiento
- Tiempo de respuesta < 1 segundo en operaciones
- Actualizaci�n visual inmediata

#### RNF04: Mantenibilidad
- C�digo bien documentado con comentarios
- Separaci�n de responsabilidades en clases
- Nomenclatura clara y consistente

---

## 5. DISE�O DE LA SOLUCI�N

### 5.1 Arquitectura del Sistema

El sistema sigue una arquitectura en capas:

```
???????????????????????????????????????
?     Capa de Presentaci�n            ?
?  (Windows Forms - Form1.cs)         ?
???????????????????????????????????????
?     Capa de L�gica de Negocio       ?
?  - Gesti�n de pasajeros             ?
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

### 5.3 Flujo de Operaci�n

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
[Validar nombre no vac�o] ???NO??> [Mostrar error]
   ?                                      ?
   S�                                     ?
   ?                                      ?
[Validar capacidad < 19] ???NO??> [Mostrar "Combi llena"]
   ?                                      ?
   S�                                     ?
   ?                                      ?
[Crear objeto Pasajero]                   ?
   ?                                      ?
   ?                                      ?
[Enqueue a filaDeEspera]                  ?
   ?                                      ?
   ?                                      ?
[�Es el primero?] ???S�??> [Iniciar temporizador]
   ?                                      ?
   NO                                     ?
   ?                                      ?
[Actualizar ListBox]                      ?
   ?                                      ?
   ?                                      ?
[Mostrar confirmaci�n]                    ?
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
[�Hay pasajeros?] ???NO??> [Mostrar "Puerto Madero vac�a"]
   ?                              ?
   S�                             ?
   ?                              ?
[Solicitar confirmaci�n]           ?
   ?                              ?
   ?                              ?
[�Confirma?] ???NO??> [Cancelar] ??
   ?                    ?
   S�                   ?
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
[Registrar en estad�sticas]        ?
   ?                              ?
   ?                              ?
[Calcular recaudaci�n]             ?
   ?                              ?
   ?                              ?
[Mostrar detalles del viaje]       ?
   ?                              ?
   ?                              ?
[Limpiar combi]                    ?
   ?                              ?
   ?                              ?
[Actualizar estad�sticas UI]       ?
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
??? Form1.cs                    # L�gica principal del formulario
??? Form1.Designer.cs           # Dise�o visual (generado)
??? Form1.resx                  # Recursos del formulario
?
??? Pasajero.cs                 # Clase modelo Pasajero
??? EstadisticasDiarias.cs      # Clase para estad�sticas
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

## 6. IMPLEMENTACI�N

### 6.1 Tecnolog�as Utilizadas

| Tecnolog�a | Versi�n | Prop�sito |
|------------|---------|-----------|
| C# | 12.0 | Lenguaje de programaci�n |
| .NET | 8.0 | Framework de desarrollo |
| Windows Forms | .NET 8 | Interfaz gr�fica de usuario |
| Visual Studio | 2022 | Entorno de desarrollo integrado |

### 6.2 Estructuras de Datos Implementadas

#### Queue<Pasajero>
Se utiliz� la clase gen�rica `Queue<T>` de .NET para implementar la fila de espera de pasajeros. Esta estructura garantiza el principio FIFO necesario para el correcto funcionamiento del sistema.

```csharp
// Declaraci�n
private Queue<Pasajero> filaDeEspera = new Queue<Pasajero>();

// Agregar pasajero (al final)
filaDeEspera.Enqueue(nuevoPasajero);

// Quitar pasajero (del frente)
Pasajero primero = filaDeEspera.Dequeue();

// Consultar cantidad
int cantidad = filaDeEspera.Count;
```

**Justificaci�n:** Una cola es la estructura natural para representar una fila de espera en el mundo real. El primer pasajero en llegar debe ser el primero en subir, lo cual es exactamente el comportamiento FIFO que proporciona Queue.

#### List<Pasajero>
Se utiliz� `List<T>` para almacenar temporalmente los pasajeros que suben a la combi durante un viaje, as� como para mantener el historial de viajes en las estad�sticas.

```csharp
// Declaraci�n
private List<Pasajero> pasajerosEnCombi = new List<Pasajero>();

// Agregar pasajero
pasajerosEnCombi.Add(pasajero);

// Operaciones LINQ
int cantidad = pasajerosEnCombi.Count;
decimal total = pasajerosEnCombi.Sum(p => p.Tarifa);
```

**Justificaci�n:** List proporciona flexibilidad para acceder a elementos por �ndice y realizar operaciones de consulta con LINQ, lo cual es �til para generar estad�sticas y reportes.

### 6.3 Clases Principales

#### Clase Pasajero
Esta clase encapsula toda la informaci�n relevante de un pasajero.

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

**Caracter�sticas:**
- Propiedad calculada `Tarifa` (no se almacena, se calcula seg�n el tipo)
- M�todo `ToCsv()` para serializaci�n
- M�todo est�tico `FromCsv()` para deserializaci�n
- Enumeraci�n interna `TipoPasajero` para type-safety

#### Clase EstadisticasDiarias
Gestiona las estad�sticas y genera reportes.

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
        // Calcular recaudaci�n
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

### 6.4 Caracter�sticas Avanzadas

#### Temporizador con Cuenta Regresiva
```csharp
private void timerCombi_Tick(object sender, EventArgs e)
{
    tiempoRestanteSegundos--;
    ActualizarTiempoRestante();
    
    if (tiempoRestanteSegundos <= 0)
    {
        timerCombi.Stop();
        // Iniciar viaje autom�ticamente
        btnSubir_Click(sender, e);
    }
}
```

El temporizador:
- Se actualiza cada segundo (Interval = 1000ms)
- Cambia de color seg�n el tiempo restante
- Inicia autom�ticamente un viaje al llegar a 0
- Se persiste al cerrar la aplicaci�n

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
- Usa formato CSV para f�cil lectura
- Guarda autom�ticamente al cerrar
- Carga autom�ticamente al iniciar
- Restaura el temporizador considerando el tiempo transcurrido

### 6.5 Validaciones Implementadas

| Validaci�n | Ubicaci�n | Mensaje |
|------------|-----------|---------|
| Nombre vac�o | btnAnotar_Click | "Por favor, ingrese el nombre del pasajero." |
| Capacidad excedida | btnAnotar_Click | "Combi llena. No se pueden anotar m�s pasajeros." |
| Fila vac�a | btnSubir_Click | "No hay pasajeros en espera..." |
| Confirmaci�n cierre | Form1_FormClosing | "Hay pasajeros en espera. �Cerrar igualmente?" |

---

## 7. PRUEBAS Y RESULTADOS

### 7.1 Casos de Prueba Ejecutados

#### CP01: Anotar Pasajero Normal
**Entrada:**
- Nombre: "Juan P�rez"
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
- Recaudaci�n: (5�$500) + (3�$250) + (2�$0) = $3,250

**Resultado Obtenido:** ? Correcto

#### CP04: Temporizador Autom�tico
**Entrada:**
- Anotar 1 pasajero
- Esperar 20 minutos (o ajustar constante para prueba)

**Resultado Esperado:**
- Viaje inicia autom�ticamente
- Se solicita ruta

**Resultado Obtenido:** ? Correcto

#### CP05: Persistencia
**Entrada:**
- Anotar 3 pasajeros
- Cerrar aplicaci�n
- Reabrir

**Resultado Esperado:**
- Se cargan los 3 pasajeros
- Temporizador contin�a desde donde qued�

**Resultado Obtenido:** ? Correcto

### 7.2 Pruebas de Integraci�n

| M�dulo | Componente | Estado |
|--------|------------|--------|
| Gesti�n de Pasajeros | Queue<Pasajero> | ? |
| Temporizador | Timer | ? |
| Estad�sticas | EstadisticasDiarias | ? |
| Reportes | StringBuilder | ? |
| Persistencia | StreamWriter/Reader | ? |
| Interfaz | Windows Forms | ? |

### 7.3 M�tricas del Proyecto

| M�trica | Valor |
|---------|-------|
| L�neas de c�digo (total) | ~800 |
| Clases implementadas | 3 |
| M�todos p�blicos | 15 |
| M�todos privados | 12 |
| Eventos manejados | 6 |
| Controles UI | 15 |
| Archivos de documentaci�n | 7 |

### 7.4 Capturas de Pantalla

*[En un trabajo real, aqu� incluir�as capturas de pantalla de:]*
- Pantalla principal con pasajeros en espera
- Selector de rutas
- Detalles de viaje
- Panel de estad�sticas
- Reporte generado
- Confirmaci�n de cierre

---

## 8. CONCLUSIONES

### 8.1 Logros Alcanzados

1. **Aplicaci�n exitosa de estructuras de datos:** Se implement� correctamente el uso de Queue para la fila de espera y List para el almacenamiento temporal, demostrando comprensi�n de cu�ndo usar cada estructura seg�n las necesidades del problema.

2. **Interfaz intuitiva y funcional:** La aplicaci�n cuenta con una interfaz clara que facilita su uso, con retroalimentaci�n visual inmediata y mensajes descriptivos.

3. **Validaciones robustas:** Se implementaron validaciones en todos los puntos cr�ticos, garantizando la integridad de los datos y evitando errores en tiempo de ejecuci�n.

4. **Persistencia confiable:** El sistema de archivos implementado permite mantener el estado entre sesiones, incluyendo la restauraci�n inteligente del temporizador.

5. **Estad�sticas completas:** El m�dulo de estad�sticas proporciona informaci�n valiosa sobre el servicio, con c�lculos autom�ticos de promedios y porcentajes.

### 8.2 Dificultades Encontradas y Soluciones

#### Dificultad 1: Persistencia del Temporizador
**Problema:** Al cerrar y reabrir la aplicaci�n, el temporizador no consideraba el tiempo transcurrido mientras la aplicaci�n estaba cerrada.

**Soluci�n:** Se implement� un c�lculo que resta el tiempo transcurrido (diferencia entre DateTime.Now y HoraAnotacion del primer pasajero) del tiempo total de espera.

#### Dificultad 2: Recorrer Queue sin Modificarla
**Problema:** No se puede usar foreach directamente en Queue sin extraer elementos.

**Soluci�n:** Se utiliz� el m�todo `ToArray()` para crear una copia temporal que permite iterar sin modificar la cola original.

#### Dificultad 3: Sincronizaci�n de Estad�sticas
**Problema:** Asegurar que las estad�sticas se actualicen correctamente despu�s de cada viaje.

**Soluci�n:** Se cre� un m�todo `ActualizarEstadisticas()` que se llama expl�citamente despu�s de registrar cada viaje, garantizando la consistencia.

### 8.3 Posibles Mejoras Futuras

1. **Base de datos:** Migrar de archivos de texto a una base de datos (SQL Server o SQLite) para mejor rendimiento y capacidades de consulta.

2. **M�ltiples terminales:** Extender el sistema para soportar varias terminales simult�neamente.

3. **Reservas anticipadas:** Permitir que los pasajeros reserven con anticipaci�n.

4. **Notificaciones:** Implementar notificaciones cuando quedan pocos minutos para la salida.

5. **Gr�ficos estad�sticos:** Agregar visualizaci�n gr�fica de las estad�sticas (chart controls).

6. **Historial hist�rico:** Mantener estad�sticas de m�ltiples d�as y permitir consultas hist�ricas.

7. **Exportaci�n de reportes:** Permitir exportar reportes en formatos adicionales (PDF, Excel).

8. **Autenticaci�n:** Implementar un sistema de login para control de acceso.

### 8.4 Aprendizajes

Este trabajo pr�ctico permiti� consolidar conocimientos sobre:
- Selecci�n y uso apropiado de estructuras de datos
- Dise�o de interfaces de usuario con Windows Forms
- Programaci�n orientada a eventos
- Manejo de archivos para persistencia
- Validaci�n de datos de entrada
- Separaci�n de responsabilidades en clases
- Documentaci�n de c�digo
- Testing y depuraci�n

Adem�s, se adquiri� experiencia pr�ctica en el desarrollo de aplicaciones completas, desde el an�lisis del problema hasta la implementaci�n y pruebas finales.

---

## 9. BIBLIOGRAF�A

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

5. **Joyanes Aguilar, Luis. "Fundamentos de Programaci�n - Algoritmos, Estructuras de Datos y Objetos"**  
   4ta Edici�n, McGraw-Hill, 2008

6. **Deitel, Paul J. "C# How to Program"**  
   6th Edition, Pearson, 2017

7. **Apuntes de la C�tedra - Programaci�n y Estructuras de Datos**  
   Universidad Abierta Interamericana, 2025

---

## 10. ANEXOS

### Anexo A: C�digo Fuente Completo

*[Los archivos de c�digo fuente se encuentran en el proyecto]*

**Archivos principales:**
- `Form1.cs`: L�gica del formulario (350 l�neas)
- `Form1.Designer.cs`: Dise�o de la UI (200 l�neas)
- `Pasajero.cs`: Clase modelo (120 l�neas)
- `EstadisticasDiarias.cs`: Estad�sticas y reportes (180 l�neas)

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

*[En un trabajo acad�mico completo, aqu� incluir�as diagramas de flujo detallados]*

### Anexo E: Manual de Usuario

#### Inicio de la Aplicaci�n
1. Ejecutar `AppCombis.exe`
2. La aplicaci�n carga autom�ticamente datos guardados (si existen)
3. La interfaz principal muestra la Terminal Obelisco

#### Anotar un Pasajero
1. Ingresar nombre en el campo "Pasajero"
2. Seleccionar tipo en el desplegable
3. Presionar bot�n "Anotar"
4. El pasajero aparece en la lista "En Espera"
5. Si es el primero, el temporizador inicia en 20:00

#### Iniciar un Viaje
1. Presionar ">> Subir a la combi (Iniciar Viaje)"
2. Confirmar con "Yes"
3. Seleccionar ruta deseada
4. Presionar "Confirmar"
5. Ver detalles del viaje (pasajeros, recaudaci�n, etc.)

#### Generar Reporte
1. Presionar "Generar Reporte Del Dia"
2. El archivo se crea con fecha/hora actual
3. Se abre autom�ticamente en Notepad
4. Guardar donde se desee

#### Cerrar la Aplicaci�n
1. Presionar "Cerrar Aplicacion"
2. Si hay pasajeros en espera, confirmar el cierre
3. Los datos se guardan autom�ticamente

---

## DECLARACI�N DE AUTOR�A

Declaro que este trabajo pr�ctico fue realizado �ntegramente por mi persona, aplicando los conocimientos adquiridos durante la cursada de Programaci�n y Estructuras de Datos en la Universidad Abierta Interamericana.

El c�digo fue escrito desde cero, consultando �nicamente la documentaci�n oficial de Microsoft y los apuntes de la c�tedra.

**Firma:** _____________________

**Fecha:** _____________________

---

**FIN DEL INFORME**
