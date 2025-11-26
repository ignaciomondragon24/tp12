# ?? NUEVAS FUNCIONALIDADES - Sistema de Combis

## ? Actualizaciones Implementadas

### 1. **Gestión de Múltiples Combis**
La aplicación ahora permite gestionar varias combis simultáneamente, cada una con su propia fila de pasajeros y temporizador independiente.

#### Características:
- ? Crear combis con nombre y capacidad personalizados
- ? Ver estado de todas las combis en tiempo real
- ? Cada combi tiene su propio temporizador de 20 minutos
- ? Temporizadores independientes para cada combi
- ? Indicadores visuales de estado (Disponible, En Espera, En Viaje)

### 2. **Modificación de Pasajeros**
Ahora puedes editar la lista de pasajeros antes de que la combi salga de viaje.

#### Características:
- ? **Agregar pasajeros** a una combi específica
- ? **Quitar pasajeros** de la fila de espera
- ? Seleccionar la combi donde agregar/quitar pasajeros
- ? Visualización en tiempo real de los cambios

---

## ?? Cómo Usar las Nuevas Funcionalidades

### Acceder a la Gestión de Múltiples Combis

1. En la pantalla principal, click en el botón **"Gestión de Múltiples Combis"** (botón verde)
2. Se abrirá una nueva ventana con tres paneles:
   - **Izquierda**: Lista de combis disponibles
   - **Centro**: Pasajeros de la combi seleccionada
   - **Derecha**: Acciones (agregar/quitar pasajeros, iniciar viaje)

### Crear una Nueva Combi

1. Click en **"+ Nueva Combi"**
2. Ingresar el nombre (ej: "Combi 4 - Retiro")
3. Ingresar la capacidad (por defecto: 19)
4. La nueva combi aparecerá en la lista

### Eliminar una Combi

1. Seleccionar la combi de la lista
2. Click en **"- Eliminar Combi"**
3. Confirmar la eliminación (si tiene pasajeros, se solicitará confirmación)

### Agregar Pasajeros a una Combi

1. **Seleccionar la combi** de la lista (panel izquierdo)
2. En el panel derecho:
   - Ingresar el **nombre** del pasajero
   - Seleccionar el **tipo** (Normal, Estudiante, Jubilado)
3. Click en **"+ Agregar Pasajero"**
4. El pasajero aparecerá en la lista del panel central

### Quitar un Pasajero

1. **Seleccionar la combi** de la lista
2. En el panel central, **seleccionar el pasajero** que deseas quitar
3. Click en **"- Quitar Pasajero Seleccionado"** (botón naranja)
4. Confirmar la acción
5. El pasajero se elimina de la fila

### Iniciar Viaje de una Combi

1. **Seleccionar la combi** que deseas que salga
2. Click en **">> Iniciar Viaje"** (botón verde grande)
3. Confirmar el viaje
4. Se mostrará el resumen del viaje con todos los detalles
5. La combi se resetea y queda disponible para un nuevo viaje

---

## ?? Información Mostrada

### Panel de Combis
```
Combi 1 - Obelisco - 5/19 [18:30]
```
- **Nombre** de la combi
- **Pasajeros actuales** / **Capacidad total**
- **Tiempo restante** (si está en espera)

### Panel de Pasajeros
```
Combi: Combi 1 - Obelisco
Pasajeros: 5/19 | Recaudación: $2,250.00
N:3 | E:1 | J:1
```
- Información de la combi seleccionada
- Cantidad de pasajeros por tipo
- Recaudación total en espera

### Lista de Pasajeros
```
1. [N] Juan Pérez (Normal) - $500 | Espera: 5 min
2. [E] María García (Estudiante) - $250 | Espera: 4 min
3. [J] Roberto López (Jubilado) - $0 | Espera: 2 min
```

---

## ?? Flujo de Trabajo Recomendado

