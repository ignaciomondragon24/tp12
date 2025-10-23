# ?? GUÍA RÁPIDA DE INICIO
## Sistema de Combis - Versión Mejorada

---

## ? INICIO RÁPIDO (5 MINUTOS)

### 1?? **Abrir el Proyecto**
```
1. Visual Studio 2022
2. Archivo ? Abrir ? Proyecto/Solución
3. Seleccionar: AppCombis.sln
4. Presionar F5 (o click en ? Iniciar)
```

---

### 2?? **Primera Prueba: Viaje Simple**

#### Paso 1: Anotar Pasajeros
```
1. Escribir "Juan Pérez"
2. Seleccionar "?? Normal - $500"
3. Click "Anotar"
   ? Temporizador inicia: 20:00

4. Escribir "María García"
5. Seleccionar "?? Estudiante - $250"
6. Click "Anotar"

7. Escribir "Roberto López"
8. Seleccionar "?? Jubilado - Gratis"
9. Click "Anotar"
```

#### Paso 2: Iniciar Viaje
```
1. Click "?? Subir a la combi (Iniciar Viaje)"
2. Click "Yes" para confirmar
3. Seleccionar "?? Ruta 1: Obelisco ? Puerto Madero"
4. Click "Confirmar"
5. Ver detalles del viaje:
   - 3 pasajeros
   - Recaudación: $750
   - Desglose por tipo
```

#### Paso 3: Ver Estadísticas
```
Observar panel "?? Estadísticas":
?? Viajes: 1
?? Pasajeros: 3
?? Recaudación: $750.00
```

---

### 3?? **Generar Reporte**
```
1. Click "?? Generar Reporte Del Día"
2. Click "Yes" para abrir en Notepad
3. Ver reporte completo con estadísticas
```

---

## ?? FUNCIONALIDADES PRINCIPALES

### ?? **Tipos de Pasajeros**
| Tipo | Tarifa | Cómo Seleccionar |
|------|--------|------------------|
| ?? Normal | $500 | ComboBox ? "?? Normal - $500" |
| ?? Estudiante | $250 | ComboBox ? "?? Estudiante - $250" |
| ?? Jubilado | $0 | ComboBox ? "?? Jubilado - Gratis" |

### ? **Temporizador**
- **Inicia:** Al anotar el primer pasajero
- **Duración:** 20:00 minutos
- **Colores:**
  - ?? Azul: +5 minutos
  - ?? Naranja: 1-5 minutos
  - ?? Rojo: <1 minuto
- **Salida automática:** Cuando llega a 00:00

### ??? **Rutas Disponibles**
- **?? Ruta 1:** Obelisco ? Puerto Madero (20 min)
- **?? Ruta 2:** Obelisco ? Recoleta (25 min)
- **?? Ruta 3:** Obelisco ? Palermo (30 min)

---

## ?? PRUEBAS RÁPIDAS

### ? Prueba 1: Diferentes Tipos de Pasajeros
```
1. Anotar 5 Normal, 3 Estudiante, 2 Jubilado
2. Iniciar viaje
3. Verificar recaudación:
   (5 × $500) + (3 × $250) + (2 × $0) = $3,250
```

### ? Prueba 2: Temporizador Automático
```
1. Anotar 1 pasajero
2. NO presionar "Subir"
3. Esperar 20 minutos (o modificar const a 10 segundos para prueba)
4. Combi parte automáticamente
```

### ? Prueba 3: Persistencia
```
1. Anotar 3 pasajeros
2. Cerrar aplicación
3. Reabrir
4. Pasajeros están ahí
5. Temporizador continúa desde donde quedó
```

### ? Prueba 4: Capacidad Máxima
```
1. Anotar 19 pasajeros
2. Intentar anotar el #20
3. Mensaje: "Combi llena"
```

### ? Prueba 5: Reporte Completo
```
1. Realizar 3 viajes diferentes
2. Click "Generar Reporte"
3. Ver archivo con detalles de los 3 viajes
```

---

## ?? SOLUCIÓN DE PROBLEMAS

### ? Error: "No se puede iniciar el temporizador"
**Solución:** Verificar que `timerCombi.Interval = 1000` en el Designer

