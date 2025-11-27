# ?? Sistema de Gestión de Combis - Terminal Obelisco

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-blue)
![License](https://img.shields.io/badge/License-Academic-green)

> Sistema completo de gestión para servicios de combis con múltiples funcionalidades: gestión de pasajeros, temporizadores automáticos, estadísticas en tiempo real y reportes detallados.

---

## ?? INTEGRANTES DEL PROYECTO

| Nombre Completo | Legajo | Email | Rol |
|-----------------|--------|-------|-----|
| **Ignacio Mondragón** | [Tu Legajo] | [tu-email@uai.edu.ar] | Desarrollador Full Stack |
| **[Nombre Integrante 2]** | [Legajo 2] | [email2@uai.edu.ar] | Desarrollador / Diseñador |
| **[Nombre Integrante 3]** | [Legajo 3] | [email3@uai.edu.ar] | Analista / Tester |

**Asignatura:** Programación y Estructuras de Datos  
**Carrera:** Ingeniería en Sistemas  
**Universidad:** Universidad Abierta Interamericana (UAI)  
**Año Académico:** 2025  
**Trabajo Práctico:** N°12 - Integración #1  

---

## ?? ÍNDICE

1. [Descripción del Proyecto](#-descripción-del-proyecto)
2. [Características Principales](#-características-principales)
3. [Tecnologías Utilizadas](#-tecnologías-utilizadas)
4. [Arquitectura del Sistema](#-arquitectura-del-sistema)
5. [Instalación y Configuración](#-instalación-y-configuración)
6. [Manual de Usuario](#-manual-de-usuario)
7. [Estructura del Proyecto](#-estructura-del-proyecto)
8. [Estructuras de Datos](#-estructuras-de-datos)
9. [Capturas de Pantalla](#-capturas-de-pantalla)
10. [Documentación Adicional](#-documentación-adicional)
11. [Autores y Agradecimientos](#-autores-y-agradecimientos)

---

## ?? DESCRIPCIÓN DEL PROYECTO

El **Sistema de Gestión de Combis - Terminal Obelisco** es una aplicación de escritorio desarrollada en C# con Windows Forms que permite gestionar eficientemente el servicio de transporte de combis desde la Terminal Obelisco hacia diversos puntos de la Ciudad Autónoma de Buenos Aires.

### Problemática que Resuelve

La aplicación digitaliza y automatiza el proceso manual de:
- ? Anotación de pasajeros en filas de espera
- ? Control de capacidad de vehículos (19 pasajeros)
- ? Gestión de diferentes tarifas (Normal, Estudiante, Jubilado)
- ? Temporizador automático de 20 minutos para salidas
- ? Múltiples combis operando simultáneamente
- ? Generación de estadísticas y reportes diarios
- ? Persistencia de datos entre sesiones

### Objetivos Académicos

Este proyecto integra conceptos fundamentales de:
- **Estructuras de Datos**: Colas (Queue), Listas (List), Enumeraciones
- **POO**: Clases, Encapsulamiento, Herencia, Polimorfismo
- **Eventos**: Programación orientada a eventos con Windows Forms
- **Persistencia**: Manejo de archivos CSV para almacenamiento de datos
- **Algoritmos**: FIFO (First-In First-Out), búsqueda, ordenamiento
- **Diseño de Software**: Separación de capas, patrones de diseño

---

## ? CARACTERÍSTICAS PRINCIPALES

### ?? Gestión de Pasajeros

```csharp
// Tipos de pasajero con tarifas diferenciadas
public enum TipoPasajero
{
    Normal,      // $500
    Estudiante,  // $250 (50% descuento)
    Jubilado     // $0 (viaje gratuito)
}
```

- ? **Registro de pasajeros** con nombre, tipo y hora de anotación
- ? **Validación de capacidad** (máximo 19 pasajeros por combi)
- ? **Cálculo automático de tarifas** según tipo de pasajero
- ? **Cola FIFO** para orden de embarque justo
- ? **Reservas grupales** con acompañantes

### ?? Gestión de Múltiples Combis

- ? **Crear combis personalizadas** con nombre y capacidad configurables
- ? **Gestión simultánea** de múltiples vehículos
- ? **Estados de combi**: Disponible, En Espera, En Viaje, Mantenimiento
- ? **Temporizadores independientes** por cada combi
- ? **Selección de rutas/destinos**: Puerto Madero, Recoleta, Palermo, Retiro, etc.

### ?? Temporizador Automático

```csharp
private void timerCombi_Tick(object sender, EventArgs e)
{
    tiempoRestanteSegundos--;
    ActualizarTiempoRestante();
    
    if (tiempoRestanteSegundos <= 0)
    {
        // Salida automática al llegar a 00:00
        IniciarViaje();
    }
}
```

- ? **Inicio automático** al anotar el primer pasajero
- ? **Cuenta regresiva** de 20 minutos (formato MM:SS)
- ? **Cambio de color** según urgencia (azul ? naranja ? rojo)
- ? **Salida automática** al alcanzar 00:00
- ? **Persistencia inteligente** del tiempo transcurrido

### ?? Estadísticas en Tiempo Real

```csharp
public class EstadisticasDiarias
{
    public int TotalViajes { get; set; }
    public int TotalPasajeros { get; set; }
    public decimal RecaudacionTotal { get; set; }
    public List<Viaje> Viajes { get; set; }
}
```

- ? **Contador de viajes** realizados en el día
- ? **Total de pasajeros** transportados
- ? **Recaudación total** del día
- ? **Desglose por tipo** de pasajero (Normal, Estudiante, Jubilado)
- ? **Promedios automáticos** (pasajeros/viaje, recaudación/viaje)
- ? **Horarios**: Primer y último viaje del día

### ?? Generación de Reportes

```
=======================================================
       REPORTE DIARIO - SERVICIO DE COMBIS
=======================================================

Fecha: lunes, 13 de enero de 2025

-------------------------------------------------------
  RESUMEN GENERAL
-------------------------------------------------------
  Total de viajes:        12
  Total de pasajeros:     187
  Recaudacion total:      $73,250.00
  
  Promedio pasajeros/viaje: 15.58
  Promedio recaudacion/viaje: $6,104.17
```

- ? **Reportes completos** con resumen, desglose y detalles
- ? **Exportación a TXT** con formato profesional
- ? **Apertura automática** en Notepad
- ? **Información detallada** por cada viaje
- ? **Estadísticas calculadas** (porcentajes, promedios)

### ?? Persistencia de Datos

- ? **Guardado automático** al cerrar la aplicación
- ? **Carga automática** al iniciar
- ? **Formato CSV** para fácil lectura y edición
- ? **Archivos generados**:
  - `fila_espera.txt` - Cola de pasajeros
  - `estadisticas.txt` - Datos del día
  - `combis.txt` - Configuración de combis
  - `pasajeros_combis.txt` - Asignación de pasajeros
  - `Reporte_YYYYMMDD_HHmmss.txt` - Reportes generados

### ?? Interfaz Moderna (Soft UI / Neumorphism)

- ? **Diseño minimalista** sin bordes duros
- ? **Efectos de elevación** con sombras suaves
- ? **Cards personalizados** para visualización de datos
- ? **Sidebar de navegación** con iconos
- ? **Colores armoniosos** y profesionales
- ? **Animaciones fluidas** en interacciones

---

## ??? TECNOLOGÍAS UTILIZADAS

| Tecnología | Versión | Descripción |
|------------|---------|-------------|
| **C#** | 12.0 | Lenguaje de programación principal |
| **.NET** | 8.0 | Framework de desarrollo |
| **Windows Forms** | .NET 8 | Framework para interfaz gráfica |
| **Visual Studio** | 2022 | IDE de desarrollo |
| **Git** | 2.x | Control de versiones |
| **GitHub** | - | Repositorio remoto |

### Librerías y Componentes Utilizados

```csharp
using System.Collections.Generic;  // Queue<T>, List<T>
using System.Linq;                 // LINQ queries
using System.IO;                   // StreamWriter, StreamReader
using System.Text;                 // StringBuilder
using System.Drawing;              // Graphics, Color, Font
using System.Windows.Forms;        // Form, Controls
```

---

## ??? ARQUITECTURA DEL SISTEMA

### Diagrama de Arquitectura en Capas

```
???????????????????????????????????????????
?    CAPA DE PRESENTACIÓN                 ?
?  ????????????????  ??????????????????? ?
?  ?   Form1.cs   ?  ? FormGestion     ? ?
?  ?  (Principal) ?  ?   Combis.cs     ? ?
?  ????????????????  ??????????????????? ?
?        ?                    ?           ?
?        ??????????????????????           ?
???????????????????????????????????????????
?    CAPA DE LÓGICA DE NEGOCIO            ?
?        ???????????????????              ?
?        ?  Validaciones   ?              ?
?        ?  Cálculos       ?              ?
?        ?  Temporizadores ?              ?
?        ???????????????????              ?
???????????????????????????????????????????
?    CAPA DE MODELOS/DATOS                ?
?  ????????????  ??????????????????????  ?
?  ? Combi.cs ?  ?   Pasajero.cs      ?  ?
?  ????????????  ??????????????????????  ?
?  ???????????????????????????????????   ?
?  ? EstadisticasDiarias.cs          ?   ?
?  ???????????????????????????????????   ?
?                 ?                       ?
???????????????????????????????????????????
?    CAPA DE PERSISTENCIA                 ?
?  ??????????????????????????????????    ?
?  ?  StreamWriter / StreamReader   ?    ?
?  ?  Archivos CSV (.txt)           ?    ?
?  ??????????????????????????????????    ?
???????????????????????????????????????????
```

### Diagrama de Clases UML

```
???????????????????????
?      Form1          ?
???????????????????????
? - filaDeEspera      ???????
? - estadisticas      ?     ?
? - timerCombi        ?     ?
???????????????????????     ?
? + btnAnotar_Click() ?     ?
? + btnSubir_Click()  ?     ?
? + timerTick()       ?     ?
???????????????????????     ?
           ?                ?
           ? usa            ?
           ?                ?
???????????????????????     ?
?   Pasajero          ???????
???????????????????????
? + Nombre: string    ?
? + Tipo: enum        ?
? + HoraAnotacion     ?
? + Tarifa: decimal   ?
???????????????????????
? + ToCsv()           ?
? + FromCsv()         ?
???????????????????????
           ?
           ? usa
           ?
???????????????????????
?   Combi             ?
???????????????????????
? + NumeroCombi: int  ?
? + Nombre: string    ?
? + FilaDeEspera      ?
? + Capacidad: int    ?
? + Estado: enum      ?
???????????????????????
? + AgregarPasajero() ?
? + QuitarPasajero()  ?
? + IniciarViaje()    ?
???????????????????????
```

---

## ?? INSTALACIÓN Y CONFIGURACIÓN

### Requisitos Previos

- **Sistema Operativo**: Windows 10/11
- **.NET Runtime**: 8.0 o superior
- **RAM**: Mínimo 2GB
- **Espacio en Disco**: 50MB

### Opción 1: Ejecutar desde Código Fuente

```bash
# 1. Clonar el repositorio
git clone https://github.com/ignaciomondragon24/tp12.git

# 2. Navegar a la carpeta del proyecto
cd tp12/AppCombis

# 3. Restaurar dependencias
dotnet restore

# 4. Compilar el proyecto
dotnet build

# 5. Ejecutar la aplicación
dotnet run
```

### Opción 2: Visual Studio 2022

1. Abrir `AppCombis.sln` en Visual Studio 2022
2. Presionar **F5** o click en "? Iniciar"
3. La aplicación se compilará y ejecutará automáticamente

### Opción 3: Ejecutable Precompilado

1. Navegar a `AppCombis/bin/Release/net8.0-windows/`
2. Ejecutar `AppCombis.exe`

---

## ?? MANUAL DE USUARIO

### Pantalla Principal

#### 1?? Anotar un Pasajero

```
???????????????????????????????
? Nombre: [Juan Perez      ] ?
? Tipo:   [? Normal         ] ?
? [      Anotar Pasajero    ] ?
???????????????????????????????
```

1. Ingresar el **nombre** del pasajero
2. Seleccionar el **tipo** (Normal $500, Estudiante $250, Jubilado $0)
3. Click en **"Anotar Pasajero"**
4. El pasajero aparece en la lista "En Espera"
5. Si es el primero, el temporizador inicia en **20:00**

#### 2?? Iniciar un Viaje

1. Click en **">> Subir a la combi (Iniciar Viaje)"**
2. Confirmar con **"Sí"** en el diálogo
3. Seleccionar el **destino/ruta** deseada
4. Click en **"Confirmar"**
5. Se muestra un resumen del viaje con:
   - Cantidad de pasajeros
   - Recaudación total
   - Desglose por tipo
   - Hora de salida

#### 3?? Gestión de Múltiples Combis

1. Click en **"Gestión de Múltiples Combis"** (botón verde)
2. Se abre ventana con 3 paneles:
   - **Izquierda**: Lista de combis disponibles
   - **Centro**: Pasajeros de la combi seleccionada
   - **Derecha**: Acciones (agregar/quitar, iniciar viaje)

**Crear Nueva Combi:**
- Click en **"+ Nueva Combi"**
- Ingresar nombre y capacidad
- La combi aparece en la lista

**Agregar Pasajero a Combi:**
- Seleccionar combi en panel izquierdo
- Ingresar nombre y tipo en panel derecho
- Click en **"+ Agregar Pasajero"**

**Quitar Pasajero:**
- Seleccionar combi
- Seleccionar pasajero en panel central
- Click en **"- Quitar Pasajero Seleccionado"**

#### 4?? Generar Reporte

1. Click en **"Generar Reporte Del Día"**
2. El archivo se crea automáticamente: `Reporte_YYYYMMDD_HHmmss.txt`
3. Se abre en **Notepad** para visualización
4. Guardar en la ubicación deseada

#### 5?? Ver Estadísticas

Las estadísticas se muestran en tiempo real en la interfaz:
- **Total de viajes** del día
- **Total de pasajeros** transportados
- **Recaudación acumulada**
- **Desglose por tipo** de pasajero

---

## ?? ESTRUCTURA DEL PROYECTO

```
AppCombis/
?
??? AppCombis/                      # Proyecto principal
?   ??? Form1.cs                    # Formulario principal (lógica)
?   ??? Form1.Designer.cs           # Diseño visual del Form1
?   ??? Form1.resx                  # Recursos del Form1
?   ?
?   ??? FormGestionCombis.cs        # Gestión de múltiples combis
?   ??? FormGestionCombis.Designer.cs
?   ??? FormGestionCombis.resx
?   ?
?   ??? Combi.cs                    # Clase modelo: Combi
?   ??? Pasajero.cs                 # Clase modelo: Pasajero
?   ??? EstadisticasDiarias.cs      # Clase modelo: Estadísticas
?   ?
?   ??? SoftUIPanel.cs              # Componente UI personalizado
?   ??? SoftUIButton.cs             # Componente UI personalizado
?   ??? SoftUISidebar.cs            # Componente UI personalizado
?   ??? CombiCardItem.cs            # Card personalizado para combis
?   ??? CombiListContainer.cs       # Contenedor personalizado
?   ?
?   ??? Program.cs                  # Punto de entrada de la app
?   ??? AppCombis.csproj            # Archivo de proyecto
?   ?
?   ??? bin/Debug/net8.0-windows/   # Archivos compilados
?       ??? AppCombis.exe           # Ejecutable
?       ??? fila_espera.txt         # Persistencia: fila de espera
?       ??? estadisticas.txt        # Persistencia: estadísticas
?       ??? combis.txt              # Persistencia: combis
?       ??? Reporte_*.txt           # Reportes generados
?
??? README.md                        # Este archivo
??? SOFT_UI_GUIDE.md                # Guía de diseño Soft UI
??? MATERIAL_DESIGN_GUIDE.md        # Guía de Material Design
??? NUEVAS_FUNCIONALIDADES.md       # Documentación de funciones
??? GUIA_SISTEMA_INTEGRADO.md       # Guía del sistema integrado
??? PROMPT_DIAPOSITIVAS.md          # Prompt para generar presentación
?
??? .gitignore                      # Archivos ignorados por Git
??? AppCombis.sln                   # Solución de Visual Studio
```

### Descripción de Archivos Clave

| Archivo | Líneas | Descripción |
|---------|--------|-------------|
| **Form1.cs** | ~800 | Lógica principal del formulario (eventos, validaciones) |
| **Combi.cs** | ~350 | Modelo de combi con métodos de gestión |
| **Pasajero.cs** | ~180 | Modelo de pasajero con tarifas y serialización |
| **EstadisticasDiarias.cs** | ~220 | Gestión de estadísticas y generación de reportes |
| **FormGestionCombis.cs** | ~500 | Gestión de múltiples combis simultáneas |

---

## ??? ESTRUCTURAS DE DATOS

### Queue<Pasajero> - Cola FIFO

```csharp
private Queue<Pasajero> filaDeEspera = new Queue<Pasajero>();

// Agregar al final de la fila
filaDeEspera.Enqueue(nuevoPasajero);

// Quitar del frente (primero en entrar, primero en salir)
Pasajero primero = filaDeEspera.Dequeue();

// Ver el primero sin quitarlo
Pasajero proximo = filaDeEspera.Peek();

// Cantidad de elementos
int cantidad = filaDeEspera.Count;
```

**¿Por qué Queue?**
- ? Implementa perfectamente el principio FIFO requerido
- ? Operaciones Enqueue y Dequeue en O(1)
- ? Representa naturalmente una "fila de espera"

### List<Pasajero> - Lista Dinámica

```csharp
private List<Pasajero> pasajerosEnCombi = new List<Pasajero>();

// Agregar elemento
pasajerosEnCombi.Add(pasajero);

// Acceso por índice O(1)
Pasajero p = pasajerosEnCombi[0];

// Operaciones LINQ
int normales = pasajerosEnCombi.Count(p => p.Tipo == TipoPasajero.Normal);
decimal total = pasajerosEnCombi.Sum(p => p.Tarifa);
var ordenados = pasajerosEnCombi.OrderBy(p => p.Nombre);
```

**¿Por qué List?**
- ? Acceso rápido por índice
- ? Compatible con LINQ para consultas
- ? Tamaño dinámico
- ? Ideal para almacenar historial de viajes

### Comparación de Complejidades

| Operación | Queue<T> | List<T> |
|-----------|----------|---------|
| **Agregar al final** | O(1) | O(1) amortizado |
| **Quitar del inicio** | O(1) | O(n) |
| **Acceso por índice** | O(n) | O(1) |
| **Búsqueda** | O(n) | O(n) |
| **Ordenamiento** | N/A | O(n log n) |

---

## ?? CAPTURAS DE Pantalla

### Pantalla Principal
```
??????????????????????????????????????????????????????
?  ?? Sistema de Gestión de Combis - Terminal Obelisco?
?                                                     ?
?  ???????????????????  ?????????????????????????????
?  ? PASAJEROS       ?  ? INFORMACIÓN DEL VIAJE    ??
?  ? EN ESPERA       ?  ?                          ??
?  ?                 ?  ? Tiempo restante: 18:45   ??
?  ? [N] Juan Perez  ?  ? Destino: Puerto Madero   ??
?  ? [E] Ana Garcia  ?  ? Pasajeros: 12/19         ??
?  ? [J] Luis Lopez  ?  ? Recaudación: $5,250      ??
?  ?                 ?  ?                          ??
?  ???????????????????  ?????????????????????????????
?                                                     ?
?  [Anotar Pasajero]  [Iniciar Viaje]  [Estadísticas]?
??????????????????????????????????????????????????????
```

### Panel de Estadísticas
```
??????????????????????????????????????????
?  ?? ESTADÍSTICAS DEL DÍA               ?
?                                         ?
?  Viajes realizados:       15           ?
?  Total pasajeros:         237          ?
?  Recaudación total:       $98,750.00   ?
?                                         ?
?  ?? Desglose por tipo ??               ?
?  [N] Normales:     145 (61.2%)         ?
?  [E] Estudiantes:   67 (28.3%)         ?
?  [J] Jubilados:     25 (10.5%)         ?
?                                         ?
?  [Generar Reporte]                     ?
??????????????????????????????????????????
```

---

## ?? DOCUMENTACIÓN ADICIONAL

### Documentos Disponibles

1. **README.md** (este archivo) - Documentación general del proyecto
2. **SOFT_UI_GUIDE.md** - Guía completa de diseño Soft UI / Neumorphism
3. **MATERIAL_DESIGN_GUIDE.md** - Guía de Material Design (alternativa)
4. **NUEVAS_FUNCIONALIDADES.md** - Documentación de características nuevas
5. **GUIA_SISTEMA_INTEGRADO.md** - Guía del sistema integrado
6. **PROMPT_DIAPOSITIVAS.md** - Prompt para generar presentación PowerPoint

### Conceptos Clave Implementados

#### 1. Principio FIFO (First-In First-Out)

```csharp
// El primer pasajero en anotarse es el primero en subir
public void IniciarViaje()
{
    while (filaDeEspera.Count > 0)
    {
        Pasajero pasajero = filaDeEspera.Dequeue(); // Extrae del frente
        pasajerosEnCombi.Add(pasajero);
    }
}
```

#### 2. Enumeraciones Type-Safe

```csharp
public enum TipoPasajero
{
    Normal = 0,
    Estudiante = 1,
    Jubilado = 2
}

// Uso con switch expression (C# 8.0+)
public decimal Tarifa => Tipo switch
{
    TipoPasajero.Normal => 500m,
    TipoPasajero.Estudiante => 250m,
    TipoPasajero.Jubilado => 0m,
    _ => 500m
};
```

#### 3. Propiedades Calculadas

```csharp
// Propiedad de solo lectura calculada dinámicamente
public decimal RecaudacionEnEspera
{
    get
    {
        return FilaDeEspera.Sum(p => p.Tarifa);
    }
}
```

#### 4. Serialización CSV

```csharp
// Convertir objeto a formato CSV
public string ToCsv()
{
    return $"{Nombre}|{(int)Tipo}|{HoraAnotacion:yyyy-MM-dd HH:mm:ss}";
}

// Crear objeto desde CSV
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

---

## ?? PRUEBAS Y TESTING

### Casos de Prueba Ejecutados

| # | Caso de Prueba | Entrada | Resultado Esperado | Estado |
|---|----------------|---------|-------------------|--------|
| 1 | Anotar pasajero normal | Nombre: "Juan", Tipo: Normal | Se agrega a la fila, $500 | ? |
| 2 | Anotar con combi llena | 20° pasajero | Mensaje "Combi llena" | ? |
| 3 | Viaje con tipos mixtos | 5N, 3E, 2J | Recaudación: $3,250 | ? |
| 4 | Temporizador automático | Esperar 20 min | Viaje inicia solo | ? |
| 5 | Persistencia de datos | Cerrar/Abrir app | Datos se restauran | ? |
| 6 | Generación de reporte | Click "Generar" | Archivo .txt creado | ? |
| 7 | Múltiples combis | 3 combis simultáneas | Funcionan independientemente | ? |

### Métricas del Proyecto

| Métrica | Valor |
|---------|-------|
| **Líneas de código (total)** | ~2,500 |
| **Clases implementadas** | 8 |
| **Métodos públicos** | 45+ |
| **Controles UI** | 30+ |
| **Archivos de documentación** | 6 |
| **Casos de prueba** | 20+ |

---

## ?? CARACTERÍSTICAS FUTURAS (Roadmap)

### Versión 2.0 (Planeada)

- [ ] **Base de datos SQL Server** en lugar de archivos CSV
- [ ] **Autenticación de usuarios** con roles (Admin, Operador)
- [ ] **Reservas anticipadas** por teléfono/web
- [ ] **Notificaciones push** cuando falta poco para salir
- [ ] **Integración con mapas** para visualizar rutas
- [ ] **Dashboard web** para monitoreo remoto
- [ ] **App móvil** para pasajeros
- [ ] **Sistema de pagos digitales** (QR, tarjetas)

### Versión 3.0 (Visión a largo plazo)

- [ ] **Machine Learning** para predicción de demanda
- [ ] **IoT Integration** con GPS en combis
- [ ] **Sistema de calificaciones** y feedback
- [ ] **Multilenguaje** (español, inglés, portugués)
- [ ] **Modo oscuro** en la interfaz
- [ ] **Exportación a Excel/PDF** de reportes
- [ ] **API REST** para integraciones externas

---

## ?? PROBLEMAS CONOCIDOS Y SOLUCIONES

### Problema 1: Temporizador no se restaura correctamente

**Síntoma:** Al cerrar y reabrir, el tiempo no considera lo transcurrido.

**Solución:** Implementado cálculo de diferencia entre `DateTime.Now` y `HoraAnotacion`:

```csharp
TimeSpan transcurrido = DateTime.Now - primerPasajero.HoraAnotacion;
tiempoRestanteSegundos = 1200 - (int)transcurrido.TotalSeconds;
```

### Problema 2: No se puede iterar Queue sin modificarla

**Síntoma:** `foreach` en `Queue` lanza excepción si se modifica durante iteración.

**Solución:** Usar `.ToArray()` para crear snapshot:

```csharp
foreach (var pasajero in filaDeEspera.ToArray())
{
    // Operaciones seguras
}
```

### Problema 3: Codificación de caracteres en archivos

**Síntoma:** Caracteres especiales (tildes, ñ) no se guardan correctamente.

**Solución:** Especificar encoding UTF-8:

```csharp
using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
{
    // Escritura con soporte completo de caracteres
}
```

---

## ?? LICENCIA

Este proyecto es un trabajo académico realizado para la asignatura **Programación y Estructuras de Datos** de la carrera de **Ingeniería en Sistemas** en la **Universidad Abierta Interamericana (UAI)**.

**Uso Académico:** Permitido para fines educativos y de aprendizaje.
**Uso Comercial:** Requiere autorización expresa de los autores.

---

## ????? AUTORES Y AGRADECIMIENTOS

### Desarrolladores

- **Ignacio Mondragón** - Desarrollador Principal - [@ignaciomondragon24](https://github.com/ignaciomondragon24)
- **[Nombre Integrante 2]** - Desarrollador / Diseñador UI
- **[Nombre Integrante 3]** - Analista / Tester / Documentación

### Agradecimientos

- **Cátedra de Programación y Estructuras de Datos - UAI** por el acompañamiento y guía durante el desarrollo
- **Microsoft Learn** por la excelente documentación de C# y .NET
- **Comunidad de Stack Overflow** por resolver dudas técnicas
- **Compañeros de cursada** por el feedback constructivo

---

## ?? CONTACTO Y SOPORTE

### Repositorio del Proyecto

?? **GitHub:** [https://github.com/ignaciomondragon24/tp12](https://github.com/ignaciomondragon24/tp12)

### Reportar Problemas

Si encuentras algún bug o tienes sugerencias:
1. Abre un **Issue** en GitHub
2. Describe el problema detalladamente
3. Incluye capturas de pantalla si es posible
4. Menciona tu sistema operativo y versión de .NET

### Contacto Directo

- **Email Académico:** [tu-email]@uai.edu.ar
- **GitHub Issues:** [github.com/ignaciomondragon24/tp12/issues](https://github.com/ignaciomondragon24/tp12/issues)

---

## ?? INFORMACIÓN ACADÉMICA

### Fundamentación del Proyecto

Este trabajo práctico integra los siguientes contenidos de la materia:

1. **Unidad 1 - Estructuras de Datos Lineales:**
   - ? Colas (Queue) - Implementación FIFO
   - ? Listas (List) - Manejo dinámico de colecciones

2. **Unidad 2 - Programación Orientada a Objetos:**
   - ? Clases y objetos
   - ? Encapsulamiento y propiedades
   - ? Herencia y polimorfismo
   - ? Enumeraciones

3. **Unidad 3 - Manejo de Archivos:**
   - ? Lectura y escritura en archivos de texto
   - ? Serialización de objetos
   - ? Formato CSV

4. **Unidad 4 - Interfaces Gráficas:**
   - ? Windows Forms
   - ? Eventos y delegados
   - ? Controles personalizados

### Competencias Desarrolladas

- ? **Análisis y diseño** de sistemas de software
- ? **Implementación** de algoritmos y estructuras de datos
- ? **Programación orientada a objetos** en C#
- ? **Diseño de interfaces** de usuario intuitivas
- ? **Persistencia de datos** mediante archivos
- ? **Testing y depuración** de aplicaciones
- ? **Documentación técnica** y de usuario
- ? **Trabajo en equipo** y control de versiones (Git)

---

## ?? ESTADÍSTICAS DEL PROYECTO

### Commits y Desarrollo

```bash
# Ver estadísticas del repositorio
git log --oneline --graph --all
git shortlog -sn --all
```

### Tecnologías (Detalle)

- **Lenguaje Principal:** C# 12.0 (100%)
- **Framework:** .NET 8.0
- **UI Framework:** Windows Forms
- **Persistencia:** File System (CSV)
- **Control de Versiones:** Git + GitHub

---

## ?? CONCLUSIÓN

El **Sistema de Gestión de Combis - Terminal Obelisco** es una aplicación completa y funcional que demuestra la aplicación práctica de conceptos fundamentales de programación y estructuras de datos. 

Logros principales:
- ? Implementación correcta de estructuras de datos (Queue, List)
- ? Interfaz gráfica moderna y profesional (Soft UI)
- ? Funcionalidades completas y robustas
- ? Código limpio, documentado y mantenible
- ? Persistencia de datos confiable
- ? Testing exhaustivo y documentación completa

Este proyecto representa el esfuerzo conjunto del equipo de desarrollo y el conocimiento adquirido durante la cursada de Programación y Estructuras de Datos en la UAI.

---

<div align="center">

**Desarrollado con ?? por estudiantes de Ingeniería en Sistemas - UAI**

![UAI Logo](https://www.uai.edu.ar/img/logo-uai.png)

**Universidad Abierta Interamericana**  
*Ingeniería en Sistemas - 2025*

</div>

---

**Última actualización:** Enero 2025  
**Versión del documento:** 2.0  
**Estado del proyecto:** ? Completado y entregado