### Escenario 1: Gestión de Múltiples Combis
```
1. Crear 3 combis diferentes
2. Agregar pasajeros a cada combi según vayan llegando
3. Monitorear los temporizadores de cada una
4. Iniciar viajes cuando estén listas
5. Las estadísticas se actualizan automáticamente
```

### Escenario 2: Modificación de Pasajeros
```
1. Pasajero se anota en Combi 1
2. Se da cuenta que quiere ir en Combi 2
3. Operador:
   - Quita al pasajero de Combi 1
   - Lo agrega a Combi 2
4. El pasajero viaja en la combi correcta
```

### Escenario 3: Cancelación de Pasajero
```
1. Pasajero se anota en una combi
2. Decide no viajar
3. Operador lo quita de la fila
4. Se libera el lugar para otro pasajero
```

---

## ?? Persistencia de Datos

Los datos de las múltiples combis se guardan automáticamente en:
- **`combis.txt`**: Información de cada combi (nombre, capacidad, estado)
- **`pasajeros_combis.txt`**: Pasajeros asignados a cada combi

Al reabrir la aplicación, todas las combis y sus pasajeros se restauran automáticamente.

---

## ?? Nuevos Archivos Creados

### `Combi.cs`
Clase que representa una combi individual con:
- Número identificador
- Nombre personalizado
- Capacidad configurable
- Cola de pasajeros
- Estado (Disponible, En Espera, En Viaje, Mantenimiento)
- Temporizador independiente

**Métodos principales:**
- `AgregarPasajero()` - Agrega un pasajero a la fila
- `QuitarPasajero()` - Remueve un pasajero específico
- `IniciarViaje()` - Obtiene todos los pasajeros y vacía la fila
- `FinalizarViaje()` - Resetea la combi para nuevo viaje
- `ContarPorTipo()` - Cuenta pasajeros por tipo (N, E, J)

### `FormGestionCombis.cs` + `FormGestionCombis.Designer.cs`
Nuevo formulario para gestionar múltiples combis con interfaz gráfica completa.

**Características:**
- Interfaz de 3 paneles
- Timer automático que actualiza cada segundo
- Gestión visual de combis y pasajeros
- Integración con estadísticas existentes

---

## ?? Ventajas de las Nuevas Funcionalidades

### Para el Operador:
? **Flexibilidad**: Gestionar varias combis desde una sola aplicación
? **Control**: Modificar pasajeros antes del viaje
? **Eficiencia**: Ver el estado de todas las combis simultáneamente
? **Organización**: Cada combi tiene su propia fila independiente

### Para los Pasajeros:
? **Mejor servicio**: Posibilidad de cambiar de combi si es necesario
? **Cancelación**: Opción de retirarse de la fila si es necesario
? **Información clara**: Saben exactamente en qué combi están

### Para el Sistema:
? **Escalabilidad**: Agregar tantas combis como sea necesario
? **Mantenibilidad**: Código modular y fácil de extender
? **Robustez**: Cada combi funciona independientemente
? **Persistencia**: Datos guardados automáticamente

---

## ?? Comparación: Antes vs Ahora

| Característica | Antes | Ahora |
|----------------|-------|-------|
| **Número de combis** | 1 fija | Ilimitadas |
| **Modificar pasajeros** | ? No | ? Sí (agregar/quitar) |
| **Nombrar combis** | ? No | ? Sí (personalizable) |
| **Capacidad** | 19 fija | ? Configurable por combi |
| **Temporizadores** | 1 global | ? Independiente por combi |
| **Cancelar pasajero** | ? Solo borrando todo | ? Individual |
| **Cambiar de combi** | ? No | ? Quitar y agregar en otra |

---

## ?? Casos de Uso Reales

### Caso 1: Terminal Concurrida
```
Terminal con 3 combis:
- Combi 1 (Puerto Madero): 15 pasajeros, sale en 5 min
- Combi 2 (Recoleta): 8 pasajeros, sale en 15 min  
- Combi 3 (Palermo): 3 pasajeros, sale en 18 min

Llega un pasajero con urgencia:
? Se agrega a Combi 1 que sale pronto
```