### ? Error: "No se encuentra el archivo"
**Solución:** Los archivos se crean en `bin\Debug\net8.0-windows\`

### ? Error: "La combi no parte automáticamente"
**Solución:** Verificar que el evento `timerCombi_Tick` está conectado

### ? El temporizador no cambia de color
**Solución:** Verificar la lógica en `ActualizarTiempoRestante()`

---

## ?? VALORES DE EJEMPLO PARA PRUEBAS

### Conjunto 1: Balanceado
```
5 Normales
5 Estudiantes
5 Jubilados
Total: 15 pasajeros
Recaudación esperada: $3,750
```

### Conjunto 2: Mayoría Normal
```
10 Normales
3 Estudiantes
2 Jubilados
Total: 15 pasajeros
Recaudación esperada: $5,750
```

### Conjunto 3: Mayoría Jubilados
```
2 Normales
2 Estudiantes
15 Jubilados
Total: 19 pasajeros (LLENO)
Recaudación esperada: $1,500
```

---

## ?? ATAJOS DE TECLADO

| Tecla | Acción |
|-------|--------|
| **F5** | Iniciar/Depurar |
| **Ctrl+F5** | Ejecutar sin depurar |
| **Shift+F5** | Detener depuración |
| **F11** | Paso a paso (debug) |
| **Tab** | Moverse entre controles |
| **Enter** | Activar botón "Anotar" (si txtPasajero tiene foco) |

---

## ?? ESTRUCTURA DE ARCHIVOS

```
AppCombis/
?
??? Form1.cs                      # Lógica principal
??? Form1.Designer.cs             # Diseño de UI
??? Pasajero.cs                   # Clase Pasajero
??? EstadisticasDiarias.cs        # Clase Estadísticas
?
??? DOCUMENTACION_MEJORADA.md     # Doc técnica completa
??? CASOS_USO_DETALLADOS.md       # Casos de uso
??? RESUMEN_EJECUTIVO.md          # Resumen del proyecto
??? INICIO_RAPIDO.md              # Esta guía
?
??? README_SOLUCION.md            # Doc original
??? GUIA_DISENO_UI.md             # Guía de diseño
??? CASOS_DE_PRUEBA.md            # Casos de prueba originales

Archivos generados (en bin\Debug\net8.0-windows\):
??? fila_espera.txt               # Persistencia de fila
??? estadisticas.txt              # Estadísticas del día
??? Reporte_Combis_*.txt          # Reportes generados
```

---

## ?? CONCEPTOS CLAVE

### Cola (Queue)
```csharp
// FIFO: First-In, First-Out
Queue<Pasajero> fila = new Queue<Pasajero>();
fila.Enqueue(pasajero);  // Agregar al final
Pasajero p = fila.Dequeue();  // Quitar del inicio
```

### Lista (List)
```csharp
// Dinámica: Crece automáticamente
List<Pasajero> combi = new List<Pasajero>();
combi.Add(pasajero);  // Agregar
int cantidad = combi.Count;  // Contar
decimal total = combi.Sum(p => p.Tarifa);  // Sumar con LINQ
```

### Temporizador (Timer)
```csharp
timer.Interval = 1000;  // 1 segundo
timer.Tick += timerCombi_Tick;  // Evento
timer.Start();  // Iniciar
timer.Stop();  // Detener
```

---

## ?? TIPS Y TRUCOS

### ?? Para Pruebas Rápidas del Temporizador
Cambiar temporalmente en `Form1.cs`:
```csharp
// Original (20 minutos):
private const int TIEMPO_ESPERA_SEGUNDOS = 1200;

// Para pruebas (10 segundos):
private const int TIEMPO_ESPERA_SEGUNDOS = 10;
```

### ?? Para Ver Más Detalles en el Reporte
Modificar `EstadisticasDiarias.GenerarReporte()` para agregar más información.

### ?? Para Cambiar Colores
Modificar valores RGB en `Form1.Designer.cs`:
```csharp
btnSubir.BackColor = Color.FromArgb(46, 125, 50);  // Verde
```

---

## ? CHECKLIST DE VERIFICACIÓN

Antes de presentar/entregar:
- [ ] ? Compilación exitosa (F5 sin errores)
- [ ] ? Todos los tipos de pasajero funcionan
- [ ] ? Temporizador inicia y cuenta correctamente
- [ ] ? Las 3 rutas se pueden seleccionar
- [ ] ? Reporte se genera y abre en Notepad
- [ ] ? Persistencia funciona (cerrar/abrir)
- [ ] ? Estadísticas se actualizan en tiempo real
- [ ] ? Botón cerrar funciona con confirmación
- [ ] ? Validaciones funcionan (capacidad, mínimo)
- [ ] ? Documentación está completa

---

## ?? ¡LISTO PARA USAR!

Tu sistema está completo y funcional. 

**Siguiente paso:**
1. Ejecutar con **F5**
2. Seguir "Primera Prueba: Viaje Simple"
3. Experimentar con diferentes escenarios
4. Generar varios reportes
5. Probar persistencia

**¡Buena suerte! ????**

---

## ?? REFERENCIAS RÁPIDAS

| Documento | Para Qué |
|-----------|----------|
| `INICIO_RAPIDO.md` | Esta guía - Comenzar ahora |
| `DOCUMENTACION_MEJORADA.md` | Detalles técnicos completos |
| `CASOS_USO_DETALLADOS.md` | Pruebas paso a paso |
| `RESUMEN_EJECUTIVO.md` | Visión general del proyecto |

---

**¡Todo listo! Presiona F5 y comienza a usar el sistema! ??**
