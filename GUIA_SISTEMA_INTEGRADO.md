# ?? SISTEMA DE COMBIS - VERSIÓN INTEGRADA Y OPTIMIZADA

## ? Mejoras Implementadas

### 1. **Interfaz Integrada en un Solo Formulario**
- ? Todo ahora está en la ventana principal (Form1)
- ? No hay ventanas secundarias
- ? Diseño compacto optimizado para monitores de 19 pulgadas (950x435 px)
- ? Controles más pequeños y eficientes

### 2. **Timers Independientes por Combi** ??
- ? Cada combi tiene su propio temporizador de 20 minutos
- ? El timer inicia automáticamente cuando se anota el PRIMER pasajero
- ? Cada combi cuenta regresivamente de forma independiente
- ? Indicador visual en tiempo real del tiempo restante
- ? Colores de alerta: Azul (normal), Naranja (<5 min), Rojo (<1 min)
- ? Salida automática cuando el tiempo llega a 00:00

### 3. **Destino Asignado al Crear la Combi** ??
- ? Se selecciona el destino al crear la combi
- ? 6 destinos preconfigurados:
  - Puerto Madero (20 min)
  - Recoleta (25 min)
  - Palermo (30 min)
  - Retiro (15 min)
  - San Telmo (25 min)
  - Belgrano (35 min)
- ? El destino se muestra siempre en la lista de combis
- ? Cada combi tiene su ruta fija

### 4. **Diseño Compacto y Eficiente** ??
- ? Tamaño reducido: 950x435 px (antes: ~1000x560 px)
- ? Fuentes más pequeñas pero legibles (8-9pt)
- ? Controles ajustados para maximizar espacio
- ? Perfecto para monitores de 19 pulgadas

---

## ?? Distribución de la Interfaz

```
??????????????????????????????????????????????????????????????
?  ?? SISTEMA DE GESTIÓN DE COMBIS - Terminal Obelisco      ?
??????????????????????????????????????????????????????????????
?   COMBIS     ?   PASAJEROS EN ESPERA       ?   AGREGAR     ?
?              ?                             ?   PASAJERO    ?
? ?? Combi 1   ? ?? Combi 1 | ?? Puerto      ?               ?
?  [18:45]     ? ?? 5/19 | ?? $2,250        ? Nombre:       ?
? ? Puerto     ? ??N:3 | ??E:1 | ??J:1     ? [___________] ?
? ?? 5/19      ? ?? 18:45                   ?               ?
?              ?                             ? Tipo:         ?
? ?? Combi 2   ? 1. [N] Juan (Normal) - $500 ? [__Combo___]  ?
?  [20:00]     ? 2. [E] María (Estud) - $250 ?               ?
? ? Recoleta   ? 3. [J] Pedro (Jubil) - $0   ? [? Agregar]  ?
? ?? 0/19      ? ...                          ?               ?
?              ?                             ?????????????????
? + Nueva      ? [- Quitar Pasajero]         ? ESTADÍSTICAS  ?
? - Eliminar   ? [?? Iniciar Viaje]          ?               ?
?              ?                             ? ?? Viajes: 0  ?
?              ?                             ? ?? Pasaj: 0   ?
?              ?                             ? ?? Recau: $0  ?
?              ?                             ?               ?
?              ?                             ? [?? Reporte]  ?
?              ?                             ? [? Cerrar]   ?
??????????????????????????????????????????????????????????????
```

---

## ?? Funcionamiento del Timer Independiente

### Comportamiento Automático:

1. **Estado Inicial:** Todas las combis muestran `[20:00]` pero el timer NO está activo
2. **Al agregar el PRIMER pasajero:**
   - Timer de esa combi SE INICIA automáticamente
   - Mensaje: "? PRIMER PASAJERO ANOTADO - ?? Timer iniciado: 20:00"
   - Cuenta regresiva comienza: 19:59, 19:58, 19:57...
3. **Mientras cuenta:**
   - El timer se actualiza cada segundo SOLO para esa combi
   - Otras combis pueden tener timers diferentes
   - Color cambia según urgencia
4. **Al llegar a 00:00:**
   - La combi PARTE AUTOMÁTICAMENTE
   - Se registra el viaje
   - Se muestra resumen
   - La combi vuelve a estar disponible

### Ejemplo Real:

```
08:00 ? Se anota primer pasajero en Combi 1
        Timer Combi 1: [20:00] ? [19:59] ? [19:58]...

08:05 ? Se anota primer pasajero en Combi 2
        Timer Combi 1: [15:00]
        Timer Combi 2: [20:00] ? Empieza independiente

08:20 ? Combi 1 agota tiempo
        Timer Combi 1: [00:00] ? PARTE AUTOMÁTICO
        Timer Combi 2: [05:00] ? Sigue su cuenta
```

---

## ?? Indicadores Visuales

### En la Lista de Combis:
```
?? Combi 1 [18:45]          ? Tiempo restante
   ? Puerto Madero (20 min)  ? Destino
   ?? 5/19 | ?? $2,250       ? Pasajeros y recaudación
```