### Caso 2: Pasajero se Equivoca
```
María se anota en Combi 1 (Puerto Madero)
Quería ir a Palermo (Combi 3)

Operador:
1. Selecciona Combi 1
2. Quita a María
3. Selecciona Combi 3
4. Agrega a María
? Problema resuelto
```

### Caso 3: Cancelación de Último Momento
```
Juan está en la fila de Combi 2
Decide no viajar (emergencia)

Operador:
1. Selecciona Combi 2
2. Selecciona a Juan en la lista
3. Click "Quitar Pasajero"
? Lugar liberado para otro pasajero
```

---

## ??? Detalles Técnicos

### Arquitectura
```
Form1.cs (Principal)
    ?
    ?? Gestión de 1 combi (modo original)
    ?? Botón ? FormGestionCombis.cs (nuevo)
                    ?
                    ?? Gestión de N combis
                    ?? Cada combi usa: Combi.cs
                    ?? Compartido: EstadisticasDiarias.cs
```

### Estados de una Combi (enum)
```csharp
public enum EstadoCombi
{
    Disponible,      // Sin pasajeros, esperando
    EnEspera,        // Con pasajeros, timer corriendo
    EnViaje,         // Ya salió
    Mantenimiento    // No disponible
}
```

### Sincronización
- El timer del `Form1` se pausa al abrir `FormGestionCombis`
- Las estadísticas son compartidas entre ambos formularios
- Los cambios se reflejan inmediatamente en todas las interfaces

---

## ?? Configuración y Personalización

### Cambiar el Tiempo de Espera por Defecto
En `Combi.cs`, línea ~23:
```csharp
TiempoRestanteSegundos = 1200;  // 20 minutos
```

### Cambiar la Capacidad por Defecto
En `FormGestionCombis.cs`, al crear combis:
```csharp
var nuevaCombi = new Combi(numeroCombi, nombre, 19);  // Cambiar 19
```

### Agregar Más Combis Iniciales
En `FormGestionCombis_Load()`:
```csharp
combis.Add(new Combi(1, "Combi 1 - Obelisco", 19));
combis.Add(new Combi(2, "Combi 2 - Obelisco", 19));
combis.Add(new Combi(3, "Combi 3 - Obelisco", 19));
// Agregar más aquí...
```

---

## ?? Solución de Problemas

### P: No aparece el botón "Gestión de Múltiples Combis"
**R:** Recompila el proyecto (Ctrl+Shift+B) y ejecuta nuevamente.

### P: Los pasajeros no se guardan al cerrar
**R:** Verifica que los archivos `combis.txt` y `pasajeros_combis.txt` se creen en `bin\Debug\net8.0-windows\`.

### P: El timer no funciona correctamente
**R:** Asegúrate de que el `timerActualizar.Interval = 1000` (1 segundo).

### P: No puedo quitar un pasajero
**R:** Primero selecciona la combi, luego selecciona el pasajero en la lista central, y finalmente click en "Quitar".

---

## ?? Documentación Adicional

- **`INICIO_RAPIDO.md`**: Guía de inicio rápido
- **`README.md`**: Documentación principal del proyecto
- **`DEFENSA_ORAL_TP12.md`**: Información para la defensa del trabajo

---

## ?? ¡Listo para Usar!

Las nuevas funcionalidades están completamente integradas con el sistema existente. Todas las características originales siguen funcionando, y ahora tienes el poder de gestionar múltiples combis con total flexibilidad.

**Para comenzar:**
1. Ejecuta la aplicación (F5)
2. Click en "Gestión de Múltiples Combis"
3. Explora las nuevas funcionalidades
4. ¡Gestiona tu terminal de combis como un profesional! ???

---

**Desarrollado con:** C# 12.0 | .NET 8 | Windows Forms  
**Actualizado:** Diciembre 2024