### Colores del Timer:
- **Azul** `[20:00] - [05:01]`: Tiempo suficiente
- **Naranja** `[05:00] - [01:01]`: Alerta, salida próxima
- **Rojo** `[01:00] - [00:00]`: ¡Urgente! Salida inminente

### Iconos Utilizados:
- ?? Combi
- ?? Pasajeros
- ?? Recaudación
- ?? Timer
- ?? Destino
- ?? Normal
- ?? Estudiante
- ?? Jubilado
- ?? Iniciar viaje
- ? Confirmación
- ? Cerrar

---

## ?? Flujo de Trabajo Completo

### Crear una Nueva Combi:

1. Click en **"+ Nueva Combi"**
2. Formulario popup aparece
3. Llenar datos:
   - **Nombre:** "Combi 3"
   - **Destino:** Seleccionar de lista (ej: "Palermo (30 min)")
   - **Capacidad:** 19 (por defecto, editable)
4. Click **"Crear"**
5. La combi aparece en la lista con su destino asignado

### Agregar Pasajeros:

1. **Seleccionar combi** de la lista (click)
2. En panel derecho:
   - Ingresar nombre
   - Seleccionar tipo (?? Normal / ?? Estudiante / ?? Jubilado)
3. Click **"? Agregar Pasajero"**
4. Si es el primer pasajero:
   - Timer SE INICIA automáticamente
   - Mensaje especial se muestra
5. Pasajero aparece en la lista central
6. Timer cuenta regresivamente

### Quitar un Pasajero:

1. Seleccionar combi
2. Seleccionar pasajero en lista central
3. Click **"- Quitar Pasajero"**
4. Confirmar
5. Pasajero eliminado
6. Si era el último:
   - Timer SE DETIENE
   - Combi vuelve a `[20:00]` en espera

### Iniciar Viaje Manualmente:

1. Seleccionar combi con pasajeros
2. Click **"?? Iniciar Viaje"**
3. Confirmar (se muestra resumen)
4. Viaje se registra en estadísticas
5. Se muestra detalle completo
6. Combi se resetea y queda disponible

### Viaje Automático (Timer agotado):

1. Sistema detecta timer en `[00:00]`
2. Viaje SE INICIA AUTOMÁTICAMENTE
3. Mensaje: "? TIEMPO AGOTADO - La combi partió automáticamente"
4. Se muestra resumen del viaje
5. Estadísticas se actualizan
6. Combi vuelve a estado disponible

---

## ?? Tamaños Optimizados

| Elemento | Tamaño | Notas |
|----------|--------|-------|
| **Formulario** | 950x435 px | Compacto para monitor 19" |
| **Panel Combis** | 220x380 px | Lista vertical |
| **Panel Pasajeros** | 420x380 px | Vista central |
| **Panel Agregar** | 270x200 px | Formulario rápido |
| **Panel Estadísticas** | 270x170 px | Info resumida |
| **Fuentes** | 8-9pt | Legible y compacta |
| **Botones** | 25-35px alto | Clickeables |

---

## ?? Persistencia de Datos

### Archivos Guardados:

1. **`combis.txt`**
   - Información de cada combi
   - Formato: `NumCombi|Nombre|Destino|Capacidad|Estado|Tiempo|HoraInicio`

2. **`pasajeros_combis.txt`**
   - Pasajeros asignados a cada combi
   - Formato: `NumCombi|NombrePasajero|Tipo|HoraAnotacion`

3. **`estadisticas.txt`**
   - Estadísticas del día
   - Formato: `Campo|Valor`

### Al Cerrar y Reabrir:

? Todas las combis se restauran con su configuración
? Pasajeros vuelven a sus respectivas filas
? Timers se recalculan desde la hora de anotación
? Estadísticas del día se mantienen

---

## ?? Casos de Uso Prácticos

### Caso 1: Múltiples Combis con Timers Diferentes

```
09:00 ? Crear Combi 1 (Puerto Madero)
09:01 ? Agregar 5 pasajeros a Combi 1
        ? Timer Combi 1: [19:59]

09:10 ? Crear Combi 2 (Recoleta)
09:11 ? Agregar 3 pasajeros a Combi 2
        ? Timer Combi 1: [10:00]
        ? Timer Combi 2: [19:59]

09:21 ? Combi 1 parte automáticamente
        ? Timer Combi 2: [10:00] (sigue su cuenta)
```

### Caso 2: Cambiar Pasajero de Combi

```
1. Pasajero se anota en Combi 1
2. Seleccionar Combi 1
3. Seleccionar pasajero
4. Click "Quitar"
5. Seleccionar Combi 2
6. Agregar pasajero a Combi 2
? Pasajero ahora está en Combi 2
```

### Caso 3: Cancelación

```
1. Pasajero anotado
2. Decide no viajar
3. Seleccionar pasajero
4. Click "Quitar"
? Lugar liberado inmediatamente
```

---

## ?? Ventajas de la Nueva Versión

### Usabilidad:
? **Todo en una ventana:** No hay que cambiar entre ventanas
? **Más rápido:** Menos clicks para cada acción
? **Visual claro:** Información organizada y compacta
? **Iconos intuitivos:** Fácil de entender de un vistazo

### Funcionalidad:
? **Timers reales:** Cada combi es independiente
? **Destinos claros:** Se sabe a dónde va cada combi
? **Automático:** Viajes inician solos al agotar tiempo
? **Flexible:** Agregar/quitar pasajeros libremente

### Técnico:
? **Eficiente:** Un solo timer global actualiza todas las combis
? **Escalable:** Agregar infinitas combis
? **Persistente:** Datos se guardan automáticamente
? **Robusto:** Validaciones en cada acción

---

## ?? Comparación: Antes vs Ahora

| Aspecto | Versión Anterior | Versión Nueva |
|---------|------------------|---------------|
| **Ventanas** | 2 separadas | 1 integrada |
| **Tamaño** | 1000x560 px | 950x435 px |
| **Timers** | 1 global | Independientes por combi |
| **Destino** | Al iniciar viaje | Al crear combi |
| **Inicio timer** | Manual | Automático (1er pasajero) |
| **Usabilidad** | Mediana | Alta |
| **Monitor 19"** | ? Se ve mal | ? Perfecto |

---

## ??? Configuración Avanzada

### Cambiar Destinos Disponibles

En `Combi.cs`, línea 39:
```csharp
public static readonly string[] DestinosDisponibles = new[]
{
    "Puerto Madero (20 min)",
    "Recoleta (25 min)",
    "Palermo (30 min)",
    "Retiro (15 min)",
    "San Telmo (25 min)",
    "Belgrano (35 min)"
    // Agregar más aquí...
};
```

### Cambiar Tiempo de Espera (20 minutos)

En `Combi.cs`, constructor, línea 46:
```csharp
TiempoRestanteSegundos = 1200; // 1200 seg = 20 min
```

### Cambiar Capacidad por Defecto

En `Form1.cs`, método `btnNuevaCombi_Click`, línea ~211:
```csharp
Value = 19  // Cambiar a la capacidad deseada
```

---

## ?? Requisitos del Sistema

- **Sistema Operativo:** Windows 10/11
- **Framework:** .NET 8
- **Resolución Mínima:** 1024x768 (optimizado para 19")
- **Memoria:** 256 MB RAM (mínimo)
- **Espacio:** 50 MB

---

## ?? Atajos de Teclado

| Tecla | Acción |
|-------|--------|
| **F5** | Ejecutar/Depurar |
| **Tab** | Navegar entre controles |
| **Enter** | Agregar pasajero (si está en el textbox) |
| **Esc** | Cerrar diálogos |
| **Alt+F4** | Cerrar aplicación |

---

## ?? Archivos del Proyecto

```
AppCombis/
??? Form1.cs                    ? Lógica principal (TODO INTEGRADO)
??? Form1.Designer.cs           ? Diseño UI (optimizado)
??? Pasajero.cs                 ? Clase Pasajero
??? Combi.cs                    ? Clase Combi (con destino y timer)
??? EstadisticasDiarias.cs      ? Estadísticas y reportes
??? Program.cs                  ? Punto de entrada
??? AppCombis.csproj            ? Configuración del proyecto

Archivos Generados (runtime):
??? combis.txt                  ? Estado de combis
??? pasajeros_combis.txt        ? Pasajeros por combi
??? estadisticas.txt            ? Stats del día
??? Reporte_Combis_*.txt        ? Reportes generados
```

---

## ? Checklist de Funcionalidades

- [x] Interfaz integrada en un solo formulario
- [x] Diseño compacto 950x435 px
- [x] Timer independiente por combi
- [x] Timer inicia automáticamente con 1er pasajero
- [x] Destino seleccionable al crear combi
- [x] 6 destinos preconfigurados
- [x] Indicadores visuales de tiempo
- [x] Colores de alerta (azul/naranja/rojo)
- [x] Salida automática a los 00:00
- [x] Agregar/quitar pasajeros
- [x] Múltiples combis simultáneas
- [x] Estadísticas en tiempo real
- [x] Persistencia de datos
- [x] Generación de reportes
- [x] Iconos descriptivos
- [x] Validaciones completas

---

## ?? ¡Listo para Usar!

El sistema está completamente integrado, optimizado y funcional.

**Para comenzar:**
1. Presiona **F5**
2. Click **"+ Nueva Combi"**
3. Selecciona nombre y destino
4. Agrega pasajeros
5. **El timer inicia automáticamente** cuando agregas el primero
6. Observa cómo cada combi tiene su propio timer
7. ¡Gestiona tu terminal como un profesional! ???

---

**Desarrollado con:** C# 12.0 | .NET 8 | Windows Forms  
**Optimizado para:** Monitores 19" | 1024x768+  
**Actualizado:** Diciembre 2024
